using CostManagement.McpServer.Tools;

var builder = WebApplication.CreateBuilder(args);
// Listen on port 5005
builder.WebHost.UseUrls("http://0.0.0.0:5080");

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<CostManagementTools>();

var app = builder.Build();

app.MapGet("/", () => "Cost Management MCP Server is running.");

app.MapMcp("/mcp");

app.Run();