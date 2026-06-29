using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting.AGUI.AspNetCore;
using Microsoft.Extensions.AI;
using Orchestrator.Api.Models;
using Orchestrator.Api.Services;
using System.ComponentModel;
using OpenAI.Responses;
using Microsoft.Azure.Cosmos;
using ModelContextProtocol.Client;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var orchestratorActivitySource = new ActivitySource("Orchestrator.Api");

var enableOtel =
    builder.Configuration.GetValue<bool>("Observability:OpenTelemetry:Enabled");

if (enableOtel)
{
    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource =>
        {
            resource.AddService(
                serviceName: builder.Configuration["OTEL_SERVICE_NAME"]
                    ?? builder.Environment.ApplicationName);
        })
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("Orchestrator.Api")
                .AddSource("Microsoft.Extensions.AI")
                .AddOtlpExporter();
        })
        .WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddOtlpExporter();
        });

    builder.Logging.AddOpenTelemetry(logging =>
    {
        logging.IncludeFormattedMessage = true;
        logging.IncludeScopes = true;
    });
}

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

builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Services.AddAGUI();

builder.Services.AddSingleton<RemoteAgentClient>();
builder.Services.AddSingleton<PiiRedactionService>();
builder.Services.AddSingleton<WeatherTools>();
builder.Services.AddHttpContextAccessor();

var credential = new DefaultAzureCredential();

var endpoint =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? builder.Configuration["AzureOpenAI:Endpoint"]
    ?? throw new InvalidOperationException("AzureOpenAI endpoint missing.");

var deploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? builder.Configuration["AzureOpenAI:DeploymentName"]
    ?? throw new InvalidOperationException("AzureOpenAI deployment missing.");

var costManagementMcpEndpoint =
    builder.Configuration["McpServers:AzureCost:Endpoint"]
    ?? "http://costmanagementmcp:5080/mcp";

var aksMonitoringMcpEndpoint =
    builder.Configuration["McpServers:AzureAKS:Endpoint"]
    ?? "http://aksmonitoringmcp:5070/mcp";

var meetingAnalyserAgentBaseUrl =
    builder.Configuration["Agents:MeetingAnalyserAgentBaseUrl"]
    ?? "http://meetinganalyser:5005";

var documentManagerAgentBaseUrl =
    builder.Configuration["Agents:DocumentManagerAgentBaseUrl"]
    ?? "http://documentmanager:5010";

var cosmosEndpoint =
    builder.Configuration["CosmosDb:Endpoint"]
    ?? "https://cosmosdb2284.documents.azure.com:443/";

var databaseId =
    builder.Configuration["CosmosDb:DatabaseId"]
    ?? "agentdb";

var containerId =
    builder.Configuration["CosmosDb:ContainerId"]
    ?? "chatHistory";

var conversationId = "user-varinder-chat-006";

builder.Services.AddSingleton(_ =>
    new CosmosClient(
        accountEndpoint: cosmosEndpoint,
        tokenCredential: credential,
        clientOptions: new CosmosClientOptions
        {
            AllowBulkExecution = true
        }));

var app = builder.Build();

var logger = app.Services
    .GetRequiredService<ILoggerFactory>()
    .CreateLogger("Startup");

var remoteAgentClient = app.Services.GetRequiredService<RemoteAgentClient>();
var piiGuard = app.Services.GetRequiredService<PiiRedactionService>();
var weatherTools = app.Services.GetRequiredService<WeatherTools>();
var cosmosClient = app.Services.GetRequiredService<CosmosClient>();

/*
 * Optional Cosmos initialization.
 * Best practice: create database/container using IaC or deployment scripts.
 * This keeps local startup resilient.
 */
try
{
    var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);

    await database.Database.CreateContainerIfNotExistsAsync(
        id: containerId,
        partitionKeyPath: "/conversationId");

    logger.LogInformation("Cosmos DB initialized successfully.");
}
catch (Exception ex)
{
    logger.LogWarning(ex, "Cosmos DB initialization failed. API will continue starting.");
}

var historyProvider = new CosmosChatHistoryProvider(
    cosmosClient: cosmosClient,
    databaseId: databaseId,
    containerId: containerId,
    stateInitializer: session =>
        new CosmosChatHistoryProvider.State(
            conversationId: conversationId));

historyProvider.MaxMessagesToRetrieve = 30;
historyProvider.MessageTtlSeconds = 120;

[Description("Get the weather for a given location.")]
async Task<string> GetWeather(
    [Description("The location to get the weather for.")] string location)
{
    var results = await weatherTools.GetWeather(location);
    return results;
}

[Description("Discovers and calls the MeetingAnalyser agent. Returns extracted meeting details as raw JSON. Do not summarize or rewrite the response.")]
async Task<string> AskMeetingAnalyserAgentAsync(
    [Description("The meeting transcript. Total length should be less than 500 characters")] string question)
{
    var safeQuestion = await piiGuard.RedactAsync(question);

    return await remoteAgentClient.AskAgentAsync(
        meetingAnalyserAgentBaseUrl,
        safeQuestion);
}

