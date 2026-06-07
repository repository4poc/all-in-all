namespace ProposalReviewer.Api.Models;

public sealed class AuditEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProposalId { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string RequestedBy { get; set; } = string.Empty;
    public List<string> SelectedScopes { get; set; } = [];
    public string? Message { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}