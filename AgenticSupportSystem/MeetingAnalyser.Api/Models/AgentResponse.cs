using System.Text.Json.Serialization;

namespace MeetingAnalyser.Api.Models;

public record AgentResponse(
    [property: JsonPropertyName("topic")] string Topic,
    [property: JsonPropertyName("date")] string Date,
    [property: JsonPropertyName("duration")] string Duration,
    [property: JsonPropertyName("attendees")] string[] Attendees,
    [property: JsonPropertyName("actionItems")] ActionItem[] ActionItems,
    [property: JsonPropertyName("sentiment")] string Sentiment
);

public record ActionItem(
    [property: JsonPropertyName("owner")] string Owner,
    [property: JsonPropertyName("action")] string Action,
    [property: JsonPropertyName("due")] string Due
);