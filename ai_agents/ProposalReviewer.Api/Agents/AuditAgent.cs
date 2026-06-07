using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Agents;

public sealed class AuditAgent
{
    private readonly CosmosAuditService _cosmos;

    public AuditAgent(CosmosAuditService cosmos)
    {
        _cosmos = cosmos;
    }

    public async Task RecordAsync(
        ProposalReviewState state,
        string eventName,
        string? message,
        CancellationToken cancellationToken)
    {
        await _cosmos.SaveStateAsync(state, cancellationToken);

        await _cosmos.SaveAuditEventAsync(
            new AuditEvent
            {
                ProposalId = state.ProposalId,
                EventName = eventName,
                Status = state.Status.ToString(),
                RequestedBy = state.RequestedBy,
                SelectedScopes = state.SelectedScopes.Select(x => x.ToString()).ToList(),
                Message = message
            },
            cancellationToken);
    }
}