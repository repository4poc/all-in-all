namespace ProposalReviewer.Api.Models;

public sealed class PolicyReviewResult
{
    public string Scope { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public List<PolicyFinding> Findings { get; set; } = [];
    public List<PolicyChunk> Citations { get; set; } = [];
}