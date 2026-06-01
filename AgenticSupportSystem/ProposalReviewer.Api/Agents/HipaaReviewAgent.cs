using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Agents;

public sealed class HipaaReviewerAgent
{
    private readonly PolicyRagService _rag;
    private readonly OpenAIChatService _chat;

    public HipaaReviewerAgent(PolicyRagService rag, OpenAIChatService chat)
    {
        _rag = rag;
        _chat = chat;
    }

    public async Task<PolicyReviewResult> ReviewAsync(
        ProposalReviewState state,
        CancellationToken cancellationToken)
    {
        var chunks = await _rag.RetrieveAsync(
            PolicyScope.Hipaa,
            state.MaskedProposalText,
            cancellationToken);

        var systemPrompt = """
        You are a HIPAA proposal reviewer.

        Review only HIPAA/PHI/privacy/security rule related risks.
        Use only retrieved policy evidence.
        Return JSON only with:
        {
          "scope": "HIPAA",
          "riskLevel": "",
          "summary": "",
          "findings": [],
          "citations": []
        }
        """;

        var userPrompt = BuildPrompt(state, chunks);

        var result = await _chat.GetJsonAsync<PolicyReviewResult>(
            systemPrompt,
            userPrompt,
            cancellationToken);

        result.Scope = "HIPAA";
        result.Citations = chunks.ToList();

        return result;
    }

    private static string BuildPrompt(
        ProposalReviewState state,
        IReadOnlyList<PolicyChunk> chunks)
    {
        var evidence = string.Join(
            "\n\n",
            chunks.Select(c =>
                $"Source={c.Source}; Version={c.PolicyVersion}; Section={c.Section}; Title={c.Title}\n{c.Content}"));

        return $"""
        Proposal:
        {state.MaskedProposalText}

        Retrieved policy evidence:
        {evidence}
        """;
    }
}