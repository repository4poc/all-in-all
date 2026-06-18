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



# Initialize Azure AI Projects client
ai_projects_client = AIProjectClient(credential=DefaultAzureCredential(), endpoint=FOUNDRY_PROJECT_ENDPOINT)
openai_client_foundry_sdk = ai_projects_client.get_openai_client()

# API endpoint for chat
@app.route('/api/openAI/chat', methods=['POST'])
def foundry_responses_chat():
    data = request.get_json(silent=True) or {}
    print("------")
    print(data)
    app.logger.info("Received chat request with data: %s", data)

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
    

# Run the Flask app
if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5000)