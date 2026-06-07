using Microsoft.Agents.AI;
using ProposalReviewer.Api.Extensions;
using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Workflow;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddProposalReviewerServices(builder.Configuration);

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/.well-known/agent.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "ProposalReviewerAgent",
        Description = "Enterprise proposal review agent with staged guardrails, PII masking, RAG policy review, validation, consolidation, and Cosmos DB audit.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5006/api/agent/ask",
        Capabilities =
        [
            "proposal-review",
            "gdpr-review",
            "hipaa-review",
            "security-review",
            "company-policy-review",
            "rag",
            "audit",
            "guardrails"
        ]
    });
});

app.MapPost("/api/agent/ask", async (
    AgentRequest request,
    ProposalReviewWorkflow workflow,
    CancellationToken cancellationToken) =>
{

    if (string.IsNullOrWhiteSpace(request.Question))
    {
        return Results.BadRequest(new { error = "Question is required." });
    }

    var reviewRequest = new ProposalReviewRequest
    {
        ProposalId = Guid.NewGuid().ToString(),
        UserQuery = request.Question,
        ProposalText = request.Question,
        RequestedBy = "remote-orchestrator"
    };

    var result = await workflow.RunAsync(
        reviewRequest,
        cancellationToken);

    return Results.Ok(result);
});

app.MapPost("/api/proposal-review", async (
    ProposalReviewRequest request,
    ProposalReviewWorkflow workflow,
    CancellationToken cancellationToken) =>
{
    var result = await workflow.RunAsync(request, cancellationToken);
    return Results.Ok(result);
});

app.Run("http://localhost:5006");