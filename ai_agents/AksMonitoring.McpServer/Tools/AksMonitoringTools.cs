using System.ComponentModel;
using Azure.Monitor.Query;
using Azure.Monitor.Query.Models;
using ModelContextProtocol.Server;

namespace AksMonitoring.McpServer.Tools;

[McpServerToolType]
public sealed class AksMonitoringTools
{
    private readonly LogsQueryClient _logsClient;
    private readonly IConfiguration _configuration;

    public AksMonitoringTools(
        LogsQueryClient logsClient,
        IConfiguration configuration)
    {
        _logsClient = logsClient;
        _configuration = configuration;
    }

    [McpServerTool]
    [Description("Gets recent AKS pod error logs from a namespace.")]
    public async Task<IReadOnlyList<Dictionary<string, object?>>> GetRecentPodErrors(
        string namespaceName = "DefaultWorkspace-c29c4842-c87b-4916-8841-525820e6ad23-SEC",
        int minutes = 1500,
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        var query = $"""
        ContainerLogV2
        | where TimeGenerated > ago({minutes}m)
        | where PodNamespace == "{EscapeKql(namespaceName)}"
        | where LogMessage has_any ("error", "exception", "failed", "timeout", "panic")
        | project TimeGenerated, PodNamespace, PodName, ContainerName, LogMessage
        | order by TimeGenerated desc
        | take {limit}
        """;

        return await QueryAsync(query, minutes, cancellationToken);
    }

    [McpServerTool]
    [Description("Finds AKS pods with container restarts.")]
    public async Task<IReadOnlyList<Dictionary<string, object?>>> GetPodRestarts(
        string? namespaceName = "DefaultWorkspace-c29c4842-c87b-4916-8841-525820e6ad23-SEC",
        int minutes = 60,
        CancellationToken cancellationToken = default)
    {
        var namespaceFilter = "apps";

        var query = $"""
        KubePodInventory
        | where TimeGenerated > ago({minutes}m)
        {namespaceFilter}
        | summarize RestartCount=max(ContainerRestartCount), Latest=max(TimeGenerated)
            by Namespace, Name, ContainerName
        | where RestartCount > 0
        | order by RestartCount desc
        | take 50
        """;

        return await QueryAsync(query, minutes, cancellationToken);
    }

    [McpServerTool]
    [Description("Gets Kubernetes warning events for AKS.")]
    public async Task<IReadOnlyList<Dictionary<string, object?>>> GetKubernetesWarnings(
        string? namespaceName = "DefaultWorkspace-c29c4842-c87b-4916-8841-525820e6ad23-SEC",
        int minutes = 60,
        CancellationToken cancellationToken = default)
    {
        var namespaceFilter = string.IsNullOrWhiteSpace(namespaceName)
            ? ""
            : $"""| where Namespace == "{EscapeKql(namespaceName)}" """;

        var query = $"""
        KubeEvents
        | where TimeGenerated > ago({minutes}m)
        {namespaceFilter}
        | where EventType == "Warning"
        | project TimeGenerated, Namespace, Name, Reason, Message
        | order by TimeGenerated desc
        | take 100
        """;

        return await QueryAsync(query, minutes, cancellationToken);
    }

    [McpServerTool]
    [Description("Shows AKS node CPU and memory pressure.")]
    public async Task<IReadOnlyList<Dictionary<string, object?>>> GetNodePressure(
        int minutes = 60,
        CancellationToken cancellationToken = default)
    {
        var query = $"""
        Perf
        | where TimeGenerated > ago({minutes}m)
        | where ObjectName == "K8SNode"
        | where CounterName in ("cpuUsagePercentage", "memoryRssPercentage")
        | summarize AvgValue=avg(CounterValue), MaxValue=max(CounterValue)
            by Computer, CounterName
        | order by MaxValue desc
        """;

        return await QueryAsync(query, minutes, cancellationToken);
    }

    [McpServerTool]
    [Description("Runs a safe read-only KQL query against the AKS Log Analytics workspace.")]
    public async Task<IReadOnlyList<Dictionary<string, object?>>> RunSafeKql(
        string query,
        int minutes = 60,
        CancellationToken cancellationToken = default)
    {
        var blocked = new[]
        {
            ".delete", ".drop", ".set", ".append", ".ingest",
            "externaldata", "evaluate python", "evaluate r"
        };

        var lowered = query.ToLowerInvariant();

        if (blocked.Any(lowered.Contains))
        {
            throw new InvalidOperationException("Unsafe KQL command blocked.");
        }

        return await QueryAsync(query, minutes, cancellationToken);
    }

    private async Task<IReadOnlyList<Dictionary<string, object?>>> QueryAsync(
        string query,
        int minutes,
        CancellationToken cancellationToken)
    {
        var workspaceId = _configuration["AzureMonitor:WorkspaceId"];

        if (string.IsNullOrWhiteSpace(workspaceId))
        {
            throw new InvalidOperationException("AzureMonitor:WorkspaceId is not configured.");
        }

        var response = await _logsClient.QueryWorkspaceAsync(
            workspaceId,
            query,
            new QueryTimeRange(TimeSpan.FromMinutes(minutes)),
            cancellationToken: cancellationToken);

        var table = response.Value.Table;

        return table.Rows.Select(row =>
        {
            var item = new Dictionary<string, object?>();

            for (var i = 0; i < table.Columns.Count; i++)
            {
                item[table.Columns[i].Name] = row[i];
            }

            return item;
        }).ToList();
    }

    private static string EscapeKql(string value)
    {
        return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}