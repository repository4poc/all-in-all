namespace ProposalReviewer.Api.Models;

public sealed class PolicyChunk
{
    public string Id { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string PolicyVersion { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public double? Score { get; set; }
}