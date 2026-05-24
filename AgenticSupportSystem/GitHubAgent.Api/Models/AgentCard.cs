namespace RagAgent.Api.Models;

public sealed class AgentCard
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = "1.0.0";
    public string Endpoint { get; set; } = string.Empty;
    public string[] Capabilities { get; set; } = [];
}