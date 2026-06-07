namespace ProposalReviewer.Api.Models;

public sealed class PolicyFinding
{
    public string Scope { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string EvidenceFromProposal { get; set; } = string.Empty;
    public string PolicyCitation { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
}