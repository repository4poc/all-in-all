using System.Security.Cryptography;
using System.Text;
using ProposalReviewer.Api.Agents;
using ProposalReviewer.Api.Models;
using ProposalReviewer.Api.Services;

namespace ProposalReviewer.Api.Workflow;

public sealed class ProposalReviewWorkflow
{
    private readonly InputGuardrailAgent _inputGuardrail;
    private readonly PiiMaskingService _piiMasking;
    private readonly PolicyRouterAgent _router;
    private readonly GdprReviewerAgent _gdprAgent;
    private readonly HipaaReviewerAgent _hipaaAgent;
    private readonly SecurityReviewerAgent _securityAgent;
    private readonly CompanyPolicyReviewerAgent _companyPolicyAgent;
    private readonly ResultValidatorAgent _validator;
    private readonly ConsolidatorAgent _consolidator;
    private readonly OutputGuardrailAgent _outputGuardrail;
    private readonly AuditAgent _auditAgent;
    private readonly CosmosAuditService _cosmos;

    public ProposalReviewWorkflow(
        InputGuardrailAgent inputGuardrail,
        PiiMaskingService piiMasking,
        PolicyRouterAgent router,
        GdprReviewerAgent gdprAgent,
        HipaaReviewerAgent hipaaAgent,
        SecurityReviewerAgent securityAgent,
        CompanyPolicyReviewerAgent companyPolicyAgent,
        ResultValidatorAgent validator,
        ConsolidatorAgent consolidator,
        OutputGuardrailAgent outputGuardrail,
        AuditAgent auditAgent,
        CosmosAuditService cosmos)
    {
        _inputGuardrail = inputGuardrail;
        _piiMasking = piiMasking;
        _router = router;
        _gdprAgent = gdprAgent;
        _hipaaAgent = hipaaAgent;
        _securityAgent = securityAgent;
        _companyPolicyAgent = companyPolicyAgent;
        _validator = validator;
        _consolidator = consolidator;
        _outputGuardrail = outputGuardrail;
        _auditAgent = auditAgent;
        _cosmos = cosmos;
    }

