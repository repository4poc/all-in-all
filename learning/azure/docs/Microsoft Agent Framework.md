## Overview

MS Agent Framework (MAF) offers two primary categories of capabilities:

![alt text](images/{97DF56A5-686A-4E37-B0FB-D0A222E562DC}.png)

The framework also provides foundational building blocks, including

- model clients (chat completions and responses)
- an agent session for state management
- context providers for agent memory
- middleware for intercepting agent actions
- MCP clients for tool integration

Together, these components give you the flexibility and power to build interactive, robust, and safe AI applications.

```
dotnet new console -n AI-Agent-ConsoleApp
```

```
dotnet add package Azure.AI.Projects --prerelease
dotnet add package Azure.Identity
dotnet add package Microsoft.Agents.AI.Foundry --prerelease
```

```
using System;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;

AIAgent agent = new AIProjectClient(
        new Uri("https://foundryallinalldev007.openai.azure.com"),
        new AzureCliCredential())
    .AsAIAgent(
        model: "gpt-5-deployment",
        instructions: "You are a friendly assistant. Keep your answers brief.",
        name: "FriendlyAssistant");

Console.WriteLine(await agent.RunAsync("What is the largest city in France?"));

await foreach (var update in agent.RunStreamingAsync("Tell me a one-sentence 100 wordsfun fact."))
{
    Console.Write(update);
}
```

**DefaultAzureCredential** is convenient for development but requires careful consideration in production. In production, consider using a specific credential (e.g., **ManagedIdentityCredential**)

Benefits:

- Predictable authentication path
- Easier auditing
- Reduced risk of accidental fallback
- Clearer security reviews

![alt text](images/{85A7257A-2546-42F7-A6EB-77B7392381BC}.png)

## Why MS Agent Framework?

MS Agent Framework combines AutoGen's simple agent abstractions with Semantic Kernel's enterprise features

- Session-based state management
- Type safety
- middleware
- telemetry
- adds graph-based workflows

for explicit multi-agent orchestration

Semantic Kernel and AutoGen pioneered the concepts of AI agents and multi-agent orchestration. The MS Agent Framework is the direct successor , created by the same teams.

## Add Tools

Tools let your agent call custom functions — like fetching weather data, querying a database, or calling an API.

Define a tool as any method with a **[Description]** attribute:

Use **AIFunctionFactory.Create(func)** to instantiate the function

```
using System.ComponentModel;

[Description("Get the weather for a given location.")]
static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";
```

```
using System;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.ComponentModel;


[Description("Get the weather for a given location.")]
static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";

AIAgent agent = new AIProjectClient(
        new Uri("https://foundryallinalldev007.openai.azure.com"),
        new AzureCliCredential())
    .AsAIAgent(
        model: "gpt-5-deployment",
        instructions: "You are a helpful assistant. Keep your answers brief.",
        name: "FriendlyAssistant",
        tools: [AIFunctionFactory.Create(GetWeather)]);

Console.WriteLine(await agent.RunAsync("What is the weather in Paris, France?"));

```

**Output**

```
The weather in Paris, France is cloudy with a high of 15°C.
```

## Multi-Turn Conversations

Use a session to maintain conversation context so the agent remembers what was said earlier.

Use **AgentSession** to maintain context across multiple calls:

**Without AgentSession**

```
Console.WriteLine(await agent.RunAsync("What is the weather in Paris, France?"));

Console.WriteLine(await agent.RunAsync("Which city I asked the question about?"));
```

**Answer**

```
The weather in Paris, France is cloudy with a high of 15°C.
You didn’t ask about any specific city in this conversation.
```

**With AgentSession**

```
// Create a session to maintain conversation history
AgentSession session = await agent.CreateSessionAsync();

// First Turn
Console.WriteLine(await agent.RunAsync("What is the weather in Paris, France?", session));

// Second Turn
Console.WriteLine(await agent.RunAsync("Which city I asked the question about?", session));
```

**Answer**

```
The weather in Paris, France is cloudy with a high of 15°C.
Paris, France
```

## Memory And Persistance

For long term memory and persistance we use custom ChatHistoryProvider you can pass one to the agent options.

You create one when you want history persisted outside memory, for example:

- Redis
- PostgreSQL
- Cosmos DB
- SQL Server
- Blob Storage

| Component           | Purpose                                                             |
| ------------------- | ------------------------------------------------------------------- |
| AgentSession        | Identifies a conversation and holds session state                   |
| ChatHistoryProvider | Persists and retrieves conversation history                         |
| StateBag            | Stores provider-specific metadata (conversation IDs, DB keys, etc.) |

**A common pattern:**

```
User
  ↓
AgentSession
  ↓
CustomChatHistoryProvider
  ↓
Redis / PostgreSQL / Cosmos DB
```

