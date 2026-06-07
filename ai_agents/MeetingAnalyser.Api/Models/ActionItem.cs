namespace MeetingAnalyser.Api.Models;

public sealed class ActionItem
{
    public string Owner { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string Due { get; set; } = string.Empty;
}