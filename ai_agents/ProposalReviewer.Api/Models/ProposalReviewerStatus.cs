namespace ProposalReviewer.Api.Models;

public enum ProposalReviewStatus
{
    Created,
    InputGuardrailPassed,
    PiiMasked,
    Routed,
    PolicyReviewCompleted,
    ValidationPassed,
    Consolidated,
    OutputGuardrailPassed,
    Audited,
    Completed,
    Failed,
    RequiresHumanReview
}