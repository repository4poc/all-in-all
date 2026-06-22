using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.DevUI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Extensions.AI;
using System.ComponentModel;
using ModelContextProtocol.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenAIResponses();
builder.Services.AddOpenAIConversations();
builder.Services.AddDevUI();

var endpoint =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? builder.Configuration["AzureOpenAI:Endpoint"]
    ?? throw new InvalidOperationException("AzureOpenAI endpoint missing.");

var deploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? builder.Configuration["AzureOpenAI:DeploymentName"]
    ?? throw new InvalidOperationException("AzureOpenAI deployment missing.");

var credential = new AzureCliCredential();

var azureOpenAIClient = new AzureOpenAIClient(new Uri(endpoint), credential);

var chatClient = azureOpenAIClient
    .GetChatClient(deploymentName)
    .AsIChatClient();

builder.Services.AddChatClient(chatClient);

[Description("Get the weather for a given location.")]
static string GetWeather(
    [Description("The city or location.")] string location)
{
    return $"The weather in {location} is cloudy with a high of 15°C.";
}

var costManagementMcpEndpoint =
    builder.Configuration["McpServers:AzureCost:Endpoint"]
    ?? "http://localhost:5080/mcp";

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

var allTools = new List<AITool>();

allTools.Add(AIFunctionFactory.Create(GetWeather));
allTools.AddRange(azureCostMcpTools);

foreach (var tool in azureCostMcpTools)
{
    Console.WriteLine($"Cost MCP Tool: {tool.Name}");
}

builder.AddAIAgent(
    "FriendlyAssistant",
    """
    You are a helpful assistant.
    Use the weather tool when the user asks about weather.
    Use the Azure Cost MCP tools when the user asks about Azure cost.
    """
)
.WithAITools(allTools.ToArray());

var app = builder.Build();

app.MapOpenAIResponses();
app.MapOpenAIConversations();

if (app.Environment.IsDevelopment())
{
    app.MapDevUI();
}

app.MapGet("/", () => Results.Redirect("/devui"));

app.Run("http://localhost:5050");