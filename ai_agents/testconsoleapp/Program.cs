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
var conversationId = "user-varinder-chat-002";

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
historyProvider.MessageTtlSeconds = 120;

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
        "Who is the weather in Paris France?",
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