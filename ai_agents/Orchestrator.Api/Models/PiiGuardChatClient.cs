using Microsoft.Extensions.AI;

public sealed class PiiGuardChatClient : DelegatingChatClient
{
    private readonly PiiRedactionService _pii;

    public PiiGuardChatClient(IChatClient innerClient, PiiRedactionService pii)
        : base(innerClient)
    {
        _pii = pii;
        Console.WriteLine("PII Guard Chat Client CREATED");
    }

    public override async Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> messages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine("PII Guard GetResponseAsync HIT");

        var guardedMessages = await RedactMessagesAsync(messages, cancellationToken);

        return await base.GetResponseAsync(
            guardedMessages,
            options,
            cancellationToken);
    }

    public override async IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> messages,
        ChatOptions? options = null,
        [System.Runtime.CompilerServices.EnumeratorCancellation]
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine("PII Guard GetStreamingResponseAsync HIT");

        var guardedMessages = await RedactMessagesAsync(messages, cancellationToken);

        await foreach (var update in base.GetStreamingResponseAsync(
            guardedMessages,
            options,
            cancellationToken))
        {
            yield return update;
        }
    }

    private async Task<List<ChatMessage>> RedactMessagesAsync(
        IEnumerable<ChatMessage> messages,
        CancellationToken cancellationToken)
    {
        var messageList = messages.ToList();

        Console.WriteLine($"Guarding messages. Total messages: {messageList.Count}");

        var guardedMessages = new List<ChatMessage>();

        foreach (var message in messageList)
        {
            if (message.Role == ChatRole.User)
            {
                Console.WriteLine($"User message before PII mask: {message.Text}");

                var redacted = await _pii.RedactAsync(message.Text);

                Console.WriteLine($"User message after PII mask: {redacted}");

                guardedMessages.Add(new ChatMessage(message.Role, redacted));
            }
            else
            {
                guardedMessages.Add(message);
            }
        }

        return guardedMessages;
    }
}