import os
from flask import Flask, request, jsonify
from openai import OpenAI
from dotenv import load_dotenv
from azure.identity import DefaultAzureCredential
from azure.ai.projects import AIProjectClient
from anthropic import AnthropicFoundry
from azure.ai.projects.models import WebSearchPreviewTool, PromptAgentDefinition, MCPTool, Tool, BrowserAutomationPreviewTool, BrowserAutomationToolParameters, BrowserAutomationToolConnectionParameters
import requests
import jsonref
from typing import Any, cast


# Initialize Flask app
app = Flask(__name__)

# Load environment variables from .env file
load_dotenv()

# Load environment variables for Azure OpenAI
AZURE_OPENAI_API_KEY = os.getenv("AZURE_OPENAI_API_KEY")
AZURE_OPENAI_ENDPOINT = os.getenv("AZURE_OPENAI_ENDPOINT")
AZURE_MODEL_NAME = os.getenv("AZURE_MODEL_NAME")
FOUNDRY_PROJECT_ENDPOINT = os.getenv("FOUNDRY_PROJECT_ENDPOINT")
MODEL_DEPLOYMENT_NAME = os.getenv("MODEL_DEPLOYMENT_NAME")

# Initialize OpenAI client
openai_client = OpenAI(api_key=AZURE_OPENAI_API_KEY, base_url=AZURE_OPENAI_ENDPOINT)

# API endpoint for chat
@app.route('/api/chat', methods=['POST'])
def chat_completions():
    data = request.json
    user_message = data.get('message')
    if not user_message:
        return jsonify({'error': 'Message is required'}), 400
    try:
        response = openai_client.chat.completions.create(
            model=AZURE_MODEL_NAME,
            messages=[{"role": "user", "content": user_message}]
        )
        assistant_reply = response.choices[0].message.content
        return jsonify({'reply': assistant_reply})
    except Exception as e:
        return jsonify({'error': str(e)}), 500
    

# Initialize Azure AI Projects client
ai_projects_client = AIProjectClient(credential=DefaultAzureCredential(), endpoint=FOUNDRY_PROJECT_ENDPOINT)
openai_client_foundry_sdk = ai_projects_client.get_openai_client()

# API endpoint for chat
@app.route('/api/openAI/chat', methods=['POST'])
def foundry_responses_chat():
    data = request.get_json(silent=True) or {}

    user_message = data.get('message')
    if not user_message or not user_message.strip():
        return jsonify({'error': 'Message is required'}), 400

    try:
        response = openai_client_foundry_sdk.chat.completions.create(
            model=MODEL_DEPLOYMENT_NAME,
            messages=[
                {"role": "system", "content": "You are a helpful assistant."},
                {"role": "user", "content": user_message.strip()}
            ]
        )

        assistant_reply = response.choices[0].message.content

        return jsonify({'reply': assistant_reply}), 200

    except Exception:
        app.logger.exception("Foundry OpenAI request failed")
        return jsonify({'error': 'Failed to process chat request'}), 500


# Initialize Azure AI Projects client
project_client = AIProjectClient(credential=DefaultAzureCredential(), endpoint=FOUNDRY_PROJECT_ENDPOINT)
openai_client = project_client.get_openai_client()

connection_id = ""

mcp_server_name = os.getenv("MCP_SERVER_NAME")

connection_id = None
if mcp_server_name:
    for connection in project_client.connections.list():
        if connection.name == mcp_server_name:
            connection_id = connection.id
            break

print(f"MCP connection name={mcp_server_name}, id={connection_id}")


