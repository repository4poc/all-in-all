using System;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.ComponentModel;


[Description("Get the weather for a given location.")]
static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";


var mcpHostedTool = new HostedMcpServerTool(
    serverName: "microsoft_learn",
    serverAddress: "https://learn.microsoft.com/api/mcp")
{
    AllowedTools = ["microsoft_docs_search"],
    ApprovalMode = HostedMcpServerToolApprovalMode.NeverRequire
};

// Hosted Code Interpreter Tool
var codeInterpreterTool = new HostedCodeInterpreterTool();
var webSearchTool = new HostedWebSearchTool();
var imageGeneratorTool = new HostedImageGenerationTool
{
    Options = new ImageGenerationOptions
    {
        MediaType = "image/png",
        Count = 1,
        ImageSize = new System.Drawing.Size(1024, 1024)
    }
};

AIAgent agent = new AIProjectClient(
        new Uri("https://foundryallinalldev007.openai.azure.com"),
        new AzureCliCredential())
    .AsAIAgent(
        model: "gpt-5-deployment",
        instructions: """
        You are a helpful assistant.
        You can:
        - call local C# functions,
        - use hosted code interpreter for calculations and Python-style data analysis,
        - use Microsoft Learn MCP search.
        - use web search for current and web search for information.
        - use image generation to create images based on user requests.
        """,
        name: "FriendlyAssistant",
        tools: [AIFunctionFactory.Create(GetWeather), codeInterpreterTool, mcpHostedTool, webSearchTool, imageGeneratorTool]);

try
{
    await foreach (var update in agent.RunStreamingAsync(
    "Generate one PNG image of a futuristic Helsinki harbor at sunset."))
    {
        Console.WriteLine(update);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

