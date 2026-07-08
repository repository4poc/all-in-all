using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function;

public class DocumentIngestionFunc
{
    private readonly ILogger<DocumentIngestionFunc> _logger;

    public DocumentIngestionFunc(ILogger<DocumentIngestionFunc> logger)
    {
        _logger = logger;
    }

    [Function(nameof(DocumentIngestionFunc))]
    public async Task Run(
        [ServiceBusTrigger("blobqueue", Connection = "")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Flow

        // if ( message.type = "Microsoft.Storage.BlobCreated")
        //   1. Create/Update Cosmos DB document state
        //   2. Update Blob metadata: status=approval-pending
        //   2. Update Cosmos DB: workflowState=approval-pending
        //   3. Publish Service Bus message ("type":"DocumentMetadataUpdated"): status=approval-pending

        // if ( message.type = "DocumentMetadataUpdated" && message.status = "approval-pending")
        //   1. Publish Service Bus message in the emailqueue, a separte flow will send an email
        //   2. Update Cosmos DB: workflowState=approval-email-sent







        await messageActions.CompleteMessageAsync(message);
    }
}