namespace ProposalReviewer.Api.Options;

public sealed class CosmosOptions
{
    public string Endpoint { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string WorkflowStateContainer { get; set; } = string.Empty;
    public string AuditContainer { get; set; } = string.Empty;
    public string ResultsContainer { get; set; } = string.Empty;
}