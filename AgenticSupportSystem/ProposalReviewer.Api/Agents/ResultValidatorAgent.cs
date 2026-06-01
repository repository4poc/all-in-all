using System.Text.Json;
using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Agents;

public sealed class ResultValidatorAgent
{
    private readonly OpenAIChatService _chat;

    public ResultValidatorAgent(OpenAIChatService chat)
    {
        _chat = chat;
    }

    public Task<ValidationResult> ValidateAsync(
        ProposalReviewState state,
        CancellationToken cancellationToken)
    {
        var systemPrompt = """
        You are a validation agent.

        Validate policy review results.

        Check:
        - every finding has evidenceFromProposal
        - every finding has policyCitation
        - severity is Low, Medium, High, or Critical
        - recommendation is actionable
        - no PII leakage
        - no unsupported policy claim

        Return JSON only:
        {
          "isValid": true,
          "reason": "",
          "issues": []
        }
        """;

        var userPrompt = JsonSerializer.Serialize(state.PolicyResults);

        return _chat.GetJsonAsync<ValidationResult>(
            systemPrompt,
            userPrompt,
            cancellationToken);
    }
}