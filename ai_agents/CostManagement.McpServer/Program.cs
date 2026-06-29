using CostManagement.McpServer.Tools;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
// Listen on port 5005
builder.WebHost.UseUrls("http://0.0.0.0:5080");


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
                .AddSource("CostManagement.McpServer")
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

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<CostManagementTools>();

var app = builder.Build();

app.MapGet("/", () => "Cost Management MCP Server is running.");

app.MapMcp("/mcp");

app.Run();