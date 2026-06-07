namespace ProposalReviewer.Api.Models;

public sealed class AgentCard
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public List<string> Capabilities { get; set; } = [];
}