# OpanAI Agent API endpoint
@app.route('/api/openAI/agent', methods=['POST'])
def foundry_openAI_Agent():

    # Load OpenAPI spec URL as JSON object
    spec_url = "https://petstore3.swagger.io/api/v3/openapi.json"
    spec_response = requests.get(spec_url, timeout=20)
    spec_response.raise_for_status()

    petstore_spec = cast(dict[str, Any], jsonref.loads(spec_response.text))

    # Create OpenAPI tool
    petstore_tool = {
        "type": "openapi",
        "openapi": {
            "name": "petstore-api",
            "spec": petstore_spec,
            "auth": {
                "type": "anonymous"
            }
        }
    }

    MCPServerTool = MCPTool(
        server_label="microsoft_learn_server",
        server_url="https://learn.microsoft.com/api/mcp",
        server_description="Microsoft Learn documentation MCP server",
        require_approval="never"
    )

    MCPServerTool = MCPTool(
        server_label="microsoft_learn_server",
        server_url="https://learn.microsoft.com/api/mcp",
        require_approval="never",
        # Do NOT pass project_connection_id for Microsoft Learn MCP
    )

    BROWSER_CONNECTION_ID = os.getenv("BROWSER_CONNECTION_ID")

    if not BROWSER_CONNECTION_ID:
        raise ValueError("BROWSER_CONNECTION_ID is missing")

    browser_tool = BrowserAutomationPreviewTool(
        browser_automation_preview=BrowserAutomationToolParameters(
            connection=BrowserAutomationToolConnectionParameters(
                project_connection_id=BROWSER_CONNECTION_ID
            )
        )
    )

    agent =  project_client.agents.create_version(
        agent_name = "web-search-agent",
        definition = PromptAgentDefinition(
            model = MODEL_DEPLOYMENT_NAME,
            instructions = "An agent that can perform petstore3 order information by orderID, mslearn mcp server and web search capabilities.You can use web search, Petstore OpenAPI, and Microsoft Learn MCP. Use Microsoft Learn MCP only for Microsoft documentation questions.Use the browser tool when the user asks to open websites, click buttons, fill forms, inspect pages, or perform UI workflows.Do not enter passwords, payment details, or sensitive personal data.",
            tools = [
                WebSearchPreviewTool(),petstore_tool, MCPServerTool,browser_tool]
        )
    )

    print(f"Created agent with ID: {agent.id} and name: {agent.name} and tools: {[tool['type'] for tool in agent.definition.tools]}")
    

    data = request.get_json(silent=True) or {}

    user_message = data.get('message')
    if not user_message or not user_message.strip():
        return jsonify({'error': 'Message is required'}), 400

    try:
        # Create a conversation with the agent
        conversation = openai_client.conversations.create()
        print(f"Created conversation with ID: {conversation.id}")

        response = openai_client.responses.create(
            conversation=conversation.id,
            input=user_message.strip(),
            extra_body={
                "agent_reference": {
                    "type": "agent_reference",
                    "name": agent.name
                }
            }
        )

        assistant_reply = response.output_text

        print(f"Agent response: {assistant_reply}")

        return jsonify({'reply': assistant_reply}), 200

    except Exception:
        app.logger.exception("Foundry OpenAI request failed")
        return jsonify({'error': 'Failed to process chat request'}), 500
    



# Anthropic Foundry API integration

ANTHROPIC_API_KEY = os.getenv("ANTHROPIC_FOUNDRY_API_KEY")
ANTHROPIC_ENDPOINT = os.getenv("ANTHROPIC_FOUNDRY_ENDPOINT")
CLAUDE_DEPLOYMENT_NAME = os.getenv("CLAUDE_DEPLOYMENT_NAME")

anthropic_client = AnthropicFoundry(api_key=ANTHROPIC_API_KEY, base_url=ANTHROPIC_ENDPOINT)

@app.route('/api/anthropic/chat', methods=['POST'])
def foundry_anthropic_chat():
    data = request.get_json(silent=True) or {}

    user_message = data.get('message')
    if not user_message or not user_message.strip():
        return jsonify({'error': 'Message is required'}), 400

    try:
        message = anthropic_client.messages.create(
            model=CLAUDE_DEPLOYMENT_NAME,
            system="You are a helpful assistant.",
            max_tokens=1024,
            messages=[
                {
                    "role": "user",
                    "content": user_message.strip()
                }
            ]
        )

        assistant_reply = "".join(
            block.text
            for block in message.content
            if getattr(block, "type", None) == "text"
        )

        return jsonify({'reply': assistant_reply}), 200

    except Exception:
        app.logger.exception("Foundry Anthropic Claude request failed")
        return jsonify({'error': 'Failed to process Claude chat request'}), 500

# Run the Flask app
if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5000)