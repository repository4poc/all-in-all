using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting.AGUI.AspNetCore;
using Microsoft.Extensions.AI;
using Orchestrator.Api.Models;
using Orchestrator.Api.Services;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Services.AddAGUI();
builder.Services.AddSingleton<RemoteAgentClient>();
builder.Services.AddHttpContextAccessor();

var endpoint =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? builder.Configuration["AzureOpenAI:Endpoint"]
    ?? throw new InvalidOperationException("AzureOpenAI endpoint missing.");

var deploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? builder.Configuration["AzureOpenAI:DeploymentName"]
    ?? throw new InvalidOperationException("AzureOpenAI deployment missing.");

var ragAgentBaseUrl =
    builder.Configuration["Agents:RagAgentBaseUrl"]
    ?? "http://localhost:5001";

var githubAgentBaseUrl =
    builder.Configuration["Agents:GitHubAgentBaseUrl"]
    ?? "http://localhost:5002";

var azureCostAgentBaseUrl =
    builder.Configuration["Agents:AzureCostAgentBaseUrl"]
    ?? "http://localhost:5004";

var meetingAnalyserAgentBaseUrl =
    builder.Configuration["Agents:MeetingAnalyserAgentBaseUrl"]
    ?? "http://localhost:5005";


var app = builder.Build();

var remoteAgentClient = app.Services.GetRequiredService<RemoteAgentClient>();

[Description("Discovers and calls the RAG agent for document, policy, knowledge-base, architecture, onboarding, and internal guide questions.")]
async Task<string> AskRagAgentAsync(string question)
{
    return await remoteAgentClient.AskAgentAsync(ragAgentBaseUrl, question);
}

[Description("Discovers and calls the GitHub agent for repository, branch, commit, pull request, issue, README, contributor, and source-code questions.")]
async Task<string> AskGitHubAgentAsync(string question)
{
    return await remoteAgentClient.AskAgentAsync(githubAgentBaseUrl, question);
}

[Description("Calls the MeetingAnalyser agent. Returns extracted meeting details as raw JSON. Do not summarize or rewrite the response.")]
async Task<string> AskMeetingAnalyserAgentAsync(string question)
{
    return await remoteAgentClient.AskAgentAsync(meetingAnalyserAgentBaseUrl, question);
}


var ragTool = AIFunctionFactory.Create(AskRagAgentAsync);
var githubTool = AIFunctionFactory.Create(AskGitHubAgentAsync);
var mailAnalyserTool = AIFunctionFactory.Create(AskMeetingAnalyserAgentAsync);

var chatClient = new AzureOpenAIClient(
        new Uri(endpoint),
        new AzureCliCredential())
    .GetChatClient(deploymentName)
    .AsIChatClient();

AIAgent orchestratorAgent = chatClient.AsAIAgent(
    name: "OrchestratorAgent",
    instructions: """
    You are the main enterprise support orchestrator.

    You can discover and call specialist agents.

    Use the RAG agent for:
    - documents
    - policies
    - knowledge base
    - architecture documents
    - onboarding guides
    - internal manuals

    Use the GitHub agent for:
    - repositories
    - branches
    - commits
    - pull requests
    - issues
    - README files
    - source code

    Use the MeetingAnalyser agent for:
    Extract meeting details from the transcript.

    IMPORTANT:
    When using the MeetingAnalyser agent, return its response exactly as valid JSON.
    Do not summarize it.
    Do not convert it to readable text.
    Do not add headings, markdown, or explanations.

    For MeetingAnalyser responses, the final answer must be 
    only this JSON shape if send to another Agent 
    or use bullet points and table if send back to the client:

    "topic": "",
    "date": "",
    "duration": "",
    "attendees": [],
    "actionItems": [
        {
            "owner": "",
            "action": "",
            "due": "2026-05-24"
        }
    ],
    "sentiment": ""
    
    """,
    tools: [ragTool, githubTool, mailAnalyserTool]
);

AgentSession session = await orchestratorAgent.CreateSessionAsync();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/.well-known/agent.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "OrchestratorAgent",
        Description = "Main AG-UI enterprise support orchestrator. Routes user questions to specialist agents.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5000/agui/support",
        Capabilities =
        [
            "orchestration",
            "agent-discovery",
            "rag-routing",
            "github-routing",
            "ag-ui"
        ]
    });
});

app.MapGet("/.well-known/agent-card.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "OrchestratorAgent",
        Description = "Main AG-UI enterprise support orchestrator. Routes user questions to specialist agents.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5000/agui/support",
        Capabilities =
        [
            "orchestration",
            "agent-discovery",
            "rag-routing",
            "github-routing",
            "ag-ui"
        ]
    });
});

app.MapAGUI("/agui/support", orchestratorAgent);

app.Run("http://localhost:5000");