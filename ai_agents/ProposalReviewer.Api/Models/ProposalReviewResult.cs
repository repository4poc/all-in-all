namespace ProposalReviewer.Api.Models;

public sealed class ProposalReviewResult
{
    public string ProposalId { get; set; } = string.Empty;
    public List<string> ReviewScope { get; set; } = [];
    public string OverallRisk { get; set; } = string.Empty;
    public string ApprovalRecommendation { get; set; } = string.Empty;
    public string ExecutiveSummary { get; set; } = string.Empty;
    public List<PolicyFinding> Findings { get; set; } = [];
    public List<RequiredAction> RequiredActions { get; set; } = [];
}