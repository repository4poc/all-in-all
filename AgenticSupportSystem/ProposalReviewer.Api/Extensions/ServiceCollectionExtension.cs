using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using ProposalReviewer.Api.Agents;
using ProposalReviewer.Api.Options;
using ProposalReviewer.Api.Services;
using ProposalReviewer.Api.Workflow;

namespace ProposalReviewer.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProposalReviewerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AzureOpenAIOptions>(
            configuration.GetSection("AzureOpenAI"));

        services.Configure<AzureSearchOptions>(
            configuration.GetSection("AzureSearch"));

        services.Configure<AzureLanguageOptions>(
            configuration.GetSection("AzureLanguage"));

        services.Configure<CosmosOptions>(
            configuration.GetSection("Cosmos"));

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<CosmosOptions>>().Value;

            return new CosmosClient(
                options.Endpoint,
                new DefaultAzureCredential());
        });


        services.AddScoped<OpenAIChatService>();
        services.AddScoped<PiiMaskingService>();
        services.AddScoped<PolicyRagService>();
        services.AddScoped<CosmosAuditService>();

        services.AddScoped<InputGuardrailAgent>();
        services.AddScoped<PolicyRouterAgent>();
        services.AddScoped<GdprReviewerAgent>();
        services.AddScoped<HipaaReviewerAgent>();
        services.AddScoped<SecurityReviewerAgent>();
        services.AddScoped<CompanyPolicyReviewerAgent>();
        services.AddScoped<ResultValidatorAgent>();
        services.AddScoped<ConsolidatorAgent>();
        services.AddScoped<OutputGuardrailAgent>();
        services.AddScoped<AuditAgent>();

        services.AddScoped<ProposalReviewWorkflow>();

        return services;
    }
}