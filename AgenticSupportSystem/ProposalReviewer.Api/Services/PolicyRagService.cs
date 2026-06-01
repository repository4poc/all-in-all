using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Options;
using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Options;

namespace ProposalReviewer.Api.Services;

public sealed class PolicyRagService
{
    private readonly AzureOpenAIClient _openAIClient;
    private readonly AzureOpenAIOptions _openAIOptions;
    private readonly AzureSearchOptions _searchOptions;

    public PolicyRagService(
        IOptions<AzureOpenAIOptions> openAIOptions,
        IOptions<AzureSearchOptions> searchOptions)
    {
        _openAIOptions = openAIOptions.Value;
        _searchOptions = searchOptions.Value;

        _openAIClient = new AzureOpenAIClient(
            new Uri(_openAIOptions.Endpoint),
            new AzureCliCredential());
    }

    public async Task<IReadOnlyList<PolicyChunk>> RetrieveAsync(
        PolicyScope scope,
        string query,
        CancellationToken cancellationToken)
    {
        var indexName = GetIndexName(scope);

        var searchClient = new SearchClient(
            new Uri(_searchOptions.Endpoint),
            indexName,
            new AzureKeyCredential(_searchOptions.ApiKey));

        var queryEmbedding = await CreateEmbeddingAsync(
            query,
            cancellationToken);

        var vectorQuery = new VectorizedQuery(queryEmbedding)
        {
            KNearestNeighborsCount = 8,
            Fields = { _searchOptions.VectorFieldName }
        };

        var options = new SearchOptions
        {
            Size = 8,
            QueryType = SearchQueryType.Semantic,
            SemanticSearch = new SemanticSearchOptions
            {
                SemanticConfigurationName = "default"
            }
        };

        options.VectorSearch = new VectorSearchOptions
        {
            Queries = { vectorQuery }
        };

        options.Select.Add("id");
        options.Select.Add("scope");
        options.Select.Add("title");
        options.Select.Add("section");
        options.Select.Add("source");
        options.Select.Add("policyVersion");
        options.Select.Add("content");

        var response = await searchClient.SearchAsync<SearchDocument>(
            query,
            options,
            cancellationToken);

        var chunks = new List<PolicyChunk>();

        await foreach (var result in response.Value.GetResultsAsync())
        {
            var doc = result.Document;

            chunks.Add(new PolicyChunk
            {
                Id = GetString(doc, "id"),
                Scope = GetString(doc, "scope"),
                Title = GetString(doc, "title"),
                Section = GetString(doc, "section"),
                Source = GetString(doc, "source"),
                PolicyVersion = GetString(doc, "policyVersion"),
                Content = GetString(doc, "content"),
                Score = result.Score
            });
        }

        return chunks;
    }

    private async Task<ReadOnlyMemory<float>> CreateEmbeddingAsync(
        string input,
        CancellationToken cancellationToken)
    {
        var embeddingClient = _openAIClient.GetEmbeddingClient(
            _openAIOptions.EmbeddingDeploymentName);

        var response = await embeddingClient.GenerateEmbeddingAsync(
            input,
            cancellationToken: cancellationToken);

        return response.Value.ToFloats();
    }

    private string GetIndexName(PolicyScope scope)
    {
        return scope switch
        {
            PolicyScope.Gdpr => _searchOptions.GdprIndexName,
            PolicyScope.Hipaa => _searchOptions.HipaaIndexName,
            PolicyScope.Security => _searchOptions.SecurityIndexName,
            PolicyScope.CompanyPolicy => _searchOptions.CompanyPolicyIndexName,
            _ => throw new ArgumentOutOfRangeException(nameof(scope))
        };
    }

    private static string GetString(SearchDocument document, string key)
    {
        return document.TryGetValue(key, out var value)
            ? value?.ToString() ?? string.Empty
            : string.Empty;
    }
}