namespace DocumentManager.Api.Models;

public sealed class SearchDocument
{
    public string? id { get; set; }
    public string? content { get; set; }
    public string? documentId { get; set; } = "";
    public string? fileName { get; set; } = "";
    public string? documentType { get; set; } = "";
    public string? customer { get; set; } = "";
    public string? project { get; set; } = "";
    public string? owner { get; set; } = "";
    public string? status { get; set; } = "";
    public string? workflowState { get; set; } = "";
    public string? blobPath { get; set; } = "";

    public string? department { get; set; } = "";
    public string? retentionClass { get; set; } = "";
    public DateTime? createdDate { get; set; }
    public DateTime? expiryDate { get; set; }
}