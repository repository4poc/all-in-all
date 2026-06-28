import type { AguiEvent, ChatMessage } from "../types/agui";

const AGUI_ENDPOINT = import.meta.env.VITE_AGUI_ENDPOINT ?? "/agui/support";

type StreamOptions = {
  messages: ChatMessage[];
  threadId?: string | null;
  signal?: AbortSignal;
  onEvent: (event: AguiEvent) => void;
};

export async function runAguiStreaming({
  messages,
  threadId,
  signal,
  onEvent,
}: StreamOptions): Promise<void> {
  const payload = {
    threadId: threadId ?? undefined,
    messages: messages.map((m) => ({
      role: m.role,
      content: m.content,
    })),
  };

  const response = await fetch(AGUI_ENDPOINT, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Accept: "text/event-stream",
    },
    body: JSON.stringify(payload),
    signal,
  });

  if (!response.ok) {
    throw new Error(`AG-UI request failed: ${response.status}`);
  }

  if (!response.body) {
    throw new Error("AG-UI response body is empty.");
  }

  const reader = response.body.getReader();
  const decoder = new TextDecoder();

  let buffer = "";

  while (true) {
    const { value, done } = await reader.read();

    if (done) break;

    buffer += decoder.decode(value, { stream: true });

    const events = buffer.split("\n\n");
    buffer = events.pop() ?? "";

    for (const rawEvent of events) {
      const dataLine = rawEvent
        .split("\n")
        .find((line) => line.startsWith("data:"));

      if (!dataLine) continue;

      const json = dataLine.replace(/^data:\s*/, "").trim();

      if (!json || json === "[DONE]") continue;

      try {
        onEvent(JSON.parse(json));
      } catch {
        console.warn("Could not parse AG-UI event:", json);
      }
    }
  }
}
