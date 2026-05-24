using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text.Json;

using MeetingAnalyser.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var endpoint =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? builder.Configuration["AzureOpenAI:Endpoint"]
    ?? throw new InvalidOperationException("AzureOpenAI endpoint missing.");

var deploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? builder.Configuration["AzureOpenAI:DeploymentName"]
    ?? throw new InvalidOperationException("AzureOpenAI deployment missing.");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/.well-known/agent.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "MeetingAnalystAgent",
        Description = "You are an MeetingAnalyst Agent. Extract the topic, date , attendees, duration, action items, and overall sentiment from the provided transcript.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5005/api/agent/ask",
        Capabilities =
        [
            "meetings",
            "transcripts"
        ]
    });
});

app.MapGet("/.well-known/agent-card.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "MeetingAnalystAgent",
        Description = "You are an MeetingAnalyst Agent. Extract the topic, date , attendees, duration, action items, and overall sentiment from the provided transcript.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5005/api/agent/ask",
        Capabilities =
        [
            "meetings",
            "transcripts"
        ]
    });
});



IChatClient chatClient = new AzureOpenAIClient(
        new Uri(endpoint),
        new AzureCliCredential())
    .GetChatClient(deploymentName)
    .AsIChatClient();

AIAgent meetingAnalyserAgent = chatClient.AsAIAgent(
    name: "MeetingAnalyserAgent",
    instructions: """
    Extract meeting details from the transcript.
    Return:
    - topic
    - date
    - duration
    - attendees
    - actionItems
    - sentiment
    """
);

// 3. Execute the Agent with RunAsync<T> for strongly-typed structured output
string transcript = """
Varinder: Good morning everyone. Thanks for joining. Today 24 May 2026 , will connect for 15 min. I wanted to review the Azure migration progress and confirm next steps for deployment.

Sarah: Morning. From the infrastructure side, the App Service and Azure SQL resources are provisioned in the dev environment. Networking is also configured with VNet integration.

John: Great. On the application side, we completed the .NET 10 upgrade and deployed the latest build to the dev slot yesterday. Basic smoke testing passed.

Priya: I also validated database connectivity using managed identity. Passwordless connection is working correctly from App Service to Azure SQL.

Varinder: Nice. Any blockers at the moment?

Sarah: One thing—we still need to finalize private endpoint DNS configuration before we can disable public access on the database.

John: I also noticed one API endpoint is returning a timeout during bulk processing. I’m investigating whether it’s a SQL query issue or app configuration.

Priya: For QA testing, I’ll need the updated endpoint URL and sample data by tomorrow.

Varinder: That works. Sarah, can you complete private endpoint configuration by end of day?

Sarah: Yes, I can do that.

Varinder: John, please investigate the timeout and share findings.

John: Will do.

Varinder: Priya, once you receive the endpoint, please begin regression testing.

Priya: Sounds good.

Varinder: Quick timeline check—are we still on track for pre-prod deployment this Friday?

Sarah: Infra side yes.

John: App side yes, assuming the timeout issue is resolved today.

Priya: QA is aligned.

Varinder: Perfect. Thanks everyone. Let’s reconnect tomorrow for a quick check-in.
""";


// Meeting Analyser
app.MapPost("/api/agent/ask", async (AgentRequest request) =>
{
    AgentResponse<MeetingAnalyser.Api.Models.AgentResponse> response =
        await meetingAnalyserAgent.RunAsync<MeetingAnalyser.Api.Models.AgentResponse>(request.Question);

    Console.WriteLine(JsonSerializer.Serialize(
    response.Result,
        new JsonSerializerOptions
        {
            WriteIndented = true
        }
    ));

    return Results.Ok(response.Result);


});




app.Run("http://localhost:5005");