The session contains:

The provider uses that key to load and save messages.

Use only Session when:

- Chat history can be lost after restart
- Internal tools or demos
- No need to resume conversations later

Use CustomChatHistoryProvider when:

- You need persistent conversations
- Users can resume previous chats
- You want audit/history storage
- Running multiple application instances
- Need Redis/PostgreSQL/Cosmos-backed memory

Lets use CosmosDB for persistence.

```
dotnet add package Microsoft.Agents.AI.CosmosNoSql --prerelease
dotnet add package Microsoft.Azure.Cosmos
```

```
using System;
using System.ComponentModel;
using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.AI;

[Description("Get the weather for a given location.")]
static string GetWeather(
    [Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";

var azureOpenAIEndpoint = "https://foundryallinalldev007.openai.azure.com";
var deploymentName = "gpt-5-deployment";

var cosmosEndpoint = "https://cosmosdb2284.documents.azure.com:443/";
var databaseId = "agentdb";
var containerId = "chatHistory";

// In real app, use stable ID from your app/session.
// Example: $"{userId}:{chatId}"
var conversationId = "user-varinder-chat-001";

var credential = new DefaultAzureCredential();

CosmosClient cosmosClient = new CosmosClient(
    accountEndpoint: cosmosEndpoint,
    tokenCredential: credential,
    new CosmosClientOptions
    {
        AllowBulkExecution = true
    });

Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);

await database.CreateContainerIfNotExistsAsync(
    id: containerId,
    partitionKeyPath: "/conversationId");

// Cosmos-backed history provider.
// Container partition key must be /conversationId.
var historyProvider = new CosmosChatHistoryProvider(
    cosmosClient: cosmosClient,
    databaseId: databaseId,
    containerId: containerId,
    stateInitializer: session =>
        new CosmosChatHistoryProvider.State(
            conversationId: conversationId));

historyProvider.MaxMessagesToRetrieve = 30;
historyProvider.MessageTtlSeconds = -1;

// Azure OpenAI chat client.
// This path does not use Foundry server-side conversation history.
AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(
    new Uri(azureOpenAIEndpoint),
    new AzureCliCredential());

IChatClient chatClient = azureOpenAIClient
    .GetChatClient(deploymentName)
    .AsIChatClient();

AIAgent agent = chatClient.AsAIAgent(new ChatClientAgentOptions
{
    Name = "FriendlyAssistant",

    // OK here, because this is framework-managed history.
    ChatHistoryProvider = historyProvider,

    ChatOptions = new ChatOptions
    {
        Instructions = "You are a helpful assistant. Keep your answers brief.",
        Tools = [AIFunctionFactory.Create(GetWeather)]
    }
});

AgentSession session = await agent.CreateSessionAsync();

Console.WriteLine("First turn:");
try
{
    Console.WriteLine(await agent.RunAsync(
        "Who is narendra modi?",
        session));
}
catch (Exception ex)
{
    Console.WriteLine(ex);

    if (ex.InnerException != null)
    {
        Console.WriteLine("INNER:");
        Console.WriteLine(ex.InnerException);
    }

    throw;
}

Console.WriteLine();
Console.WriteLine("Second turn:");
Console.WriteLine(await agent.RunAsync(
    "what are the last 3 questions I asked to you?",
    session));
```

```
  <ItemGroup>
    <PackageReference Include="Azure.AI.OpenAI" Version="2.1.0" />
    <PackageReference Include="Azure.AI.Projects" Version="2.1.0-beta.3" />
    <PackageReference Include="Azure.Identity" Version="1.21.0" />
    <PackageReference Include="Microsoft.Agents.AI.CosmosNoSql" Version="1.10.0-preview.260610.1" />
    <PackageReference Include="Microsoft.Agents.AI.Foundry" Version="1.10.0-preview.260610.1" />
    <PackageReference Include="Microsoft.Azure.Cosmos" Version="3.61.0" />
  </ItemGroup>
```

## Tools

- MS Agent Framework supports many different types of tools that extend agent capabilities.

- Tools allow agents to interact with external systems, execute code, search data, and more.

## Tool Types

![alt text](images/{01348239-9CAB-4409-B761-50936BD58ACA}.png)

## Tool Approval

It is a framework feature that lets you gate every tool invocation

```
AIFunction weatherFunction = AIFunctionFactory.Create(GetWeather);

AIFunction approvalRequiredWeatherFunction = new ApprovalRequiredAIFunction(weatherFunction);
```

```
AIAgent agent = new AIProjectClient(
    new Uri("<your-foundry-project-endpoint>"),
    new DefaultAzureCredential())
     .AsAIAgent(
        model: "gpt-4o-mini",
        instructions: "You are a helpful assistant",
        tools: [approvalRequiredWeatherFunction]);
```

