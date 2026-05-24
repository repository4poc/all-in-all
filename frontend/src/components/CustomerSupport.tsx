import { useEffect, useMemo, useRef, useState } from "react";
import { useMsal } from "@azure/msal-react";

import { HttpAgent, AbstractAgent } from "@ag-ui/client";

import {
  ChatProvider,
  ChatHeader,
  MessageList,
  MessageInput,
} from "react-ag-ui";

import "react-ag-ui/dist/styles.css";
import "./CustomerSupport.css";

const AZURE_SCOPE = ["https://management.azure.com/user_impersonation"];

function getThreadId(): string {
  let threadId = localStorage.getItem("agui-thread-id");

  if (!threadId) {
    threadId = crypto.randomUUID();

    localStorage.setItem("agui-thread-id", threadId);
  }

  return threadId;
}

function CustomerSupport() {
  const { instance, accounts } = useMsal();

  const messagesRef = useRef<HTMLDivElement>(null);

  const inputRef = useRef<HTMLDivElement>(null);

  const [loading, setLoading] = useState(false);

  const [accessToken, setAccessToken] = useState<string>("");

  //
  // Load Azure token once
  //
  useEffect(() => {
    async function loadToken() {
      try {
        const account = accounts[0];

        if (!account) return;

        const tokenResponse = await instance.acquireTokenSilent({
          scopes: AZURE_SCOPE,
          account,
        });

        setAccessToken(tokenResponse.accessToken);
      } catch (error) {
        console.error("Could not acquire Azure token", error);
      }
    }

    loadToken();
  }, [accounts, instance]);

  //
  // AG-UI agent
  //
  const agent: AbstractAgent = useMemo(() => {
    return new HttpAgent({
      url: "http://localhost:5000/agui/support",

      description: "Customer Support Agent",

      threadId: getThreadId(),

      //
      // Must be plain object
      //
      headers: accessToken
        ? {
            Authorization: `Bearer ${accessToken}`,
          }
        : {},
    });
  }, [accessToken]);

  //
  // start typing animation
  //
  useEffect(() => {
    const inputContainer = inputRef.current;

    if (!inputContainer) return;

    const startLoading = () => {
      setLoading(true);
    };

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.key === "Enter" && !event.shiftKey) {
        startLoading();
      }
    };

    const button = inputContainer.querySelector("button");

    const textarea = inputContainer.querySelector("textarea");

    const input = inputContainer.querySelector("input");

    button?.addEventListener("click", startLoading);

    textarea?.addEventListener("keydown", handleKeyDown);

    input?.addEventListener("keydown", handleKeyDown);

    return () => {
      button?.removeEventListener("click", startLoading);

      textarea?.removeEventListener("keydown", handleKeyDown);

      input?.removeEventListener("keydown", handleKeyDown);
    };
  }, []);

  //
  // auto-scroll + stop typing
  //
  useEffect(() => {
    const container = messagesRef.current;

    if (!container) return;

    let stopTimer: ReturnType<typeof setTimeout> | undefined;

    const observer = new MutationObserver(() => {
      container.scrollTop = container.scrollHeight;

      if (loading) {
        if (stopTimer) {
          clearTimeout(stopTimer);
        }

        stopTimer = setTimeout(() => {
          setLoading(false);
        }, 1500);
      }
    });

    observer.observe(container, {
      childList: true,
      subtree: true,
      characterData: true,
    });

    return () => {
      observer.disconnect();

      if (stopTimer) {
        clearTimeout(stopTimer);
      }
    };
  }, [loading]);

  //
  // reset chat
  //
  const resetConversation = () => {
    const newId = crypto.randomUUID();

    localStorage.setItem("agui-thread-id", newId);

    window.location.reload();
  };

  return (
    <div id="customer-support">
      <ChatProvider agent={agent}>
        <div className="chat-window">
          <div className="chat-toolbar">
            <ChatHeader />

            <button className="new-chat-button" onClick={resetConversation}>
              New Chat
            </button>
          </div>

          <div ref={messagesRef} className="messages-container">
            <MessageList />

            {loading && (
              <div className="typing-loader">
                <div className="typing-dot"></div>
                <div className="typing-dot"></div>
                <div className="typing-dot"></div>
              </div>
            )}
          </div>

          <div
            ref={inputRef}
            className={`input-container ${loading ? "is-loading" : ""}`}
          >
            <MessageInput />
          </div>
        </div>
      </ChatProvider>
    </div>
  );
}

export default CustomerSupport;
