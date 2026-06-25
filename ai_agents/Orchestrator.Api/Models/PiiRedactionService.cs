using Azure;
using Azure.AI.TextAnalytics;
using Azure.Identity;

public sealed class PiiRedactionService
{
    private readonly TextAnalyticsClient _client;

    public PiiRedactionService(IConfiguration config)
    {
        var endpoint = config["AzureLanguage:Endpoint"]
            ?? throw new InvalidOperationException("AzureLanguage endpoint missing.");

        _client = new TextAnalyticsClient(
            new Uri(endpoint),
            new AzureCliCredential());
    }

    public async Task<string> RedactAsync(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        var response = await _client.RecognizePiiEntitiesAsync(input);

        return response.Value.RedactedText;
    }
}