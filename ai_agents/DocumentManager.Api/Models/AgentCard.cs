namespace DocumentManager.Api.Models;

public sealed class AgentCard
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Version { get; set; } = "";
    public string Endpoint { get; set; } = "";
    public string[] Capabilities { get; set; } = [];
}