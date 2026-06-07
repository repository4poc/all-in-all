namespace ProposalReviewer.Api.Models;

public sealed class ValidationResult
{
    public bool IsValid { get; set; }
    public string Reason { get; set; } = string.Empty;
    public List<string> Issues { get; set; } = [];
}