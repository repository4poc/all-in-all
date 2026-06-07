using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using RagAgent.Api.Models;
using System.ComponentModel;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var endpoint =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? builder.Configuration["AzureOpenAI:Endpoint"]
    ?? throw new InvalidOperationException("AzureOpenAI endpoint missing.");

var deploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? builder.Configuration["AzureOpenAI:DeploymentName"]
    ?? throw new InvalidOperationException("AzureOpenAI deployment missing.");

var chatClient = new AzureOpenAIClient(
        new Uri(endpoint),
        new AzureCliCredential())
    .GetChatClient(deploymentName)
    .AsIChatClient();

[Description("Searches internal documents and knowledge base.")]
Task<string> SearchKnowledgeBaseAsync(string query)
{
    var result = $"""
    RAG result for: {query}

    This is placeholder RAG data.
    Replace this with Azure AI Search, pgvector, Cosmos DB, or your document search.
    """;

    return Task.FromResult(result);
}

var ragTool = AIFunctionFactory.Create(SearchKnowledgeBaseAsync);

AIAgent ragAgent = chatClient.AsAIAgent(
    name: "RagAgent",
    instructions: """
    You are a RAG specialist agent.

    Use the knowledge-base search tool for document, policy, internal guide,
    architecture, onboarding, or uploaded-file questions.

    If the answer is not found, say so clearly.
    """,
    tools: [ragTool]
);

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
        Name = "RagAgent",
        Description = "Answers document, policy, knowledge-base, and internal guide questions.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5001/api/agent/ask",
        Capabilities =
        [
            "rag",
            "documents",
            "knowledge-base",
            "policies",
            "architecture-documents",
            "onboarding-guides"
        ]
    });
});

app.MapGet("/.well-known/agent-card.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "RagAgent",
        Description = "Answers document, policy, knowledge-base, and internal guide questions.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5001/api/agent/ask",
        Capabilities =
        [
            "rag",
            "documents",
            "knowledge-base",
            "policies",
            "architecture-documents",
            "onboarding-guides"
        ]
    });
});

app.MapPost("/api/agent/ask", async (AgentRequest request) =>
{
    var response = await ragAgent.RunAsync(request.Question);
    return Results.Ok(new RagAgent.Api.Models.AgentResponse
    {
        Answer = response.Text
    });
});

app.Run("http://localhost:5001");