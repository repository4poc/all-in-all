using System.Text.Json;
using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Agents;

public sealed class ConsolidatorAgent
{
    private readonly OpenAIChatService _chat;

    public ConsolidatorAgent(OpenAIChatService chat)
    {
        _chat = chat;
    }

    public Task<ProposalReviewResult> ConsolidateAsync(
        ProposalReviewState state,
        CancellationToken cancellationToken)
    {
        var systemPrompt = """
        You are a senior enterprise proposal review consolidator.

        Consolidate specialist results.

        Rules:
        - deduplicate similar findings
        - preserve highest severity
        - preserve policy citations
        - create executive summary
        - create required actions
        - choose approvalRecommendation:
          Approved, ConditionalApproval, RequiresHumanReview, Rejected

        Return JSON only:
        {
          "proposalId": "",
          "reviewScope": [],
          "overallRisk": "",
          "approvalRecommendation": "",
          "executiveSummary": "",
          "findings": [],
          "requiredActions": []
        }
        """;

        var userPrompt = $"""
        ProposalId:
        {state.ProposalId}

        Selected scopes:
        {string.Join(", ", state.SelectedScopes)}

        Specialist results:
        {JsonSerializer.Serialize(state.PolicyResults)}
        """;

        return _chat.GetJsonAsync<ProposalReviewResult>(
            systemPrompt,
            userPrompt,
            cancellationToken);
    }
}