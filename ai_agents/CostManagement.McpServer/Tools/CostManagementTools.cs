using System.ComponentModel;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.CostManagement;
using Azure.ResourceManager.CostManagement.Models;
using ModelContextProtocol.Server;
using Azure.ResourceManager.Advisor;
using Azure.ResourceManager.Resources;
using System.Globalization;

namespace CostManagement.McpServer.Tools;

[McpServerToolType]
public sealed class CostManagementTools
{
    private readonly IConfiguration _configuration;
    private readonly ArmClient _armClient;

    public CostManagementTools(IConfiguration configuration)
    {
        _configuration = configuration;
        _armClient = new ArmClient(new DefaultAzureCredential());
    }



    [McpServerTool]
    [Description("Gets current month-to-date Azure cost for the configured subscription.")]
    public async Task<CostSummary> GetCurrentMonthCost(
    [Description("Optional Azure scope. Example: /subscriptions/{subscriptionId} or /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}")]
    string? scope = null,
    CancellationToken cancellationToken = default)
    {
        var resolvedScope = "/subscriptions/c29c4842-c87b-4916-8841-525820e6ad23";

        var firstDay = new DateTimeOffset(
            DateTime.UtcNow.Year,
            DateTime.UtcNow.Month,
            1,
            0,
            0,
            0,
            TimeSpan.Zero);

        var now = DateTimeOffset.UtcNow;

        var query = CreateActualCostQuery(
            from: firstDay,
            to: now,
            granularity: GranularityType.Daily);

        var response = await CostManagementExtensions.UsageQueryAsync(
            _armClient,
            new ResourceIdentifier(resolvedScope),
            query,
            cancellationToken);

        return ParseTotalCost(response.Value, "CurrentMonthToDate");
    }


    [McpServerTool]
    [Description("Gets Azure daily cost trend for a time range.")]
    public async Task<IReadOnlyList<DailyCost>> GetDailyCostTrend(
        int days = 30,
        string? scope = null,
        CancellationToken cancellationToken = default)
    {
        days = Math.Clamp(days, 1, 90);

        var resolvedScope = "/subscriptions/c29c4842-c87b-4916-8841-525820e6ad23";

        var to = DateTimeOffset.UtcNow;
        var from = to.AddDays(-days);

        var query = CreateActualCostQuery(
            from: from,
            to: to,
            granularity: GranularityType.Daily);

        var response = await CostManagementExtensions.UsageQueryAsync(
            _armClient,
            new ResourceIdentifier(resolvedScope),
            query,
            cancellationToken);

        return ParseDailyCosts(response.Value);
    }

    [McpServerTool]
    [Description("Gets top Azure services by cost.")]
    public async Task<IReadOnlyList<ServiceCost>> GetTopServicesByCost(
        int days = 30,
        int top = 10,
        string? scope = null,
        CancellationToken cancellationToken = default)
    {
        days = Math.Clamp(days, 1, 90);
        top = Math.Clamp(top, 1, 25);

        var resolvedScope = ResolveScope(scope);
        var to = DateTimeOffset.UtcNow;
        var from = to.AddDays(-days);

        var dataset = new QueryDataset
        {
            Granularity = null
        };

        dataset.Aggregation.Add(
            "totalCost",
            new QueryAggregation("PreTaxCost", FunctionType.Sum));

        dataset.Grouping.Add(
            new QueryGrouping(QueryColumnType.Dimension, "ServiceName"));

        var query = new QueryDefinition(
            ExportType.ActualCost,
            TimeframeType.Custom,
            dataset)
        {
            TimePeriod = new QueryTimePeriod(from, to)
        };

        var result = await CostManagementExtensions.UsageQueryAsync(
            _armClient,
            new ResourceIdentifier(resolvedScope),
            query,
            cancellationToken);

        return ParseServiceCosts(result.Value)
            .OrderByDescending(x => x.Cost)
            .Take(top)
            .ToList();
    }

    [McpServerTool]
    [Description("Gets top Azure resource groups by cost.")]
    public async Task<IReadOnlyList<ResourceGroupCost>> GetTopResourceGroupsByCost(
    int days = 30,
    int top = 10,
    string? scope = null,
    CancellationToken cancellationToken = default)
    {
        days = Math.Clamp(days, 1, 90);
        top = Math.Clamp(top, 1, 25);

        var resolvedScope = "/subscriptions/c29c4842-c87b-4916-8841-525820e6ad23";

        var to = DateTimeOffset.UtcNow;
        var from = to.AddDays(-days);

        var dataset = new QueryDataset
        {
            Granularity = null
        };

        dataset.Aggregation.Add(
            "totalCost",
            new QueryAggregation("PreTaxCost", FunctionType.Sum));

        dataset.Grouping.Add(
            new QueryGrouping(
                QueryColumnType.Dimension,
                "ResourceGroupName"));

        var query = new QueryDefinition(
            ExportType.ActualCost,
            TimeframeType.Custom,
            dataset)
        {
            TimePeriod = new QueryTimePeriod(from, to)
        };

        var response = await CostManagementExtensions.UsageQueryAsync(
            _armClient,
            new ResourceIdentifier(resolvedScope),
            query,
            cancellationToken);

        return ParseResourceGroupCosts(response.Value)
            .OrderByDescending(x => x.Cost)
            .Take(top)
            .ToList();
    }

