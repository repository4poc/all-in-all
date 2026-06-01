namespace ProposalReviewer.Api.Models;

public sealed class RequiredAction
{
    public string Owner { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
}