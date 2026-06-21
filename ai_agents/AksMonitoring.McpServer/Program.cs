using AksMonitoring.McpServer.Tools;
using Azure.Identity;
using Azure.Monitor.Query;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(
    new LogsQueryClient(new DefaultAzureCredential()));

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<AksMonitoringTools>();

var app = builder.Build();

app.MapGet("/", () => "AKS Monitoring MCP Server is running.");

app.MapMcp("/mcp");

app.Run("http://localhost:5070");