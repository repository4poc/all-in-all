## Flow

1. Document Uploaded Workflow

```
Blob Uploaded
  ↓
Event Grid
  ↓
Service Bus: blob-queue/topic
  ↓
Workflow Function
  ↓
Create/Update Cosmos DB document state
  ↓
Update Blob metadata: status=approval-pending
  ↓
Publish Service Bus message: status=approval-pending
  ↓
Workflow Function
  ↓
Send approval notification
  ↓
Update Cosmos DB: workflowState=approval-email-sent
```

2. Approval/Reject Workflow

```
For approval:

User clicks approve/reject
  ↓
Approval API
  ↓
Update Cosmos DB state
  ↓
Update Blob metadata
  ↓
Publish Service Bus message: approved/rejected
  ↓
Workflow Function
  ↓
Send final notification
```
