using CostManagement.McpServer.Tools;
using ModelContextProtocol.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<CostManagementTools>();

var app = builder.Build();

app.MapGet("/", () => "Cost Management MCP Server is running.");

app.MapMcp("/mcp");

app.Run();