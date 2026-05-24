using Orchestrator.Api.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Orchestrator.Api.Services;

public sealed class RemoteAgentClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoteAgentClient(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AgentCard> DiscoverAgentAsync(string baseUrl)
    {
        var http = _httpClientFactory.CreateClient();

        var card = await http.GetFromJsonAsync<AgentCard>(
            $"{baseUrl.TrimEnd('/')}/.well-known/agent.json");

        if (card is null)
        {
            throw new InvalidOperationException(
                $"Could not discover agent at {baseUrl}");
        }

        return card;
    }

    public async Task<string> AskAgentAsync(
        string baseUrl,
        string question,
        string? subscriptionId = null)
    {
        var http = _httpClientFactory.CreateClient();

        var card = await DiscoverAgentAsync(baseUrl);

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            card.Endpoint)
        {
            Content = JsonContent.Create(new AgentRequest
            {
                Question = question,
                SubscriptionId = subscriptionId ?? ""
            })
        };

        //
        // FORWARD USER ACCESS TOKEN
        //
        var authHeader =
            _httpContextAccessor
                .HttpContext?
                .Request
                .Headers
                .Authorization
                .ToString();

        if (!string.IsNullOrWhiteSpace(authHeader) &&
            authHeader.StartsWith("Bearer "))
        {
            request.Headers.Authorization =
                AuthenticationHeaderValue.Parse(authHeader);
        }

        using var response =
            await http.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error =
                await response.Content.ReadAsStringAsync();

            return $"{card.Name} failed with status {response.StatusCode}. Details: {error}";
        }

        var payload =
            await response.Content.ReadFromJsonAsync<AgentResponse>();

        return payload?.Answer
            ?? $"{card.Name} returned no answer.";
    }
}