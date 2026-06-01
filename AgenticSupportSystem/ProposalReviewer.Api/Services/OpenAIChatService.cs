using System.Text.Json;
using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using ProposalReviewer.Api.Options;

namespace ProposalReviewer.Api.Services;

public sealed class OpenAIChatService
{
    private readonly IChatClient _chatClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public OpenAIChatService(IOptions<AzureOpenAIOptions> options)
    {
        var value = options.Value;

        _chatClient = new AzureOpenAIClient(
                new Uri(value.Endpoint),
                new AzureCliCredential())
            .GetChatClient(value.DeploymentName)
            .AsIChatClient();
    }

    public async Task<T> GetJsonAsync<T>(
        string systemPrompt,
        string userPrompt,
        CancellationToken cancellationToken)
    {
        var messages = new List<ChatMessage>
        {
            new(ChatRole.System, systemPrompt),
            new(ChatRole.User, userPrompt)
        };

        var response = await _chatClient.GetResponseAsync(
            messages,
            cancellationToken: cancellationToken);

        var text = response.Text.Trim();

        text = text
            .Replace("```json", string.Empty)
            .Replace("```", string.Empty)
            .Trim();

        var result = JsonSerializer.Deserialize<T>(text, JsonOptions);

        if (result is null)
        {
            throw new InvalidOperationException(
                $"Unable to deserialize LLM response to {typeof(T).Name}. Response: {text}");
        }

        return result;
    }
}