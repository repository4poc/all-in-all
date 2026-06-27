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
using Azure.Search.Documents.Models;
using OpenAI.Embeddings;

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

var vectorFieldName =
    builder.Configuration["AzureSearch:VectorFieldName"]
    ?? "contentVector";

var credential = new AzureCliCredential();

builder.Services.AddSingleton(new SearchClient(
    new Uri(searchEndpoint),
    searchIndex,
    credential));

var embeddingDeploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_EMBEDDING_DEPLOYMENT_NAME")
    ?? builder.Configuration["AzureOpenAI:EmbeddingDeploymentName"]
    ?? "text-embedding-3-mini";

builder.Services.AddSingleton(sp =>
{
    return new AzureOpenAIClient(new Uri(endpoint), credential)
        .GetEmbeddingClient(embeddingDeploymentName);
});

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



[Description("Searches documents using vector similarity over document content. Use this for semantic questions where exact keywords may not match, including joining date, start date, employment date, contract facts, obligations, risks, penalties, payment delays, termination rights, renewal terms, or compliance meaning.")]
async Task<string> SearchDocumentsByVectorAsync(
    [Description("Natural language semantic query, for example: contracts with payment delay penalties")] string query,
    [Description("Optional OData metadata filter, for example: customer eq 'Contoso' and status eq 'Approved'")] string? filter = null)
{
    var searchClient = app.Services.GetRequiredService<SearchClient>();
    var embeddingClient = app.Services.GetRequiredService<EmbeddingClient>();

    var embeddingResponse = await embeddingClient.GenerateEmbeddingAsync(query);
    var queryVector = embeddingResponse.Value.ToFloats();

    var options = new SearchOptions
    {
        Size = 5,
        Filter = string.IsNullOrWhiteSpace(filter) ? null : filter
    };

    options.Select.Add("id");
    options.Select.Add("documentId");
    options.Select.Add("fileName");
    options.Select.Add("content");
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

    options.VectorSearch = new()
    {
        Queries =
        {
            new VectorizedQuery(queryVector)
            {
                KNearestNeighborsCount = 5,
                Fields = { vectorFieldName }
            }
        }
    };

    var response = await searchClient.SearchAsync<DocumentManager.Api.Models.SearchDocument>(null, options);

    var results = new List<object>();

    await foreach (var item in response.Value.GetResultsAsync())
    {
        var content = item.Document.content ?? "";

        results.Add(new
        {
            item.Score,
            item.Document.id,
            item.Document.documentId,
            item.Document.fileName,
            item.Document.documentType,
            item.Document.customer,
            item.Document.project,
            item.Document.department,
            item.Document.owner,
            item.Document.status,
            item.Document.workflowState,
            item.Document.retentionClass,
            item.Document.blobPath,
            item.Document.createdDate,
            item.Document.expiryDate,
            Snippet = content.Length > 700 ? content[..700] : content
        });
    }

    return JsonSerializer.Serialize(results, new JsonSerializerOptions
    {
        WriteIndented = true
    });
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
        options.Select.Add("content");
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

        var response = await searchClient.SearchAsync<DocumentManager.Api.Models.SearchDocument>(query, options);

        var results = new List<object>();

        await foreach (var item in response.Value.GetResultsAsync())
        {
            var content = item.Document.content ?? "";

            results.Add(new
            {
                item.Score,
                item.Document.id,
                item.Document.documentId,
                item.Document.fileName,
                item.Document.documentType,
                item.Document.customer,
                item.Document.project,
                item.Document.department,
                item.Document.owner,
                item.Document.status,
                item.Document.workflowState,
                item.Document.retentionClass,
                item.Document.blobPath,
                item.Document.createdDate,
                item.Document.expiryDate,
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

[Description("Searches documents using only metadata filters. Use this when the user asks by customer, project, status, workflow state, owner, department, retention class, document type, or expiry date.")]
async Task<string> SearchDocumentsByMetadataAsync(
    [Description("Azure AI Search OData filter, for example: customer eq 'Contoso' and status eq 'Approved'")] string filter)
{
    var searchClient = app.Services.GetRequiredService<SearchClient>();

    var options = new SearchOptions
    {
        Size = 20,
        Filter = filter
    };

    options.Select.Add("id");
    options.Select.Add("documentId");
    options.Select.Add("fileName");
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

    var response = await searchClient.SearchAsync<DocumentManager.Api.Models.SearchDocument>("*", options);

    var results = new List<object>();

    await foreach (var item in response.Value.GetResultsAsync())
    {
        results.Add(new
        {
            item.Score,
            item.Document.id,
            item.Document.documentId,
            item.Document.fileName,
            item.Document.documentType,
            item.Document.customer,
            item.Document.project,
            item.Document.department,
            item.Document.owner,
            item.Document.status,
            item.Document.workflowState,
            item.Document.retentionClass,
            item.Document.blobPath,
            item.Document.createdDate,
            item.Document.expiryDate
        });
    }

    return JsonSerializer.Serialize(results, new JsonSerializerOptions
    {
        WriteIndented = true
    });
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
    AIFunctionFactory.Create(SearchDocumentsByMetadataAsync),
    AIFunctionFactory.Create(SearchDocumentsByVectorAsync),
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

        Your role is similar to a lightweight document assistant.

        You help users:
        - Find documents by business metadata.
        - Search document content.
        - Summarize matching documents.
        - Explain document status and workflow state.
        - Prepare workflow transitions.

        Use SearchDocumentsByMetadataAsync when the user asks about:
        - documents by customer
        - documents by project
        - documents by owner
        - documents by status
        - documents by workflow state
        - documents by department
        - documents by document type
        - documents by retention class
        - documents by created date
        - documents by expiry date

        Use SearchDocumentsByMetadataAsync when the user asks only by metadata fields.

        Use SearchDocumentsByVectorAsync for semantic/content questions where the answer may be inside document text, including:
        - joining date
        - start date
        - employment date
        - effective date
        - contract date
        - date mentioned in a document
        - who joined which company
        - facts that are inside document content, not metadata
        - obligations, risks, penalties, payment delays, termination rights, renewal terms, or compliance meaning

        If the user asks a factual question and the answer is not metadata:
        1. Call SearchDocumentsByVectorAsync first.
        2. If evidence is weak, call SearchDocumentsAsync with likely keywords.
        3. Answer only from returned Snippet/content.
        4. Do not say metadata is missing unless the user specifically asked about metadata.

        Use SearchDocumentsAsync when the user asks for exact keyword/content search or when the user gives exact phrases.
        
        Rules:
        - Do not invent documents.
        - Do not answer from memory.
        - Cite available fields: fileName, blobURL , status, and workflowState.
        - If there are multiple results, only show top record each distinct blobURL in a bullet point 
        - Do not use markdown tables. Use bullet points only.
        - If fileName or documentId is empty, use id.
        - If no documents are found, say so clearly.
        - Do not approve, archive, delete, or modify documents automatically.
        - Workflow changes must be prepared only and require human approval.
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