using Azure.AI.OpenAI;
using Azure.Identity;
using MeetingAnalyser.Api.Models;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var endpoint =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? builder.Configuration["AzureOpenAI:Endpoint"]
    ?? throw new InvalidOperationException("AzureOpenAI endpoint missing.");

var deploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? builder.Configuration["AzureOpenAI:DeploymentName"]
    ?? throw new InvalidOperationException("AzureOpenAI deployment missing.");

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var credential = new AzureCliCredential();

IChatClient chatClient = new AzureOpenAIClient(
        new Uri(endpoint),
        credential)
    .GetChatClient(deploymentName)
    .AsIChatClient();

AIAgent meetingAnalyserAgent = chatClient.AsAIAgent(
    name: "MeetingAnalyserAgent",
    instructions: """
        You are a Meeting Analyser Agent.

        Extract meeting information from the transcript.

        Return Markdown only.
        Do not return JSON.
        Do not wrap the answer in code fences.

        Use exactly this format:

        # Meeting Summary

        ## Topic
        <topic>

        ## Date
        <date>

        ## Duration
        <duration>

        ## Attendees
        - <attendee>

        ## Action Items

        | Owner | Action | Due Date |
        |---|---|---|
        | <owner> | <action> | <due date> |

        ## Sentiment
        <Positive, Neutral, Negative, or Mixed>

        Rules:
        - If a value is missing, write: Not specified
        - If attendees are missing, write: No attendees specified
        - If no action items exist, write: No action items identified
        - Sentiment must be one of: Positive, Neutral, Negative, Mixed
        """
/*instructions: """
You are a Meeting Analyser Agent.

Extract structured meeting information from the transcript.

Return JSON only.
Do not return markdown.
Do not add explanations.
Do not wrap the JSON in ```json.

Required JSON shape:

{
"topic": "",
"date": "",
"duration": "",
"attendees": [],
"actionItems": [
{
"owner": "",
"action": "",
"due": ""
}
],
"sentiment": ""
}

Rules:
- If a value is missing, use an empty string.
- If attendees are missing, return an empty array.
- If no action items exist, return an empty array.
- Sentiment must be one of: Positive, Neutral, Negative, Mixed.
"""
*/

);

app.MapGet("/.well-known/agent.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "MeetingAnalyserAgent",
        Description = "Extracts topic, date, attendees, duration, action items, and sentiment from a meeting transcript.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5005/api/agent/ask",
        Capabilities =
        [
            "meetings",
            "transcripts",
            "action-items",
            "sentiment-analysis"
        ]
    });
});

app.MapPost("/api/agent/ask", async (AgentRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Question))
    {
        return Results.BadRequest(new
        {
            error = "Transcript is required."
        });
    }

    if (request.Question.Length > 8000)
    {
        return Results.BadRequest(new
        {
            error = "Transcript is too long. Maximum allowed length is 8000 characters."
        });
    }

    AgentResponse<MeetingAnalyser.Api.Models.AgentResponse> response =
        await meetingAnalyserAgent.RunAsync<MeetingAnalyser.Api.Models.AgentResponse>(
            request.Question);

    return Results.Ok(response.Result);
});

app.Run("http://localhost:5005");