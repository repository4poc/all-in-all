namespace Orchestrator.Api.Models;

public sealed class AgentRequest
{
    public string Question { get; set; } = string.Empty;
     public string SubscriptionId { get; set; } = "";
}