    [McpServerTool]
    [Description("Returns cost-saving suggestions using Azure Advisor cost recommendations.")]
    public async Task<IReadOnlyList<CostSavingSuggestion>> GetCostSavingSuggestions(
    CancellationToken cancellationToken = default)
    {
        var subscriptionId = _configuration["Azure:SubscriptionId"];

        if (string.IsNullOrWhiteSpace(subscriptionId))
        {
            throw new InvalidOperationException("Azure:SubscriptionId is missing.");
        }

        var subscriptionResourceId =
            SubscriptionResource.CreateResourceIdentifier(subscriptionId);

        var recommendations = new List<CostSavingSuggestion>();

        var advisorRecommendations =
            _armClient.GetAdvisorRecommendations(subscriptionResourceId);

        await foreach (var recommendation in advisorRecommendations.GetAllAsync())
        {
            cancellationToken.ThrowIfCancellationRequested();

            var data = recommendation.Data;

            if (!string.Equals(
                    data.Category?.ToString(),
                    "Cost",
                    StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            recommendations.Add(new CostSavingSuggestion
            {
                Impact = data.Impact?.ToString(),
                Problem = data.ShortDescription?.Problem,
                Solution = data.ShortDescription?.Solution,
                ResourceId = data.ResourceMetadata.ResourceId,
                RecommendationTypeId = data.RecommendationTypeId?.ToString(),
                LastUpdated = null
            });
        }

        return recommendations
            .Take(50)
            .ToList();
    }

    [McpServerTool]
    [Description("Creates a FinOps-style summary from current cost, top services, resource groups, and Advisor recommendations.")]
    public async Task<CostOptimizationSummary> GetCostOptimizationSummary(
        int days = 30,
        string? scope = null,
        CancellationToken cancellationToken = default)
    {
        days = Math.Clamp(days, 1, 90);

        var resolvedScope = "/subscriptions/c29c4842-c87b-4916-8841-525820e6ad23";

        var currentMonth = await GetCurrentMonthCost(
            resolvedScope,
            cancellationToken);

        var topResourceGroups = await GetTopResourceGroupsByCost(
            days,
            5,
            resolvedScope,
            cancellationToken);

        var suggestions = await GetCostSavingSuggestions(cancellationToken);

        var to = DateTimeOffset.UtcNow;
        var from = to.AddDays(-days);

        var dataset = new QueryDataset
        {
            Granularity = null
        };

        dataset.Aggregation.Add(
            "totalCost",
            new QueryAggregation("PreTaxCost", FunctionType.Sum));

        dataset.Grouping.Add(
            new QueryGrouping(QueryColumnType.Dimension, "ServiceName"));

        var query = new QueryDefinition(
            ExportType.ActualCost,
            TimeframeType.Custom,
            dataset)
        {
            TimePeriod = new QueryTimePeriod(from, to)
        };

        var response = await CostManagementExtensions.UsageQueryAsync(
            _armClient,
            new ResourceIdentifier(resolvedScope),
            query,
            cancellationToken);

        var topServices = response.Value.Rows
            .Select(row => new ServiceCost
            {
                Cost = ParseBinaryDataDecimal(row[0]),
                ServiceName = row.Count > 1 ? row[1].ToString().Trim('"') : "Unknown",
                Currency = row.Count > 2 ? row[2].ToString().Trim('"') : "Unknown"
            })
            .OrderByDescending(x => x.Cost)
            .Take(5)
            .ToList();

        return new CostOptimizationSummary
        {
            CurrentMonthCost = currentMonth,
            TopServices = topServices,
            TopResourceGroups = topResourceGroups,
            SavingsSuggestions = suggestions.Take(10).ToList(),
            Notes =
            [
                "Validate Azure Advisor recommendations before applying changes.",
            "Prioritize high-impact recommendations for idle, underutilized, or oversized resources.",
            "Review top services and resource groups with sudden cost growth."
            ]
        };
    }

    private QueryDefinition CreateActualCostQuery(
        DateTimeOffset from,
        DateTimeOffset to,
        GranularityType granularity)
    {
        var dataset = new QueryDataset
        {
            Granularity = granularity
        };

        dataset.Aggregation.Add(
            "totalCost",
            new QueryAggregation(
                "PreTaxCost",
                FunctionType.Sum));

        return new QueryDefinition(
            ExportType.ActualCost,
            TimeframeType.Custom,
            dataset)
        {
            TimePeriod = new QueryTimePeriod(from, to)
        };
    }

    private string ResolveScope(string? scope)
    {
        if (!string.IsNullOrWhiteSpace(scope))
        {
            return scope;
        }

        var configuredScope = _configuration["Azure:DefaultScope"];

        if (!string.IsNullOrWhiteSpace(configuredScope))
        {
            return configuredScope;
        }

        var subscriptionId = _configuration["Azure:SubscriptionId"];

        if (string.IsNullOrWhiteSpace(subscriptionId))
        {
            throw new InvalidOperationException("Azure subscription scope is not configured.");
        }

        return $"/subscriptions/{subscriptionId}";
    }

    private static CostSummary ParseTotalCost(QueryResult result, string period)
    {
        decimal totalCost = 0m;

        var costColumnIndex = 0;

        for (var i = 0; i < result.Columns.Count; i++)
        {
            var name = result.Columns[i].Name;

            if (string.Equals(name, "Cost", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(name, "PreTaxCost", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(name, "CostUSD", StringComparison.OrdinalIgnoreCase))
            {
                costColumnIndex = i;
                break;
            }
        }

        foreach (var row in result.Rows)
        {
            var value = row[costColumnIndex];

            totalCost += ParseBinaryDataDecimal(value);
        }

        return new CostSummary
        {
            Period = period,
            Cost = Math.Round(totalCost, 2)
        };
    }

    private static decimal ParseBinaryDataDecimal(BinaryData binaryData)
    {
        var text = binaryData.ToString().Trim().Trim('"');

        if (decimal.TryParse(
                text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var parsed))
        {
            return parsed;
        }

        return binaryData.ToObjectFromJson<decimal>();
    }

    private static IReadOnlyList<DailyCost> ParseDailyCosts(QueryResult result)
    {
        var list = new List<DailyCost>();

        foreach (var row in result.Rows)
        {
            list.Add(new DailyCost
            {
                Cost = ParseBinaryDataDecimal(row[0]),
                UsageDate = row.Count > 1 ? row[1].ToString().Trim('"') : null,
                Currency = row.Count > 2 ? row[2].ToString().Trim('"') : "Unknown"
            });
        }

        return list;
    }

    private static IReadOnlyList<ServiceCost> ParseServiceCosts(QueryResult result)
    {
        var list = new List<ServiceCost>();

        foreach (var row in result.Rows)
        {
            decimal cost = 0;

            if (row.Count > 0)
            {
                var costValue = row[0];

                if (costValue is BinaryData binaryData)
                {
                    cost = ParseBinaryDataDecimal(binaryData);
                }
                else
                {
                    cost = Convert.ToDecimal(costValue, CultureInfo.InvariantCulture);
                }
            }

            list.Add(new ServiceCost
            {
                Cost = cost,
                ServiceName = row.Count > 1
                    ? row[1]?.ToString()?.Trim('"') ?? "Unknown"
                    : "Unknown",
                Currency = row.Count > 2
                    ? row[2]?.ToString()?.Trim('"') ?? "Unknown"
                    : "Unknown"
            });
        }

        return list;
    }

    private static IReadOnlyList<ResourceGroupCost> ParseResourceGroupCosts(QueryResult result)
    {
        var list = new List<ResourceGroupCost>();

        foreach (var row in result.Rows)
        {
            list.Add(new ResourceGroupCost
            {
                Cost = ParseBinaryDataDecimal(row[0]),
                ResourceGroupName = row.Count > 1
                    ? row[1].ToString().Trim('"')
                    : "Unknown",
                Currency = row.Count > 2
                    ? row[2].ToString().Trim('"')
                    : "Unknown"
            });
        }

        return list;
    }
}

public sealed class CostSummary
{
    public string? Period { get; set; }
    public decimal Cost { get; set; }
    public string? Currency { get; set; }
}

public sealed class DailyCost
{
    public string? UsageDate { get; set; }
    public decimal Cost { get; set; }
    public string? Currency { get; set; }
}

public sealed class ServiceCost
{
    public string? ServiceName { get; set; }
    public decimal Cost { get; set; }
    public string? Currency { get; set; }
}

public sealed class ResourceGroupCost
{
    public string? ResourceGroupName { get; set; }
    public decimal Cost { get; set; }
    public string? Currency { get; set; }
}

public sealed class CostSavingSuggestion
{
    public string? Impact { get; set; }
    public string? Problem { get; set; }
    public string? Solution { get; set; }
    public string? ResourceId { get; set; }
    public string? RecommendationTypeId { get; set; }
    public DateTimeOffset? LastUpdated { get; set; }
}

public sealed class CostOptimizationSummary
{
    public CostSummary? CurrentMonthCost { get; set; }
    public IReadOnlyList<ServiceCost> TopServices { get; set; } = [];
    public IReadOnlyList<ResourceGroupCost> TopResourceGroups { get; set; } = [];
    public IReadOnlyList<CostSavingSuggestion> SavingsSuggestions { get; set; } = [];
    public IReadOnlyList<string> Notes { get; set; } = [];
}