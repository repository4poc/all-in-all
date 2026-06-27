using Azure.AI.OpenAI;
using Azure.Identity;
//MS Foundery Agent SDK includes both the core agent framework and the OpenAI client, 
//so you only need to reference Microsoft.Agents.AI.Foundry
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting.AGUI.AspNetCore;
using Microsoft.Extensions.AI;
using Orchestrator.Api.Models;
using Orchestrator.Api.Services;
using System.ComponentModel;
using OpenAI.Responses;
//Azure SDK used to connect your application to an Azure AI Foundry Project.
using Azure.AI.Projects;
using Microsoft.Azure.Cosmos;
using ModelContextProtocol.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Register AG-UI Services
builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Services.AddAGUI();

builder.Services.AddSingleton<RemoteAgentClient>();
builder.Services.AddSingleton<PiiRedactionService>();
builder.Services.AddSingleton<WeatherTools>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var endpoint =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? builder.Configuration["AzureOpenAI:Endpoint"]
    ?? throw new InvalidOperationException("AzureOpenAI endpoint missing.");

var deploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? builder.Configuration["AzureOpenAI:DeploymentName"]
    ?? throw new InvalidOperationException("AzureOpenAI deployment missing.");
// Build the tools list and add the native Code Interpreter ResponseTool via the AITool bridge extension

var remoteAgentClient = app.Services.GetRequiredService<RemoteAgentClient>();

var costManagementMcpEndpoint =
    builder.Configuration["McpServers:AzureCost:Endpoint"]
    ?? "http://localhost:5080/mcp";

var AksMonitoringMcpEndpoint =
    builder.Configuration["McpServers:AzureAKS:Endpoint"]
    ?? "http://localhost:5070/mcp";

var meetingAnalyserAgentBaseUrl =
    builder.Configuration["Agents:MeetingAnalyserAgentBaseUrl"]
    ?? "http://localhost:5005";

var documentManagerAgentBaseUrl =
    builder.Configuration["Agents:DocumentManagerAgentBaseUrl"]
    ?? "http://localhost:5010";

/*
* Persistant State Management in CosmosDB
*/
var cosmosEndpoint = "https://cosmosdb2284.documents.azure.com:443/";
var databaseId = "agentdb";
var containerId = "chatHistory";

// In real app, use stable ID from your app/session.
// Example: $"{userId}:{chatId}"
var conversationId = "user-varinder-chat-006";
var credential = new AzureCliCredential();

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

var piiGuard = app.Services.GetRequiredService<PiiRedactionService>();
var weatherTools = app.Services.GetRequiredService<WeatherTools>();


// Cosmos-backed history provider.
// Container partition key must be /conversationId.
/*
var historyProvider = new CosmosChatHistoryProvider(
    cosmosClient: cosmosClient,
    databaseId: databaseId,
    containerId: containerId,
    stateInitializer: session =>
        new CosmosChatHistoryProvider.State(
            conversationId: conversationId),
    storeInputRequestMessageFilter: messages =>
    {
        return messages.Select(message =>
        {
            if (message.Role != ChatRole.User)
                return message;

            var redactedText = piiGuard
                .RedactAsync(message.Text)
                .GetAwaiter()
                .GetResult();

            Console.WriteLine("===== COSMOS STORE FILTER =====");
            Console.WriteLine($"Before Cosmos: {message.Text}");
            Console.WriteLine($"After Cosmos: {redactedText}");
            Console.WriteLine("===============================");

            return new ChatMessage(ChatRole.User, redactedText);
        });
    });*/

var historyProvider = new CosmosChatHistoryProvider(
    cosmosClient: cosmosClient,
    databaseId: databaseId,
    containerId: containerId,
    stateInitializer: session =>
        new CosmosChatHistoryProvider.State(
            conversationId: conversationId)
    );

historyProvider.MaxMessagesToRetrieve = 30;
historyProvider.MessageTtlSeconds = 120;

