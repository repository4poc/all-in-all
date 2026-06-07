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


// Register AG-UI Services
builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Services.AddAGUI();

builder.Services.AddSingleton<RemoteAgentClient>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


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

var proposalReviewerAgentBaseUrl =
    builder.Configuration["Agents:ProposalReviewerAgentBaseUrl"]
    ?? "http://localhost:5006";

var petstoreOpenApiAgentBaseUrl =
    builder.Configuration["Agents:PetstoreOpenApiAgentBaseUrl"]
    ?? "http://localhost:5007";

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

[Description("Discovers and calls the MeetingAnalyser agent. Returns extracted meeting details as raw JSON. Do not summarize or rewrite the response.")]
async Task<string> AskMeetingAnalyserAgentAsync(
    [Description("The meeting trascript. Total length shoule be less than 500 characters")] string question)
{
    return await remoteAgentClient.AskAgentAsync(meetingAnalyserAgentBaseUrl, question);
}

[Description("Discovers and calls the ProposalReviewer agent for proposal reviews against GDPR, HIPAA, security, compliance, and company policies.")]
async Task<string> AskProposalReviewerAgentAsync(
    [Description("Proposal review request. Include proposal text and target review scope such as GDPR, HIPAA, security, compliance, company policy, or full review.")]
    string question)
{
    return await remoteAgentClient.AskAgentAsync(
        proposalReviewerAgentBaseUrl,
        question);
}

[Description("Discovers and calls the Petstore OpenAPI agent. Use this for petstore, pets, pet id, pet status, store inventory, orders, and user operations exposed by the Petstore OpenAPI specification.")]
async Task<string> AskPetstoreOpenApiAgentAsync(
    [Description("The user's Petstore API question. Examples: find available pets, get pet by id 10, check store inventory.")]
    string question)
{
    return await remoteAgentClient.AskAgentAsync(
        petstoreOpenApiAgentBaseUrl,
        question);
}

var ragTool = AIFunctionFactory.Create(AskRagAgentAsync);
var githubTool = AIFunctionFactory.Create(AskGitHubAgentAsync);
var meetingAnalyserTool = AIFunctionFactory.Create(AskMeetingAnalyserAgentAsync);
var proposalReviewerTool =
    AIFunctionFactory.Create(AskProposalReviewerAgentAsync);

var petstoreOpenApiTool =
    AIFunctionFactory.Create(AskPetstoreOpenApiAgentAsync);

/*var meetingAnalyserTool =
    new ApprovalRequiredAIFunction(
        AIFunctionFactory.Create(AskMeetingAnalyserAgentAsync));
*/
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
    Before calling the MeetingAnalyser tool, approval is required.
    When the user provides a transcript, prepare the tool call.
    The user must approve before the transcript is sent to the MeetingAnalyser agent
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


    Use the ProposalReviewer agent for:
    - proposal document review
    - GDPR review
    - HIPAA review
    - security review
    - compliance review
    - company custom policy review
    - full proposal risk review

    If user asks "review only GDPR", call ProposalReviewer with GDPR scope.
    If user asks "review security", call ProposalReviewer with Security scope.
    If user asks "full proposal review", call ProposalReviewer with all scopes.


    Use the Petstore OpenAPI agent for:
    - petstore API questions
    - finding pets by status
    - getting a pet by id
    - store inventory
    - pet orders
    - Petstore user operations
    - questions that require reading or using the Petstore OpenAPI specification

    When the user asks about available, pending, or sold pets, call the Petstore OpenAPI agent.
    When the user asks for a specific pet id, call the Petstore OpenAPI agent.
    Do not manually construct Petstore URLs.
    Do not answer Petstore API questions from memory.    
    
    """,
    tools: [ragTool, githubTool, meetingAnalyserTool, proposalReviewerTool, petstoreOpenApiTool]
);

AgentSession session = await orchestratorAgent.CreateSessionAsync();
Console.WriteLine(session.StateBag.ToString());

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
            "openapi-routing",
            "petstore-routing",
            "ag-ui"
        ]
    });
});

app.MapAGUI("/agui/support", orchestratorAgent);

app.Run("http://localhost:5000");