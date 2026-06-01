import { useRef, useState } from "react";
import type { SyntheticEvent } from "react";
import { v4 as uuid } from "uuid";
import { runAguiStreaming } from "../services/aguiClient";
import type { AguiEvent, ChatMessage } from "../types/agui";
import "./AguiChatWindow.css";
import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";

export default function AguiChatWindow() {
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const [input, setInput] = useState("");
  const [isStreaming, setIsStreaming] = useState(false);
  const [threadId, setThreadId] = useState<string | null>(null);

  const abortRef = useRef<AbortController | null>(null);
  const assistantMessageIdRef = useRef<string | null>(null);

  async function handleSubmit(event: SyntheticEvent<HTMLFormElement>) {
    event.preventDefault();

    const text = input.trim();

    if (!text || isStreaming) return;

    const userMessage: ChatMessage = {
      id: uuid(),
      role: "user",
      content: text,
    };

    const assistantMessage: ChatMessage = {
      id: uuid(),
      role: "assistant",
      content: "",
    };

    assistantMessageIdRef.current = assistantMessage.id;

    const nextMessages = [...messages, userMessage, assistantMessage];

    setMessages(nextMessages);
    setInput("");
    setIsStreaming(true);

    const controller = new AbortController();
    abortRef.current = controller;

    try {
      await runAguiStreaming({
        messages: [...messages, userMessage],
        threadId,
        signal: controller.signal,
        onEvent: handleAguiEvent,
      });
    } catch (error) {
      appendToAssistant(
        `\n\nError: ${error instanceof Error ? error.message : "Unknown error"}`,
      );
    } finally {
      setIsStreaming(false);
      abortRef.current = null;
      assistantMessageIdRef.current = null;
    }
  }

  function handleAguiEvent(event: AguiEvent) {
    switch (event.type) {
      case "RUN_STARTED": {
        if (typeof event.threadId === "string") {
          setThreadId(event.threadId);
        }
        break;
      }

      case "TEXT_MESSAGE_CONTENT": {
        const delta =
          typeof event.delta === "string"
            ? event.delta
            : typeof event.text === "string"
              ? event.text
              : "";

        if (delta) {
          appendToAssistant(delta);
        }

        break;
      }

      case "RUN_ERROR": {
        const message =
          typeof event.message === "string"
            ? event.message
            : typeof event.error === "string"
              ? event.error
              : "Agent error.";

        appendToAssistant(`\n\n${message}`);
        break;
      }

      default:
        break;
    }
  }

  function appendToAssistant(delta: string) {
    const assistantId = assistantMessageIdRef.current;
    if (!assistantId) return;

    setMessages((current) =>
      current.map((message) =>
        message.id === assistantId
          ? { ...message, content: message.content + delta }
          : message,
      ),
    );
  }

  function stopStreaming() {
    abortRef.current?.abort();
    setIsStreaming(false);
  }

  return (
    <div className="agui-chat">
      <div className="agui-chat-header">
        <div>
          <h2>Enterprise Support Assistant</h2>
          <span>{threadId ? `Thread: ${threadId}` : "New conversation"}</span>
        </div>

        {isStreaming && (
          <button type="button" onClick={stopStreaming}>
            Stop
          </button>
        )}
      </div>

      <div className="agui-chat-messages">
        {messages.length === 0 && (
          <div className="agui-empty">
            Ask about documents, GitHub repositories, pull requests, or
            meetings.
          </div>
        )}

        {messages.map((message) => (
          <div
            key={message.id}
            className={`agui-message agui-message-${message.role}`}
          >
            <div className="agui-message-role">{message.role}</div>

            {/*
            <div className="agui-message-content">
              {message.content || (message.role === "assistant" ? "..." : "")}
            </div>
            */}

            <div className="agui-message-content">
              {message.role === "assistant" ? (
                <ReactMarkdown remarkPlugins={[remarkGfm]}>
                  {message.content || "..."}
                </ReactMarkdown>
              ) : (
                message.content
              )}
            </div>
          </div>
        ))}
      </div>

      <form className="agui-chat-input" onSubmit={handleSubmit}>
        <input
          value={input}
          disabled={isStreaming}
          onChange={(e) => setInput(e.target.value)}
          placeholder="Type your message..."
        />

        <button type="submit" disabled={isStreaming || !input.trim()}>
          Send
        </button>
      </form>
    </div>
  );
}
