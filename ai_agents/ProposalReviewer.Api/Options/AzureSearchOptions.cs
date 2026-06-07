namespace ProposalReviewer.Api.Options;

public sealed class AzureSearchOptions
{
    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string GdprIndexName { get; set; } = string.Empty;
    public string HipaaIndexName { get; set; } = string.Empty;
    public string SecurityIndexName { get; set; } = string.Empty;
    public string CompanyPolicyIndexName { get; set; } = string.Empty;
    public string VectorFieldName { get; set; } = "contentVector";
}