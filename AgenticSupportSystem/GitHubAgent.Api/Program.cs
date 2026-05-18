using Azure.AI.OpenAI;
using Azure.Identity;
using GitHubAgent.Api.Models;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text.Json;

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

var httpClientFactory = app.Services.GetRequiredService<IHttpClientFactory>();

IChatClient chatClient = new AzureOpenAIClient(
        new Uri(endpoint),
        new AzureCliCredential())
    .GetChatClient(deploymentName)
    .AsIChatClient();

[Description("Gets GitHub repository metadata.")]
async Task<string> GetRepositoryDetailsAsync(string owner, string repo)
{
    //var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
    var token = "";

    var http = httpClientFactory.CreateClient();
    http.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubAgent.Api");

    if (!string.IsNullOrWhiteSpace(token))
    {
        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    var response = await http.GetAsync($"https://api.github.com/repos/{owner}/{repo}");

    if (!response.IsSuccessStatusCode)
    {
        return $"Could not fetch GitHub repository {owner}/{repo}. Status: {response.StatusCode}";
    }

    var json = await response.Content.ReadAsStringAsync();
    using var document = JsonDocument.Parse(json);
    var root = document.RootElement;

    return $"""
    Repository: {root.GetProperty("full_name").GetString()}
    Description: {root.GetProperty("description").GetString()}
    Default branch: {root.GetProperty("default_branch").GetString()}
    Stars: {root.GetProperty("stargazers_count").GetInt32()}
    Forks: {root.GetProperty("forks_count").GetInt32()}
    Open issues: {root.GetProperty("open_issues_count").GetInt32()}
    URL: {root.GetProperty("html_url").GetString()}
    """;
}

var githubTool = AIFunctionFactory.Create(GetRepositoryDetailsAsync);

AIAgent githubAgent = chatClient.AsAIAgent(
    name: "GitHubAgent",
    instructions: """
    You are a GitHub repository specialist.

    Use the GitHub tool for repository metadata, branches, commits,
    pull requests, issues, README, contributors, and source-code questions.
    """,
    tools: [githubTool]
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/.well-known/agent.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "GitHubAgent",
        Description = "Answers GitHub repository, branch, commit, pull request, issue, README, contributor, and source-code questions.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5002/api/agent/ask",
        Capabilities =
        [
            "github",
            "repositories",
            "branches",
            "commits",
            "pull-requests",
            "issues",
            "readme",
            "source-code"
        ]
    });
});

app.MapGet("/.well-known/agent-card.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "GitHubAgent",
        Description = "Answers GitHub repository, branch, commit, pull request, issue, README, contributor, and source-code questions.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5002/api/agent/ask",
        Capabilities =
        [
            "github",
            "repositories",
            "branches",
            "commits",
            "pull-requests",
            "issues",
            "readme",
            "source-code"
        ]
    });
});

// Synchronous or non-streaming approach with RunAsync
/* 

app.MapPost("/api/agent/ask", async (AgentRequest request) =>
{
    var response = await githubAgent.RunAsync(request.Question);
    return Results.Ok(new GitHubAgent.Api.Models.AgentResponse
    {
        Answer = response.Text
    });
});
*/

// Meeting Analyser
app.MapPost("/api/agent/ask", async (AgentRequest request) =>
{
    var response = await githubAgent.RunAsync(request.Question);
    return Results.Ok(new GitHubAgent.Api.Models.AgentResponse
    {
        Answer = response.Text
    });
});




app.Run("http://localhost:5002");