    public async Task<ProposalReviewResult> RunAsync(
        ProposalReviewRequest request,
        CancellationToken cancellationToken)
    {
        var state = new ProposalReviewState
        {
            ProposalId = request.ProposalId,
            RequestedBy = request.RequestedBy,
            OriginalQuery = request.UserQuery,
            OriginalProposalTextHash = Sha256(request.ProposalText)
        };

        await _auditAgent.RecordAsync(
            state,
            "WorkflowStarted",
            "Proposal review workflow started.",
            cancellationToken);

        try
        {
            var inputGuardrail = await _inputGuardrail.ValidateAsync(
                request,
                cancellationToken);

            if (!inputGuardrail.IsAllowed)
            {
                state.Status = ProposalReviewStatus.Failed;
                state.FailureReason = inputGuardrail.Reason;

                await _auditAgent.RecordAsync(
                    state,
                    "InputGuardrailBlocked",
                    inputGuardrail.Reason,
                    cancellationToken);

                throw new InvalidOperationException(inputGuardrail.Reason);
            }

            state.Status = ProposalReviewStatus.InputGuardrailPassed;

            await _auditAgent.RecordAsync(
                state,
                "InputGuardrailPassed",
                "Input guardrail passed.",
                cancellationToken);

            state.MaskedProposalText = await _piiMasking.MaskAsync(
                request.ProposalText,
                cancellationToken);

            state.Status = ProposalReviewStatus.PiiMasked;

            await _auditAgent.RecordAsync(
                state,
                "PiiMasked",
                "PII masking completed.",
                cancellationToken);

            state.SelectedScopes = await _router.RouteAsync(
                request,
                state.MaskedProposalText,
                cancellationToken);

            state.Status = ProposalReviewStatus.Routed;

            await _auditAgent.RecordAsync(
                state,
                "PolicyRoutingCompleted",
                $"Selected scopes: {string.Join(", ", state.SelectedScopes)}",
                cancellationToken);

            var reviewerTasks = new List<Task<PolicyReviewResult>>();

            if (state.SelectedScopes.Contains(PolicyScope.Gdpr))
            {
                reviewerTasks.Add(_gdprAgent.ReviewAsync(state, cancellationToken));
            }

            if (state.SelectedScopes.Contains(PolicyScope.Hipaa))
            {
                reviewerTasks.Add(_hipaaAgent.ReviewAsync(state, cancellationToken));
            }

            if (state.SelectedScopes.Contains(PolicyScope.Security))
            {
                reviewerTasks.Add(_securityAgent.ReviewAsync(state, cancellationToken));
            }

            if (state.SelectedScopes.Contains(PolicyScope.CompanyPolicy))
            {
                reviewerTasks.Add(_companyPolicyAgent.ReviewAsync(state, cancellationToken));
            }

            state.PolicyResults = (await Task.WhenAll(reviewerTasks)).ToList();

            state.Status = ProposalReviewStatus.PolicyReviewCompleted;

            await _auditAgent.RecordAsync(
                state,
                "PolicyReviewsCompleted",
                "Specialist policy reviews completed.",
                cancellationToken);

            var validation = await _validator.ValidateAsync(
                state,
                cancellationToken);

            if (!validation.IsValid)
            {
                state.Status = ProposalReviewStatus.RequiresHumanReview;
                state.RequiresHumanReview = true;
                state.FailureReason = validation.Reason;

                await _auditAgent.RecordAsync(
                    state,
                    "ValidationFailed",
                    validation.Reason,
                    cancellationToken);

                throw new InvalidOperationException(
                    $"Review validation failed: {validation.Reason}");
            }

            state.Status = ProposalReviewStatus.ValidationPassed;

            await _auditAgent.RecordAsync(
                state,
                "ValidationPassed",
                "Result validation passed.",
                cancellationToken);

            state.FinalResult = await _consolidator.ConsolidateAsync(
                state,
                cancellationToken);

            state.FinalResult.ProposalId = state.ProposalId;
            state.FinalResult.ReviewScope = state.SelectedScopes
                .Select(x => x.ToString())
                .ToList();

            state.Status = ProposalReviewStatus.Consolidated;

            await _auditAgent.RecordAsync(
                state,
                "Consolidated",
                "Final result consolidated.",
                cancellationToken);

            var outputGuardrail = await _outputGuardrail.ValidateAsync(
                state.FinalResult,
                cancellationToken);

            if (!outputGuardrail.IsAllowed)
            {
                state.Status = ProposalReviewStatus.RequiresHumanReview;
                state.RequiresHumanReview = true;
                state.FailureReason = outputGuardrail.Reason;

                await _auditAgent.RecordAsync(
                    state,
                    "OutputGuardrailBlocked",
                    outputGuardrail.Reason,
                    cancellationToken);

                throw new InvalidOperationException(outputGuardrail.Reason);
            }

            state.Status = ProposalReviewStatus.OutputGuardrailPassed;

            await _auditAgent.RecordAsync(
                state,
                "OutputGuardrailPassed",
                "Output guardrail passed.",
                cancellationToken);

            await _cosmos.SaveResultAsync(
                state.ProposalId,
                state.FinalResult,
                cancellationToken);

            state.Status = ProposalReviewStatus.Completed;

            await _auditAgent.RecordAsync(
                state,
                "WorkflowCompleted",
                "Proposal review completed.",
                cancellationToken);

            return state.FinalResult;
        }
        catch (Exception ex)
        {
            state.Status = ProposalReviewStatus.Failed;
            state.FailureReason = ex.Message;

            await _auditAgent.RecordAsync(
                state,
                "WorkflowFailed",
                ex.Message,
                cancellationToken);

            throw;
        }
    }

    private static string Sha256(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes);
    }
}