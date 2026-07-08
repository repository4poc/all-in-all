public record DocumentMessage(
    string DocumentId,
    string FileName,
    string BlobUrl,
    string ContainerName,
    string BlobName,
    string RequestedBy,
    string EventId,
    string EventType,
    DateTimeOffset EventTime,
    string CorrelationId);