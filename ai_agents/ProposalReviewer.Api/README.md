## Overview

```
A complete end-to-end C# implementation for a new ProposalReviewer.Api service that integrate into your existing orchestrator.

It uses:

Azure AI Foundry / Azure OpenAI endpoint
Azure AI Search RAG
Azure AI Language PII masking
Cosmos DB audit + workflow state
Multi-stage workflow
Guardrail agent
Router agent
Parallel specialist policy agents
Validator agent
Consolidator agent
Audit agent

Remote agent endpoint compatible with your current orchestrator

Azure AI Search supports RAG, vector search, hybrid search, and semantic ranking for enterprise data grounding. Cosmos DB UpsertItemAsync is suitable for storing workflow state and audit records with partition keys. Azure AI Language PII redaction can detect and redact PII/PHI from unstructured text before sending it to an LLM.
```

## Project Structure

```
ProposalReviewer.Api/
├── Program.cs
├── appsettings.json
├── Models/
│   ├── AgentCard.cs
│   ├── AgentRequest.cs
│   ├── AuditEvent.cs
│   ├── GuardrailResult.cs
│   ├── PolicyChunk.cs
│   ├── PolicyFinding.cs
│   ├── PolicyReviewResult.cs
│   ├── PolicyRouterResult.cs
│   ├── PolicyScope.cs
│   ├── ProposalReviewRequest.cs
│   ├── ProposalReviewResult.cs
│   ├── ProposalReviewState.cs
│   ├── ProposalReviewStatus.cs
│   ├── RequiredAction.cs
│   └── ValidationResult.cs
├── Options/
│   ├── AzureLanguageOptions.cs
│   ├── AzureOpenAIOptions.cs
│   ├── AzureSearchOptions.cs
│   └── CosmosOptions.cs
├── Services/
│   ├── CosmosAuditService.cs
│   ├── OpenAIChatService.cs
│   ├── PiiMaskingService.cs
│   └── PolicyRagService.cs
├── Agents/
│   ├── AuditAgent.cs
│   ├── CompanyPolicyReviewerAgent.cs
│   ├── ConsolidatorAgent.cs
│   ├── GdprReviewerAgent.cs
│   ├── HipaaReviewerAgent.cs
│   ├── InputGuardrailAgent.cs
│   ├── OutputGuardrailAgent.cs
│   ├── PolicyRouterAgent.cs
│   ├── ResultValidatorAgent.cs
│   └── SecurityReviewerAgent.cs
├── Workflow/
│   └── ProposalReviewWorkflow.cs
└── Extensions/
    └── ServiceCollectionExtensions.cs

```

## Execution Table

| Step | Component                     |    Sequential / Parallel | Why                                       |
| ---- | ----------------------------- | -----------------------: | ----------------------------------------- |
| 1    | Create workflow state         |               Sequential | Needed before everything else             |
| 2    | Input Guardrail Agent         |               Sequential | Must block unsafe input before processing |
| 3    | PII Masking Service           |               Sequential | Must happen before LLM/RAG agents         |
| 4    | Policy Router Agent           |               Sequential | Decides which agents to call              |
| 5a   | GDPR Reviewer Agent           |                 Parallel | Independent policy review                 |
| 5b   | HIPAA Reviewer Agent          |                 Parallel | Independent policy review                 |
| 5c   | Security Reviewer Agent       |                 Parallel | Independent policy review                 |
| 5d   | Company Policy Reviewer Agent |                 Parallel | Independent policy review                 |
| 6    | Result Validator Agent        |               Sequential | Needs all reviewer outputs first          |
| 7    | Consolidator Agent            |               Sequential | Needs validated specialist results        |
| 8    | Output Guardrail Agent        |               Sequential | Needs final consolidated response         |
| 9    | Cosmos DB Audit Agent         | Sequential / side-effect | Runs after each stage                     |

## Azure AI Search

Create index with logical Schema

```
{
  "id": "string",
  "scope": "string",
  "title": "string",
  "section": "string",
  "source": "string",
  "policyVersion": "string",
  "effectiveDate": "string",
  "content": "string",
  "contentVector": "Collection(Edm.Single)"
}
```

Recommended Indexes

```
gdpr-policies
hipaa-policies
security-policies
company-policies
```

Each policy document from storage should be chunked and indexed with

```
{
  "id": "gdpr-001-004",
  "scope": "GDPR",
  "title": "Data Processing Agreement",
  "section": "4.2",
  "source": "GDPR Internal Policy",
  "policyVersion": "3.1",
  "effectiveDate": "2026-01-01",
  "content": "All vendors processing EU customer data must...",
  "contentVector": [0.012, -0.004, ...]
}
```

## CosmosDB COnfiguration

- Database : proposal-reviewer
- Containers :

```
workflow-state
partition key: /proposalId

audit-events
partition key: /proposalId

review-results
partition key: /proposalId

```

Important: your model currently uses ProposalId, but Cosmos JSON partition key is case-sensitive. To align perfectly, either use /ProposalId, or add JSON property attributes. I recommend adding attributes if you want lowercase JSON.

## Test Request

```
curl -X POST http://localhost:5006/api/proposal-review ^
  -H "Content-Type: application/json" ^
  -d "{\"proposalId\":\"p-001\",\"requestedBy\":\"varinder\",\"userQuery\":\"Review this proposal only for GDPR and security\",\"proposalText\":\"We will process EU customer names, emails, payment history and store data for 10 years. No DPA is mentioned. Admin access will be shared by the delivery team.\"}"
```

## Expected Output

```
{
  "proposalId": "p-001",
  "reviewScope": ["Gdpr", "Security"],
  "overallRisk": "High",
  "approvalRecommendation": "RequiresHumanReview",
  "executiveSummary": "...",
  "findings": [],
  "requiredActions": []
}
```

## For enterprise grade:

```
1. Use Managed Identity everywhere.
2. Replace AzureKeyCredential for Search and Language with DefaultAzureCredential if RBAC is configured.
3. Put all endpoints behind private endpoints.
4. Store original proposal only in Blob Storage, not Cosmos DB.
5. Store hash + masked text in Cosmos DB.
6. Add Application Insights.
7. Add retry policies with Polly.
8. Add content filtering and prompt-injection classifier.
9. Add human approval queue for Critical risk.
10. Add policy document versioning and effective-date filtering.
```

```

```

```

```
