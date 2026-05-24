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
#app = Flask(__name__)

# Load environment variables from .env file
load_dotenv()

# Load environment variables for Azure OpenAI
FOUNDRY_PROJECT_ENDPOINT = os.getenv("FOUNDRY_PROJECT_ENDPOINT")
MODEL_DEPLOYMENT_NAME = os.getenv("MODEL_DEPLOYMENT_NAME")

# Initialize Azure AI Projects client
project_client = AIProjectClient(credential=DefaultAzureCredential(), endpoint=FOUNDRY_PROJECT_ENDPOINT)
openai_client = project_client.get_openai_client()

agent_name ="batman-agent"

agent =  project_client.agents.create_version(
    agent_name = agent_name,
    definition = PromptAgentDefinition(
        model = MODEL_DEPLOYMENT_NAME,
        instructions = "You are Batman, the dark knight of Gotham city. Answer as concisely as possible. Always answer in the style of Batman.",
    )
)

print(f"Created agent with ID: {agent.id} and name: {agent.name} and version: {agent.version}")
    


# Run the Flask app
#if __name__ == '__main__':
#    app.run(host="0.0.0.0", port=5000)