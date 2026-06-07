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

IChatClient chatClient = new AzureOpenAIClient(
        new Uri(endpoint),
        new AzureCliCredential())
    .GetChatClient(deploymentName)
    .AsIChatClient();

AIAgent meetingAnalyserAgent = chatClient.AsAIAgent(
    name: "MeetingAnalyserAgent",
    instructions: """
    You are a Meeting Analyser Agent.

    Extract structured meeting information from the transcript.

    Rules:
    - If a value is missing, use an empty string.
    - If attendees are missing, return an empty array.
    - If no action items exist, return an empty array.
    - Sentiment must be one of: Positive, Neutral, Negative, Mixed.
    """
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