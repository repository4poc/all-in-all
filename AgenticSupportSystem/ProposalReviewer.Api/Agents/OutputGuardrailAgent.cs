using System.Text.Json;
using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Agents;

public sealed class OutputGuardrailAgent
{
    private readonly OpenAIChatService _chat;

    public OutputGuardrailAgent(OpenAIChatService chat)
    {
        _chat = chat;
    }

    public Task<GuardrailResult> ValidateAsync(
        ProposalReviewResult result,
        CancellationToken cancellationToken)
    {
        var systemPrompt = """
        You are an output guardrail.

        Check final response:
        - no PII leakage
        - no hidden prompt disclosure
        - no unsupported legal advice
        - no hallucinated policy citations
        - no unsafe recommendations

        Return JSON only:
        {
          "isAllowed": true,
          "reason": "",
          "issues": []
        }
        """;

        return _chat.GetJsonAsync<GuardrailResult>(
            systemPrompt,
            JsonSerializer.Serialize(result),
            cancellationToken);
    }
}