[Description("Discovers and calls the Document Manager agent. Use this for document, contract, invoice, compliance, metadata, and workflow questions.")]
async Task<string> AskDocumentManagerAgentAsync(
    [Description("The user's document management question.")] string question)
{
    return await remoteAgentClient.AskAgentAsync(
        documentManagerAgentBaseUrl,
        question);
}

var tools = new List<AITool>();

tools.Add(AIFunctionFactory.Create(GetWeather));
tools.Add(new HostedCodeInterpreterTool());

await AddMcpToolsIfAvailableAsync(
    tools,
    costManagementMcpEndpoint,
    "Cost MCP",
    logger,
    orchestratorActivitySource);

await AddMcpToolsIfAvailableAsync(
    tools,
    aksMonitoringMcpEndpoint,
    "AKS MCP",
    logger);

tools.Add(AIFunctionFactory.Create(AskMeetingAnalyserAgentAsync));
tools.Add(AIFunctionFactory.Create(AskDocumentManagerAgentAsync));

var azureOpenAIClient = new AzureOpenAIClient(
    new Uri(endpoint),
    credential);

var rawChatClient = azureOpenAIClient
    .GetChatClient(deploymentName)
    .AsIChatClient();

//var chatClient = rawChatClient;

var chatClient = new ChatClientBuilder(rawChatClient)
    .UseOpenTelemetry(
        loggerFactory: app.Services.GetRequiredService<ILoggerFactory>(),
        sourceName: "Microsoft.Extensions.AI",
        configure: c => c.EnableSensitiveData = true)
    .Build();

AIAgent agent = chatClient.AsAIAgent(new ChatClientAgentOptions
{
    Name = "FriendlyAssistant",
    ChatHistoryProvider = historyProvider,

    ChatOptions = new ChatOptions
    {
        Instructions = """
        You are a helpful assistant.

        For Azure Cost queries, use the local MCP tool named get_daily_cost_trend.
        Do not answer Azure Cost daily trend questions from memory.
        Call the MCP tool directly.

        Use AKS MCP tools to investigate AKS incidents.

        Start with read-only investigation:
        - Check pod errors.
        - Check pod restarts.
        - Check Kubernetes warning events.
        - Check node CPU and memory pressure.

        Rules:
        - Do not perform destructive actions.
        - Do not restart, scale, delete, cordon, or drain anything automatically.
        - Recommend remediation steps, but require human approval.

        Use the DocumentManager agent for document search, contracts, invoices, policies,
        compliance documents, metadata, and workflow state.

        Before answering document questions, call the DocumentManager agent.

        Use the MeetingAnalyser agent only after user approval when transcript content is provided.
        """,
        Tools = tools
    }
});

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
        Endpoint = "/agui/support",
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

app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    timestamp = DateTime.UtcNow
}));

app.Run("http://0.0.0.0:5000");


static async Task AddMcpToolsIfAvailableAsync(
    List<AITool> tools,
    string endpoint,
    string name,
    ILogger logger,
    ActivitySource? activitySource = null)
{
    Activity? activity = null;
    if (activitySource != null)
    {
        logger.LogWarning("ActivitySource is null. Skipping OpenTelemetry activity creation for {McpName}.", name);
        activity = activitySource.StartActivity(
$"mcp.discovery.{name}",
ActivityKind.Client);

        activity?.SetTag("mcp.name", name);
        activity?.SetTag("mcp.endpoint", endpoint);
    }

    try
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

        var transport = new HttpClientTransport(new HttpClientTransportOptions
        {
            Endpoint = new Uri(endpoint),
            TransportMode = HttpTransportMode.StreamableHttp,
            ConnectionTimeout = TimeSpan.FromSeconds(3)
        });

        var client = await McpClient.CreateAsync(transport, cancellationToken: cts.Token);
        var mcpTools = await client.ListToolsAsync(cancellationToken: cts.Token);

        foreach (var tool in mcpTools)
        {
            logger.LogInformation("{McpName} Tool discovered: {ToolName}", name, tool.Name);
        }

        tools.AddRange(mcpTools);

        if (activitySource != null)
        {
            activity?.SetTag("mcp.tool.count", mcpTools.Count);
            activity?.SetStatus(ActivityStatusCode.Ok);
        }

        logger.LogInformation("{McpName} connected successfully. Tool count: {Count}", name, mcpTools.Count);
    }
    catch (Exception ex)
    {
        logger.LogWarning(
            "{McpName} unavailable at {Endpoint}. Continuing without MCP tools. Reason: {Message}",
            name,
            endpoint,
            ex.Message);
        if (activitySource != null)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity?.RecordException(ex);
        }
    }
}