## Provider Support Matrix

The OpenAI and Azure OpenAI providers each offer two client types — Responses and Chat Completion — with different tool capabilities

![alt text](images/{35A9C98E-1A9C-4065-B57D-B6A9FE84E1BC}.png)

| Tool                        | Purpose                                    |
| --------------------------- | ------------------------------------------ |
| `HostedCodeInterpreterTool` | Execute Python/code in a sandbox           |
| `HostedWebSearchTool`       | Search the public web                      |
| `HostedMcpServerTool`       | Connect to hosted MCP servers              |
| `HostedFileSearchTool`      | Search uploaded/indexed files              |
| `HostedImageGenerationTool` | Generate images                            |
| `HostedShellTool`           | Execute shell commands in a hosted runtime |

## Function Tool

**Option 1: Using direct Function Tool**

```
[Description("Gets weather for a city")]
string GetWeather(string city)


tools: [AIFunctionFactory.Create(GetWeather)]
```

```
Main Agent
    |
    +--> GetWeather()
```

**Pros**

- Simpler
- Faster (fewer LLM calls)
- Lower cost
- Easier debugging

**Best for**

- Simple functions
- CRUD operations
- Database lookups
- API calls
- Deterministic logic

**The main agent decides:**

- When to call it
- What parameters to send
- How to interpret the result

**Option 2: Using an Agent as a Function Tool**

```
User
  |
Main Agent
  |
Weather Agent
  |
GetWeather()
```

**Pros**

Encapsulation of Domain Knowledge

Weather Agent can have its own:

```
instructions:
"You are a meteorology expert..."
```

The main agent doesn't need weather expertise.

**Main Agent:**

- Doesn't know weather specifics.
- Delegates to Weather Agent.

**Weather Agent:**

- Calls GetWeather
- Interprets wind speed
- Considers rain
- Returns recommendation

Without the Weather Agent, the main agent must perform all reasoning itself.

```
// Create the inner agent with its own tools
AIAgent weatherAgent = new AIProjectClient(
    new Uri("<your-foundry-project-endpoint>"),
    new DefaultAzureCredential())
     .AsAIAgent(
        model: "gpt-4o-mini",
        instructions: "You answer questions about the weather.",
        name: "WeatherAgent",
        description: "An agent that answers questions about the weather.",
        tools: [AIFunctionFactory.Create(GetWeather)]);

// Create the main agent and provide the inner agent as a function tool
AIAgent agent = new AIProjectClient(
    new Uri("<your-foundry-project-endpoint>"),
    new DefaultAzureCredential())
     .AsAIAgent(
        model: "gpt-4o-mini",
        instructions: "You are a helpful assistant.",
        tools: [weatherAgent.AsAIFunction()]);

// The main agent can now call the weather agent as a tool
Console.WriteLine(await agent.RunAsync("What is the weather like in Amsterdam?"));
```

## How to use WebSearch Tool

Web Search allows agents to search the web for up-to-date information. This tool enables agents to answer questions about current events, find documentation, and access information beyond their training data.

```
using System;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.ComponentModel;


[Description("Get the weather for a given location.")]
static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";


var mcpHostedTool = new HostedMcpServerTool(
    serverName: "microsoft_learn",
    serverAddress: "https://learn.microsoft.com/api/mcp")
{
    AllowedTools = ["microsoft_docs_search"],
    ApprovalMode = HostedMcpServerToolApprovalMode.NeverRequire
};

// Hosted Code Interpreter Tool
var codeInterpreterTool = new HostedCodeInterpreterTool();
var webSearchTool = new HostedWebSearchTool();

AIAgent agent = new AIProjectClient(
        new Uri("https://foundryallinalldev007.openai.azure.com"),
        new AzureCliCredential())
    .AsAIAgent(
        model: "gpt-5-deployment",
        instructions: """
        You are a helpful assistant.
        You can:
        - call local C# functions,
        - use hosted code interpreter for calculations and Python-style data analysis,
        - use Microsoft Learn MCP search.
        - use web search for current and web search for information.
        """,
        name: "FriendlyAssistant",
        tools: [AIFunctionFactory.Create(GetWeather), codeInterpreterTool, mcpHostedTool, webSearchTool]);

Console.WriteLine(await agent.RunAsync("""
What is current time in Amritsar Punjab India
"""));


```

## How to use Code Interprator Tool

