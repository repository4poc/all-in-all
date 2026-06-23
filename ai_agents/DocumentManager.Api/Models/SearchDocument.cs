namespace DocumentManager.Api.Models;

public sealed class SearchDocument
{
    public string? Id { get; set; }
    public string? Content { get; set; }
    public string DocumentId { get; set; } = "";
    public string FileName { get; set; } = "";
    public string DocumentType { get; set; } = "";
    public string Customer { get; set; } = "";
    public string Project { get; set; } = "";
    public string Owner { get; set; } = "";
    public string Status { get; set; } = "";
    public string WorkflowState { get; set; } = "";
    public string BlobPath { get; set; } = "";

    public string Department { get; set; } = "";
    public string RetentionClass { get; set; } = "";
    public DateTime CreatedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}