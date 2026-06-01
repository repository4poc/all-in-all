using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI.Chat;
using ChatMessage = Microsoft.Extensions.AI.ChatMessage;

Console.WriteLine("--- Initializing Remote Mentorship Agent with Hosted MCP Tool ---");

// -----------------------------------------------------------------------------
// 1. Configure Azure OpenAI
// -----------------------------------------------------------------------------
var endpoint =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? "https://foundryallinalldev007.openai.azure.com";

var deploymentName =
    Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? "gpt-5-deployment";

// -----------------------------------------------------------------------------
// 2. Create Hosted MCP Tool
// -----------------------------------------------------------------------------
// This connects to the Microsoft Learn MCP endpoint and allows only the
// microsoft_docs_search tool to be used.
var mcpHostedTool = new HostedMcpServerTool(
    serverName: "microsoft_learn",
    serverAddress: "https://learn.microsoft.com/api/mcp")
{
    AllowedTools = ["microsoft_docs_search"],
    ApprovalMode = HostedMcpServerToolApprovalMode.AlwaysRequire
};

Console.WriteLine($"Tools available: {string.Join(", ", mcpHostedTool.AllowedTools)}");

// -----------------------------------------------------------------------------
// 3. Create Agent and attach Hosted MCP Tool
// -----------------------------------------------------------------------------
AIAgent mentorAgent = new AzureOpenAIClient(
        new Uri(endpoint),
        new AzureCliCredential())
    .GetChatClient(deploymentName)
    .AsAIAgent(
        name: "ArchitectureMentor",
        instructions: """
        You answer technical .NET questions.

        You MUST call the microsoft_docs_search tool before answering.
        Do not ask the user for permission in natural language.
        Do not say "do I have your permission".
        Call the tool directly.
        If approval is required, the application will handle approval through ToolApprovalRequestContent.
        """,
        tools: [mcpHostedTool]);

// -----------------------------------------------------------------------------
// 4. Create Stateful Session
// -----------------------------------------------------------------------------
AgentSession session = await mentorAgent.CreateSessionAsync();

string prompt =
    "What are the exact C# classes required to implement the Agent-to-Agent A2A protocol using the Microsoft.Agents.AI namespace?";

Console.WriteLine($"\nJunior Dev: {prompt}\n");

// -----------------------------------------------------------------------------
// 5. Initial Agent Run
// -----------------------------------------------------------------------------
AgentResponse response = await mentorAgent.RunAsync(prompt, session);

// -----------------------------------------------------------------------------
// 6. Human-in-the-loop approval loop for Hosted MCP tool calls
// -----------------------------------------------------------------------------
while (true)
{
    List<ToolApprovalRequestContent> approvalRequests = response.Messages
        .SelectMany(message => message.Contents)
        .OfType<ToolApprovalRequestContent>()
        .ToList();

    if (approvalRequests.Count == 0)
    {
        break;
    }

    foreach (ToolApprovalRequestContent approvalRequest in approvalRequests)
    {
        Console.WriteLine("\n[SECURITY AUDIT] The agent wants to execute a remote tool.");

        if (approvalRequest.ToolCall is McpServerToolCallContent mcpToolCall)
        {
            Console.WriteLine($"Target Server: {mcpToolCall.ServerName}");
            Console.WriteLine($"Target Tool:   {mcpToolCall.Name}");

            string arguments = string.Join(
                ", ",
                mcpToolCall.Arguments?.Select(argument =>
                    $"{argument.Key}: '{argument.Value}'") ?? []);

            Console.WriteLine($"Arguments:     {arguments}");
        }
        else
        {
            Console.WriteLine("Target Tool:   Unknown tool call payload");
        }

        Console.Write("\nApprove network request? (Y/N): ");

        ConsoleKey key;

        do
        {
            key = Console.ReadKey(intercept: true).Key;
        }
        while (key is not ConsoleKey.Y and not ConsoleKey.N);

        bool approved = key == ConsoleKey.Y;

        Console.WriteLine(approved ? "Y" : "N");

        ChatMessage approvalMessage = new(
            ChatRole.User,
            [approvalRequest.CreateResponse(approved)]);

        Console.WriteLine("\n[System] Resuming agent execution...");

        response = await mentorAgent.RunAsync(approvalMessage, session);
    }
}

// -----------------------------------------------------------------------------
// 7. Final Answer
// -----------------------------------------------------------------------------
Console.WriteLine("\nSenior Mentor:");
Console.WriteLine(response.Text);
