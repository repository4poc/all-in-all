namespace ProposalReviewer.Api.Models;

public sealed class ProposalReviewRequest
{
    public string ProposalId { get; set; } = Guid.NewGuid().ToString();
    public string UserQuery { get; set; } = string.Empty;
    public string ProposalText { get; set; } = string.Empty;
    public string RequestedBy { get; set; } = string.Empty;
    public List<PolicyScope> RequestedScopes { get; set; } = [];
}