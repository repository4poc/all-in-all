using AksMonitoring.McpServer.Tools;
using Azure.Identity;
using Azure.Monitor.Query;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<LogsQueryClient>(_ =>
    new LogsQueryClient(new DefaultAzureCredential()));
builder.WebHost.UseUrls("http://0.0.0.0:5070");


builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithTools<AksMonitoringTools>();

var app = builder.Build();

app.MapGet("/", () => "AKS Monitoring MCP Server is running.");

app.MapMcp("/mcp");

app.Run();