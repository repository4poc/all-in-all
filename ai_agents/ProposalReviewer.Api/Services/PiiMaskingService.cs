using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Options;
using ProposalReviewer.Api.Options;

namespace ProposalReviewer.Api.Services;

public sealed class PiiMaskingService
{
    private readonly TextAnalyticsClient _client;

    public PiiMaskingService(IOptions<AzureLanguageOptions> options)
    {
        var value = options.Value;

        _client = new TextAnalyticsClient(
            new Uri(value.Endpoint),
            new AzureKeyCredential(value.ApiKey));
    }

    public async Task<string> MaskAsync(
        string input,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        var response = await _client.RecognizePiiEntitiesAsync(
            input,
            language: "en",
            cancellationToken: cancellationToken);

        return response.Value.RedactedText;
    }
}