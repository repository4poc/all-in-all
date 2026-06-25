using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using Azure.Search.Documents;
using Azure.Storage.Blobs;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Text.Json;
using DocumentManager.Api.Models;

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

var searchEndpoint =
    builder.Configuration["AzureSearch:Endpoint"]
    ?? throw new InvalidOperationException("Azure AI Search endpoint missing.");

var searchIndex =
    builder.Configuration["AzureSearch:IndexName"]
    ?? "document-manager-index";

var credential = new AzureCliCredential();

builder.Services.AddSingleton(new SearchClient(
    new Uri(searchEndpoint),
    searchIndex,
    credential));

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var accountName = config["Storage:AccountName"];

    return new BlobServiceClient(
        new Uri($"https://{accountName}.blob.core.windows.net"),
        new DefaultAzureCredential());
});

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

[Description("Searches enterprise documents using metadata and content.")]
async Task<string> SearchDocumentsAsync(
    [Description("Search text, for example: approved contracts for Contoso")] string query,
    [Description("Optional OData filter. Only use simple Azure AI Search filters like: customer eq 'Contoso' and status eq 'Approved'. Do not use contains().")] string? filter = null)
{
    var searchClient = app.Services.GetRequiredService<SearchClient>();

    async Task<string> RunSearchAsync(string? safeFilter)
    {
        var options = new SearchOptions
        {
            Size = 5,
            Filter = string.IsNullOrWhiteSpace(safeFilter) ? null : safeFilter
        };

        options.Select.Add("id");
        options.Select.Add("documentId");
        options.Select.Add("fileName");
        options.Select.Add("Content");
        options.Select.Add("documentType");
        options.Select.Add("customer");
        options.Select.Add("project");
        options.Select.Add("department");
        options.Select.Add("owner");
        options.Select.Add("status");
        options.Select.Add("workflowState");
        options.Select.Add("retentionClass");
        options.Select.Add("blobPath");
        options.Select.Add("createdDate");
        options.Select.Add("expiryDate");

        var response = await searchClient.SearchAsync<SearchDocument>(query, options);

        var results = new List<object>();

        await foreach (var item in response.Value.GetResultsAsync())
        {
            var content = item.Document.Content ?? "";

            results.Add(new
            {
                item.Score,
                item.Document.Id,
                item.Document.DocumentId,
                item.Document.FileName,
                item.Document.DocumentType,
                item.Document.Customer,
                item.Document.Project,
                item.Document.Department,
                item.Document.Owner,
                item.Document.Status,
                item.Document.WorkflowState,
                item.Document.RetentionClass,
                item.Document.BlobPath,
                item.Document.CreatedDate,
                item.Document.ExpiryDate,
                Snippet = content.Length > 700 ? content[..700] : content
            });
        }

        return JsonSerializer.Serialize(results, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    try
    {
        if (!string.IsNullOrWhiteSpace(filter) &&
            filter.Contains("contains", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Unsupported filter received: {filter}");
            filter = null;
        }

        return await RunSearchAsync(filter);
    }
    catch (RequestFailedException ex) when (
        ex.Status == 400 &&
        ex.Message.Contains("$filter", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Invalid Azure Search filter. Retrying without filter.");
        Console.WriteLine(ex.Message);

        return await RunSearchAsync(null);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Azure AI Search failed:");
        Console.WriteLine(ex);

        return JsonSerializer.Serialize(new
        {
            error = "Azure AI Search failed.",
            message = ex.Message
        });
    }
}

[Description("Prepares a document workflow state change. Requires human confirmation before execution.")]
Task<string> PrepareWorkflowChangeAsync(
    string documentId,
    string currentState,
    string requestedState)
{
    var allowed = new Dictionary<string, string[]>
    {
        ["Draft"] = ["In Review", "Rejected"],
        ["In Review"] = ["Approved", "Rejected"],
        ["Approved"] = ["Archived"],
        ["Rejected"] = ["Draft"]
    };

    if (!allowed.TryGetValue(currentState, out var nextStates) ||
        !nextStates.Contains(requestedState))
    {
        return Task.FromResult(
            $"Invalid workflow transition from {currentState} to {requestedState}.");
    }

    return Task.FromResult($"""
    Workflow change prepared.

    DocumentId: {documentId}
    Current state: {currentState}
    Requested state: {requestedState}

    Human approval is required before applying this change.
    """);
}

var tools = new List<AITool>
{
    AIFunctionFactory.Create(SearchDocumentsAsync),
    AIFunctionFactory.Create(PrepareWorkflowChangeAsync)
};

IChatClient chatClient = new AzureOpenAIClient(
        new Uri(endpoint),
        credential)
    .GetChatClient(deploymentName)
    .AsIChatClient();

AIAgent documentManagerAgent = chatClient.AsAIAgent(new ChatClientAgentOptions
{
    Name = "DocumentManagerAgent",
    ChatOptions = new ChatOptions
    {
        Instructions = """
        You are a metadata-driven Document Manager Agent.

        Your role is similar to a lightweight M-Files style document assistant.

        You help users:
        - Find documents by business metadata.
        - Search document content.
        - Summarize matching documents.
        - Explain document status and workflow state.
        - Prepare workflow transitions.

        Always use SearchDocumentsAsync for:
        - contracts
        - invoices
        - HR policies
        - project documents
        - compliance documents
        - document status questions
        - document workflow questions
        - questions about document content

        Metadata fields include:
        - id
        - documentId
        - fileName
        - documentType
        - customer
        - project
        - department
        - owner
        - status
        - workflowState
        - retentionClass

        Rules:
        - Do not invent documents.
        - Do not answer from memory.
        - Use only SearchDocumentsAsync results.
        - Cite available fields: fileName, documentId, id, status, and workflowState.
        - If fileName or documentId is empty, use id.
        - If no documents are found, say so clearly.
        - Do not approve, archive, delete, or modify documents automatically.
        - Workflow changes must be prepared only and require human approval.

        Filter rules:
        - Only use Azure AI Search OData filters.
        - Never use contains().
        - For text search, put the text in the query parameter.
        - Use filters only for exact metadata fields, for example:
        customer eq 'Contoso'
        status eq 'Approved'
        workflowState eq 'In Review'
        documentType eq 'Contract'
        
        """,
        Tools = tools
    }
});

app.MapGet("/.well-known/agent.json", () =>
{
    return Results.Ok(new AgentCard
    {
        Name = "DocumentManagerAgent",
        Description = "Metadata-driven enterprise document manager for search, summaries, workflow state, and compliance-style document discovery.",
        Version = "1.0.0",
        Endpoint = "http://localhost:5010/api/agent/ask",
        Capabilities =
        [
            "documents",
            "metadata-search",
            "enterprise-content-management",
            "workflow",
            "contracts",
            "compliance"
        ]
    });
});

app.MapPost("/api/agent/ask", async (AgentRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Question))
    {
        return Results.BadRequest(new { error = "Question is required." });
    }

    Console.WriteLine($"Received question: {request.Question}");

    var response = await documentManagerAgent.RunAsync<string>(request.Question);

    Console.WriteLine($"Received response: {response.Result}");


    return Results.Ok(new
    {
        answer = response.Result
    });
});

app.Run("http://localhost:5010");