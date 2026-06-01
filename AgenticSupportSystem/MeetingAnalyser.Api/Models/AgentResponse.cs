namespace MeetingAnalyser.Api.Models;

public sealed class AgentResponse
{
    public string Topic { get; set; } = string.Empty;

    public string Date { get; set; } = string.Empty;

    public string Duration { get; set; } = string.Empty;

    public List<string> Attendees { get; set; } = [];

    public List<ActionItem> ActionItems { get; set; } = [];

    public string Sentiment { get; set; } = string.Empty;
}