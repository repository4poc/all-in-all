using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Agents;

public sealed class SecurityReviewerAgent
{
    private readonly PolicyRagService _rag;
    private readonly OpenAIChatService _chat;

    public SecurityReviewerAgent(PolicyRagService rag, OpenAIChatService chat)
    {
        _rag = rag;
        _chat = chat;
    }

    public async Task<PolicyReviewResult> ReviewAsync(
        ProposalReviewState state,
        CancellationToken cancellationToken)
    {
        var chunks = await _rag.RetrieveAsync(
            PolicyScope.Security,
            state.MaskedProposalText,
            cancellationToken);

        var systemPrompt = """
        You are an enterprise security proposal reviewer.

        Review security controls, encryption, access control, logging, network security,
        incident response, data retention, and operational risk.

        Use only retrieved policy evidence.
        Return JSON only:
        {
          "scope": "Security",
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

        result.Scope = "Security";
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