```
using System;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.ComponentModel;


[Description("Get the weather for a given location.")]
static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";


var mcpHostedTool = new HostedMcpServerTool(
    serverName: "microsoft_learn",
    serverAddress: "https://learn.microsoft.com/api/mcp")
{
    AllowedTools = ["microsoft_docs_search"],
    ApprovalMode = HostedMcpServerToolApprovalMode.AlwaysRequire
};

AIAgent agent = new AIProjectClient(
        new Uri("https://foundryallinalldev007.openai.azure.com"),
        new AzureCliCredential())
    .AsAIAgent(
        model: "gpt-5-deployment",
        instructions: """
        You are a helpful assistant.
        You can:
        - call local C# functions,
        - use hosted code interpreter for calculations and Python-style data analysis.
        """,
        name: "FriendlyAssistant",
        tools: [AIFunctionFactory.Create(GetWeather), new HostedCodeInterpreterTool()]);

Console.WriteLine(await agent.RunAsync("""
Calculate the first 1000 prime numbers and provide:
- count
- largest prime
- average value
- execution details"
"""));

```

## How to use the Model Context Protocol tool

You can extend the capabilities of your Microsoft Foundry agent by connecting it to tools hosted on remote Model Context Protocol (MCP) servers.

```
using System;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.ComponentModel;


[Description("Get the weather for a given location.")]
static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";


var mcpHostedTool = new HostedMcpServerTool(
    serverName: "microsoft_learn",
    serverAddress: "https://learn.microsoft.com/api/mcp")
{
    AllowedTools = ["microsoft_docs_search"],
    ApprovalMode = HostedMcpServerToolApprovalMode.NeverRequire
};

// Hosted Code Interpreter Tool
var codeInterpreterTool = new HostedCodeInterpreterTool();

AIAgent agent = new AIProjectClient(
        new Uri("https://foundryallinalldev007.openai.azure.com"),
        new AzureCliCredential())
    .AsAIAgent(
        model: "gpt-5-deployment",
        instructions: """
        You are a helpful assistant.
        You can:
        - call local C# functions,
        - use hosted code interpreter for calculations and Python-style data analysis,
        - use Microsoft Learn MCP search.
        """,
        name: "FriendlyAssistant",
        tools: [AIFunctionFactory.Create(GetWeather), codeInterpreterTool, mcpHostedTool]);

Console.WriteLine(await agent.RunAsync("""
Use the microsoft_learn MCP tool, specifically microsoft_docs_search.

Search Microsoft Learn for:
"HostedMcpServerTool Agent Framework AllowedTools ApprovalMode"

Return only information found by the MCP search.

For validation, include:
1. The exact MCP tool name used.
2. The top 3 Microsoft Learn result titles.
3. The Microsoft Learn URLs.
4. A short summary of each result.
5. Say "NO MCP RESULT" if you cannot call the MCP tool.
"""));

```

#

#

#

An Enterprise Application with Multi-Agent using

- MS Agent Framework
- MCP
- Dotnet

![alt text](images/{90681AC8-BEEC-44E3-A139-97C4ABA5F578}.png)

| Area           | OpenAI Client / Agents SDK                         | Microsoft Agent Framework                             |
| -------------- | -------------------------------------------------- | ----------------------------------------------------- |
| Best for       | Direct OpenAI apps, fast prototypes, custom agents | Enterprise Azure/M365 agent systems                   |
| Ecosystem      | OpenAI API, Responses API, tools, agents           | Azure AI Foundry, Microsoft 365, .NET, Python         |
| Enterprise fit | Good, but you design more governance yourself      | Stronger if company already uses Azure/Microsoft      |
| Orchestration  | Lightweight, developer-controlled                  | More structured workflows, multi-agent, checkpointing |
| Model support  | OpenAI-first                                       | Multi-model / Azure-friendly                          |
| Complexity     | Simpler                                            | Heavier but more enterprise-ready                     |
| Vendor lock-in | OpenAI-centered                                    | Microsoft/Azure-centered                              |

In short: create agents directly in Foundry for simple managed agents; use MFA when enterprise logic and orchestration become complex.

**Example**

For a simple HR FAQ bot:

- Use Azure AI Foundry Create Agent.

For an onboarding process like:

- Read employee data
- Validate documents
- Call HR API
- Ask manager approval
- Create ServiceNow ticket
- Send email
- Track state for days

```

```

## Vector Embeddings

- Vector Embeddings are a way of representing words, sentences, images, or other data as numerical vectors in a high-diamensional space.
- These vector embeddings are generated by text-embedding models thare are offereed by Azure OpenAI
- These vectors capture the sementic or contextual information of the data, allowing similar items to be closer together in the space.
- 1536 dimentions

## RAG (Retrieval Augmentation Generation)

- When you want to use LLM against enterprise data without trainiing the LLM over your enterprise data.

![alt text](images/{19FCE695-0BD5-4402-BF21-E9568EC8500F}.png)
