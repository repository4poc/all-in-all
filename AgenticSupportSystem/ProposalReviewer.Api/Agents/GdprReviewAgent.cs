using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Agents;

public sealed class GdprReviewerAgent
{
    private readonly PolicyRagService _rag;
    private readonly OpenAIChatService _chat;

    public GdprReviewerAgent(PolicyRagService rag, OpenAIChatService chat)
    {
        _rag = rag;
        _chat = chat;
    }

    public async Task<PolicyReviewResult> ReviewAsync(
        ProposalReviewState state,
        CancellationToken cancellationToken)
    {
        var chunks = await _rag.RetrieveAsync(
            PolicyScope.Gdpr,
            state.MaskedProposalText,
            cancellationToken);

        return await ReviewWithEvidenceAsync(
            "GDPR",
            PolicyScope.Gdpr,
            state,
            chunks,
            cancellationToken);
    }

    private async Task<PolicyReviewResult> ReviewWithEvidenceAsync(
        string scopeName,
        PolicyScope scope,
        ProposalReviewState state,
        IReadOnlyList<PolicyChunk> chunks,
        CancellationToken cancellationToken)
    {
        var systemPrompt = $$"""
        You are a {scopeName} proposal reviewer.

        Use only the retrieved policy evidence.
        Do not invent policy references.
        Every finding must have proposal evidence and policy citation.

        Return JSON only:
        {
          "scope": "{scopeName}",
          "riskLevel": "Low|Medium|High|Critical",
          "summary": "",
          "findings": [
            {"scope": "{scopeName}",
              "severity": "Low|Medium|High|Critical",
              "title": "",
              "evidenceFromProposal": "",
              "policyCitation": "",
              "recommendation": ""
            }
          ],
          "citations": []
        }
        """;

        var evidence = string.Join(
            "\n\n",
            chunks.Select(c =>
                $"Source={c.Source}; Version={c.PolicyVersion}; Section={c.Section}; Title={c.Title}\n{c.Content}"));

        var userPrompt = $"""
        Proposal:
        {state.MaskedProposalText}

        Retrieved policy evidence:
        {evidence}
        """;

        var result = await _chat.GetJsonAsync<PolicyReviewResult>(
            systemPrompt,
            userPrompt,
            cancellationToken);

        result.Scope = scopeName;
        result.Citations = chunks.ToList();

        return result;
    }
}