/**
Function as Tools
**/
[Description("Get the weather for a given location.")]
async Task<string> GetWeather(
    [Description("The location to get the weather for.")] string location)
{
    Console.WriteLine($"GetWeather called with location: {location}");
    var results = await weatherTools.GetWeather(location);
    Console.WriteLine($"GetWeather returned: {results}");
    return results;
}
/**
Function as Tools
**/
[Description("Discovers and calls the MeetingAnalyser agent. Returns extracted meeting details as raw JSON. Do not summarize or rewrite the response.")]
async Task<string> AskMeetingAnalyserAgentAsync(
    [Description("The meeting trascript. Total length shoule be less than 500 characters")] string question)
{
    var safeQuestion = await piiGuard.RedactAsync(question);

    var result = await remoteAgentClient.AskAgentAsync(
     meetingAnalyserAgentBaseUrl,
     safeQuestion);
    return result;
}

[Description("Discovers and calls the Document Manager agent. Use this for document, contract, invoice, compliance, metadata, and workflow questions.")]
async Task<string> AskDocumentManagerAgentAsync(
    [Description("The user's document management question.")] string question)
{
    Console.WriteLine($"Asking DocumentManager agent: {question}");

    //var safeQuestion = await piiGuard.RedactAsync(question);


    var result = await remoteAgentClient.AskAgentAsync(
        documentManagerAgentBaseUrl,
        question);

    return result;
}

List<AITool> tools = [];

/*
* GetWeatherTool
*/
tools.Add(AIFunctionFactory.Create(GetWeather));

/*
* CodeInterpractorTool
*/
var codeInterpreterTool = new HostedCodeInterpreterTool();
tools.Add(codeInterpreterTool);

/*
* Cost Management MCP Tool
*/
var mcpTransport = new HttpClientTransport(new HttpClientTransportOptions
{
    Endpoint = new Uri(costManagementMcpEndpoint), // http://localhost:5080/mcp
    TransportMode = HttpTransportMode.StreamableHttp,
    ConnectionTimeout = TimeSpan.FromSeconds(30)
});
var azureCostMcpClient = await McpClient.CreateAsync(mcpTransport);
var azureCostMcpTools = await azureCostMcpClient.ListToolsAsync();
foreach (var tool in azureCostMcpTools)
{
    Console.WriteLine($"Cost MCP Tool: {tool.Name}");
}
tools.AddRange(azureCostMcpTools);


/*
* AKS Monitoring MCP Tool
*/
var aksmcpTransport = new HttpClientTransport(new HttpClientTransportOptions
{
    Endpoint = new Uri(AksMonitoringMcpEndpoint), // http://localhost:5070/mcp
    TransportMode = HttpTransportMode.StreamableHttp,
    ConnectionTimeout = TimeSpan.FromSeconds(30)
});
var azureAksMcpClient = await McpClient.CreateAsync(aksmcpTransport);
var azureAksMcpTools = await azureAksMcpClient.ListToolsAsync();
foreach (var tool in azureAksMcpTools)
{
    Console.WriteLine($"AKS MCP Tool: {tool.Name}");
}
tools.AddRange(azureAksMcpTools);

/*
MeetingAnalyserTool
*/
var meetingAnalyserTool = AIFunctionFactory.Create(AskMeetingAnalyserAgentAsync);
tools.Add(meetingAnalyserTool);

/*
DocumentManagerTool
*/
var documentManagerTool = AIFunctionFactory.Create(AskDocumentManagerAgentAsync);
tools.Add(documentManagerTool);


AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(
    new Uri(endpoint), credential);

//var chatClient = azureOpenAIClient.GetChatClient(deploymentName).AsIChatClient();

var rawChatClient = azureOpenAIClient.GetChatClient(deploymentName)
    .AsIChatClient();

var chatClient = rawChatClient;//new PiiGuardChatClient(rawChatClient, piiGuard);


