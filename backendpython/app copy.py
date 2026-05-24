import os
from flask import Flask, request, jsonify
from openai import OpenAI
from dotenv import load_dotenv
from azure.identity import DefaultAzureCredential
from azure.ai.projects import AIProjectClient
from anthropic import AnthropicFoundry
from azure.ai.projects.models import (
    WebSearchPreviewTool,
    PromptAgentDefinition,
    MCPTool,
    AzureAISearchTool,
    AzureAISearchToolResource,
    AISearchIndexResource,
    AzureAISearchQueryType,
)


import requests
import jsonref
from typing import Any, cast

from azure.ai.textanalytics import TextAnalyticsClient
from azure.ai.documentintelligence import DocumentIntelligenceClient
from azure.ai.documentintelligence.models import AnalyzeResult
from azure.ai.documentintelligence.models import AnalyzeDocumentRequest
from azure.ai.projects.models import PromptAgentDefinition
from azure.core.credentials import AzureKeyCredential


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

azure_language_endpoint = os.getenv("AZURE_LANGUAGE_ENDPOINT")
azure_language_api_key = os.getenv("AZURE_LANGUAGE_API_KEY")

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


ta_credential = AzureKeyCredential(azure_language_api_key)

text_analytics_client = TextAnalyticsClient(
    endpoint=azure_language_endpoint,
    credential=ta_credential
)

connection_id = ""

mcp_server_name = os.getenv("MCP_SERVER_NAME")
ai_search_connection_name = os.getenv("AI_SEARCH_CONNECTION_NAME")
ai_search_index_name = os.getenv("AI_SEARCH_INDEX_NAME")

connection_id = None
if mcp_server_name:
    for connection in project_client.connections.list():
        if connection.name == mcp_server_name:
            connection_id = connection.id
            break

print(f"MCP connection name={mcp_server_name}, id={connection_id}")

connection_id = ""

for connection in project_client.connections.list():
    if connection.name == ai_search_connection_name:
        connection_id = connection.id
        break

print(f"The AI Search Connection ID is: {connection_id}")

def PII_Text_Redaction(input_text: str) -> str:
    documents = [
        input_text
    ]

    response = text_analytics_client.recognize_pii_entities(documents, language="en")
    result = [doc for doc in response if not doc.is_error]
    for doc in result:
        redacted_text = doc.redacted_text
        for entity in doc.entities:
            print("Entity: {}".format(entity.text))
            print("	Category: {}".format(entity.category))
            print("	Confidence Score: {}".format(entity.confidence_score))
            print("	Offset: {}".format(entity.offset))
            print("	Length: {}".format(entity.length))
        return redacted_text
    
    
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

    custom_mcp_tool = MCPTool(
        server_label="varinder-mcp-server",
        server_url="http://localhost:8000/mcp",
        server_description="Courses and Recipies MCP server",
        require_approval="never"
    )

    azure_ai_search_tool = AzureAISearchTool(
        azure_ai_search=AzureAISearchToolResource(
            indexes=[
                AISearchIndexResource(
                    project_connection_id=connection_id,
                    index_name=ai_search_index_name,
                    query_type=AzureAISearchQueryType.VECTOR_SEMANTIC_HYBRID,
                    top_k=3,
                )
            ]
        )
    )

    agent =  project_client.agents.create_version(
        agent_name = "web-search-agent",
        definition = PromptAgentDefinition(
            model = MODEL_DEPLOYMENT_NAME,
            instructions = "You are a Also a travel assistant. Help users plan their trips, find flights, hotels, and provide travel advice.",
            tools = [custom_mcp_tool]
            # tools = [petstore_tool,MCPServerTool, azure_ai_search_tool]
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

        redacted_input = PII_Text_Redaction(user_message.strip())

        print(f"Redacted Input: {redacted_input}")

        response = openai_client.responses.create(
            conversation=conversation.id,
            input=redacted_input,
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