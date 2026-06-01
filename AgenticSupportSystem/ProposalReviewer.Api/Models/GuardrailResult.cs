namespace ProposalReviewer.Api.Models;

public sealed class GuardrailResult
{
    public bool IsAllowed { get; set; }
    public string Reason { get; set; } = string.Empty;
    public List<string> Issues { get; set; } = [];
}