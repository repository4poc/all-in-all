namespace ProposalReviewer.Api.Models;

public sealed class ProposalReviewState
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProposalId { get; set; } = string.Empty;
    public string RequestedBy { get; set; } = string.Empty;
    public string OriginalQuery { get; set; } = string.Empty;
    public string OriginalProposalTextHash { get; set; } = string.Empty;
    public string MaskedProposalText { get; set; } = string.Empty;
    public ProposalReviewStatus Status { get; set; } = ProposalReviewStatus.Created;
    public List<PolicyScope> SelectedScopes { get; set; } = [];
    public List<PolicyReviewResult> PolicyResults { get; set; } = [];
    public ProposalReviewResult? FinalResult { get; set; }
    public bool RequiresHumanReview { get; set; }
    public string? FailureReason { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}