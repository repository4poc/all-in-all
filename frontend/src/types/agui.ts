export type ChatRole = "user" | "assistant" | "system";

export type ChatMessage = {
  id: string;
  role: ChatRole;
  content: string;
};

export type AguiEvent =
  | {
      type: "RUN_STARTED";
      threadId?: string;
      runId?: string;
    }
  | {
      type: "TEXT_MESSAGE_START";
      messageId?: string;
      role?: "assistant";
    }
  | {
      type: "TEXT_MESSAGE_CONTENT";
      messageId?: string;
      delta?: string;
      text?: string;
    }
  | {
      type: "TEXT_MESSAGE_END";
      messageId?: string;
    }
  | {
      type: "RUN_FINISHED";
      threadId?: string;
      runId?: string;
    }
  | {
      type: "RUN_ERROR";
      message?: string;
      error?: string;
    }
  | Record<string, unknown>;