AIAgent agent = chatClient.AsAIAgent(new ChatClientAgentOptions
{
    Name = "FriendlyAssistant",
    ChatHistoryProvider = historyProvider,

    ChatOptions = new ChatOptions
    {
        Instructions = """
        You are a helpful assistant.

        PII policy:
        - Hotel names, city names, business names, and public place names are not PII.
        - The redaction layer must preserve hotel names, city names, business names, and public place names.
        - User messages may contain redacted values.
        - If a value is already redacted, do not reconstruct it.
        - However, do not ask the user to reveal it. Instead, continue using the available non-redacted context.

        For Azure Cost queries, use the local MCP tool named get_daily_cost_trend.
        Do not answer Azure Cost daily trend questions from memory.
        Call the MCP tool directly.
        If the MCP tool returns JSON, summarize it clearly unless the user asks for raw JSON.

        Use AKS MCP tools to investigate AKS incidents.

        Start with read-only investigation:
        - Check pod errors.
        - Check pod restarts.
        - Check Kubernetes warning events.
        - Check node CPU and memory pressure.
        - Use safe KQL when needed.

        Rules:
        - Do not perform destructive actions.
        - Do not restart, scale, delete, cordon, or drain anything automatically.
        - Summarize affected namespace, pod, container, time window, and likely root cause.
        - Recommend remediation steps, but require human approval.   

        Use the DocumentManager agent for:

        - document search 
        - metadata-driven document lookup
        - contract
        - invoices
        - policies- compliance documents
        - document workflow state
        Before answering document questions, call the DocumentManager agent
        Do not answer document-management questions from memory. 
        
        For document-management answers returned to the client:
        - Always call DocumentManager first.
        - Preserve the factual answer from DocumentManager.
        - Do not convert content questions into metadata-only questions.
        - Do not say metadata is missing unless DocumentManager explicitly says the user asked about metadata.
        - Do not use markdown tables.
        - Format the final response as bullet points only.

        "project": "",
        "department": "",
        "blobURL": "",
        "status": "",
        "securityClass": "",        
      

        Use Code Interprator tools to .

        - call or generate C# or python functions,
        - use hosted code interpreter for calculations and Python-style data analysis,
        - use Microsoft Learn MCP search.

        Use the Code Interpreter tool only when:
        - the user asks for calculations
        - the user asks to analyse CSV/data/table content
        - the user asks to generate charts
        - the user asks to transform or compute structured data
        - the answer requires executing code

        Use the MeetingAnalyser agent for:
        Before calling the MeetingAnalyser tool, approval is required.
        When the user provides a transcript, prepare the tool call.
        The user must approve before the transcript is sent to the MeetingAnalyser agent
        Extract meeting details from the transcript.

        IMPORTANT:
        When using the MeetingAnalyser agent.
        Summarize it.
        Convert it to readable text.
        Add headings, markdown, or explanations.

        If specifically asked to repond in markdown, use bullet points and tables to present the MeetingAnalyser response instead of JSON.

        For MeetingAnalyser responses, the final answer must be 
        only this JSON shape if send to another Agent 
        or use bullet points and table if send back to the client:

        "topic": "",
        "date": "",
        "duration": "",
        "attendees": [],
        "actionItems": [
            {
                "owner": "",
                "action": "",
                "due": "2026-05-24"
            }
        ],
        "sentiment": ""
        """,
        Tools = tools
    }
});

//AgentSession session = await agent.CreateSessionAsync();
//Console.WriteLine(session.StateBag.ToString());

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/.well-known/agent.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "OrchestratorAgent",
        Description = "Main AG-UI enterprise support orchestrator. Routes user questions to specialist agents.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5000/agui/support",
        Capabilities =
        [
            "orchestration",
            "agent-discovery",
            "rag-routing",
            "github-routing",
            "openapi-routing",
            "petstore-routing",
            "ag-ui"
        ]
    });
});


app.MapAGUI("/agui/support", agent);

app.Run("http://localhost:5000");