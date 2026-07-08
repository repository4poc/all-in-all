public record NotificationMessage(
    string DocumentId,
    string FileName,
    string BlobUrl,
    string RequestedBy,
    string NotificationType,
    string? Status,
    string CorrelationId);