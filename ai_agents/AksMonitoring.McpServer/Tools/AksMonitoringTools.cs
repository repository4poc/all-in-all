using System.ComponentModel;
using Azure.Monitor.Query;
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

    [McpServerTool(Name = "get_container_log_v2_errors")]
    [Description("Returns only the count of error logs from ContainerLogV2 in Azure Log Analytics for the last 7 days. Use this when the user asks how many AKS/container/pod errors occurred in the last 7 days.")]
    public async Task<IReadOnlyList<Dictionary<string, object?>>> GetContainerLogV2Errors(
    string? namespaceName = "DefaultWorkspace-c29c4842-c87b-4916-8841-525820e6ad23-SEC")
    {
        Console.WriteLine($"GetContainerLogV2Errors called =========: {namespaceName}");
        var namespaceFilter = string.IsNullOrWhiteSpace(namespaceName)
            ? ""
            : $"""| where PodNamespace == "{EscapeKql(namespaceName)}" """;

        var query = $"""
            ContainerLogV2
                | where TimeGenerated > ago(7d)
                | where LogLevel =~ "error"
                    or LogMessage has_any ("error", "exception", "failed", "failure")
                | summarize ErrorCount = count()
            """;

        var result = await QueryAsync(query, minutes: 7 * 24 * 60, CancellationToken.None);

        var errorCount = result.Count > 0 && result[0].TryGetValue("ErrorCount", out var value)
            ? value
            : 0;

        Console.WriteLine($"GetContainerLogV2ErrorCountLast7Days returned ========= {errorCount} errors.");

        return result;
    }


    [McpServerTool(Name = "get_pod_restarts_last_7_days")]
    [Description("Returns pods with container restarts in the last 7 days.")]
    public Task<IReadOnlyList<Dictionary<string, object?>>> GetPodRestartsLast7Days(
        string? namespaceName = "null")
    {
        var namespaceFilter = string.IsNullOrWhiteSpace(namespaceName)
            ? ""
            : $"""| where Namespace == "{EscapeKql(namespaceName)}" """;

        var query = $"""
        KubePodInventory
        | where TimeGenerated > ago(7d)
        | summarize Restarts = max(ContainerRestartCount)
            by PodName = Name, Namespace, ContainerName
        | where Restarts > 0
        | order by Restarts desc
        """;

        return QueryAsync(query, 7 * 24 * 60, CancellationToken.None);
    }



    [McpServerTool(Name = "get_failed_or_warning_kubernetes_events")]
    [Description("Returns warning or failed Kubernetes events from AKS in the last 7 days.")]
    public Task<IReadOnlyList<Dictionary<string, object?>>> GetFailedOrWarningKubernetesEvents(
        string? namespaceName = "apps")
    {
        var namespaceFilter = string.IsNullOrWhiteSpace(namespaceName)
            ? ""
            : $"""| where Namespace == "{EscapeKql(namespaceName)}" """;

        var query = $"""
        KubeEvents
        | where TimeGenerated > ago(7d)
        | where Reason has_any ("Failed", "BackOff", "Unhealthy", "Killing", "Evicted")
            or Message has_any ("failed", "error", "back-off", "unhealthy", "evicted")
        | summarize EventCount=count(), LastSeen=max(TimeGenerated)
            by Namespace, Name, Reason, Message
        | order by LastSeen desc
        """;

        return QueryAsync(query, 7 * 24 * 60, CancellationToken.None);
    }


    [McpServerTool(Name = "get_recent_pod_errors")]
    [Description("Returns recent AKS container error log messages from the last 24 hours.")]
    public Task<IReadOnlyList<Dictionary<string, object?>>> GetRecentPodErrors(
        string? namespaceName = "apps")
    {
        var namespaceFilter = string.IsNullOrWhiteSpace(namespaceName)
            ? ""
            : $"""| where PodNamespace == "{EscapeKql(namespaceName)}" """;

        var query = $"""
        ContainerLogV2
        | where TimeGenerated > ago(24h)
        | where LogLevel =~ "error"
            or LogMessage has_any ("error", "exception", "failed", "failure")
        | project TimeGenerated, PodNamespace, PodName, ContainerName, LogLevel, LogMessage
        | order by TimeGenerated desc
        | take 5
        """;

        return QueryAsync(query, 24 * 60, CancellationToken.None);
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