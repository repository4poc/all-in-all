using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Options;

namespace ProposalReviewer.Api.Services;

public sealed class CosmosAuditService
{
    private readonly Container _stateContainer;
    private readonly Container _auditContainer;
    private readonly Container _resultsContainer;

    public CosmosAuditService(
        CosmosClient cosmosClient,
        IOptions<CosmosOptions> options)
    {
        var value = options.Value;

        var database = cosmosClient.GetDatabase(value.DatabaseName);

        Console.WriteLine("-database-" + database);

        _stateContainer = database.GetContainer(value.WorkflowStateContainer);
        Console.WriteLine("-value.WorkflowStateContainer -" + value.WorkflowStateContainer);
        Console.WriteLine("-_stateContainer- " + _stateContainer);

        _auditContainer = database.GetContainer(value.AuditContainer);
        _resultsContainer = database.GetContainer(value.ResultsContainer);
    }

    public Task SaveStateAsync(
        ProposalReviewState state,
        CancellationToken cancellationToken)
    {
        state.UpdatedAt = DateTimeOffset.UtcNow;

        return _stateContainer.UpsertItemAsync(
            state,
            new PartitionKey(state.ProposalId),
            cancellationToken: cancellationToken);
    }

    public Task SaveAuditEventAsync(
        AuditEvent auditEvent,
        CancellationToken cancellationToken)
    {
        return _auditContainer.UpsertItemAsync(
            auditEvent,
            new PartitionKey(auditEvent.ProposalId),
            cancellationToken: cancellationToken);
    }

    public Task SaveResultAsync(
        string proposalId,
        ProposalReviewResult result,
        CancellationToken cancellationToken)
    {
        return _resultsContainer.UpsertItemAsync(
            result,
            new PartitionKey(proposalId),
            cancellationToken: cancellationToken);
    }
}