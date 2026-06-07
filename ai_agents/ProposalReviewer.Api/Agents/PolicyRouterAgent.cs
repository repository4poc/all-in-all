using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Agents;

public sealed class PolicyRouterAgent
{
    private readonly OpenAIChatService _chat;

    public PolicyRouterAgent(OpenAIChatService chat)
    {
        _chat = chat;
    }

    public async Task<List<PolicyScope>> RouteAsync(
        ProposalReviewRequest request,
        string maskedProposalText,
        CancellationToken cancellationToken)
    {
        if (request.RequestedScopes.Count > 0)
        {
            return request.RequestedScopes;
        }

        var systemPrompt = """
        You are a policy review router.

        Choose which policy agents should review the proposal.

        Available scopes:
        - Gdpr
        - Hipaa
        - Security
        - CompanyPolicy

        Rules:
        - If user asks GDPR only, return Gdpr only.
        - If user asks HIPAA only, return Hipaa only.
        - If user asks security only, return Security only.
        - If user asks company policy only, return CompanyPolicy only.
        - If user asks compliance, return Gdpr, Hipaa, CompanyPolicy.
        - If user asks full review, return all.
        - If unclear, return all.

        Return JSON only:
        {
          "scopes": ["Gdpr", "Security"]
        }
        """;

        var userPrompt = $"""
        User query:
        {request.UserQuery}

        Proposal excerpt:
        {maskedProposalText[..Math.Min(maskedProposalText.Length, 3000)]}
        """;

        var result = await _chat.GetJsonAsync<PolicyRouterResult>(
            systemPrompt,
            userPrompt,
            cancellationToken);

        return result.Scopes.Count > 0
            ? result.Scopes
            :
            [
                PolicyScope.Gdpr,
                PolicyScope.Hipaa,
                PolicyScope.Security,
                PolicyScope.CompanyPolicy
            ];
    }
}