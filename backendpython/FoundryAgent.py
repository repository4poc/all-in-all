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


from a2a.server.agent_execution import AgentExecutor, RequestContext
from a2a.server.events import EventQueue
from a2a.utils import new_agent_text_message
from a2a.types import (
    TaskArtifactUpdateEvent,
    TaskState,
    TaskStatus,
    TaskStatusUpdateEvent,
)
from a2a.utils import new_text_artifact

# Initialize Flask app
#app = Flask(__name__)

# Load environment variables from .env file
load_dotenv()

# Load environment variables for Azure OpenAI
FOUNDRY_PROJECT_ENDPOINT = os.getenv("FOUNDRY_PROJECT_ENDPOINT")

# Initialize Azure AI Projects client
project_client = AIProjectClient(credential=DefaultAzureCredential(), endpoint=FOUNDRY_PROJECT_ENDPOINT)

agent_name ="batman-agent"

class FoundryAgent():
    """This class will contain helper functions for interacting with our Foundry Agent"""

    async def invoke_agent(self, user_query: str) -> str:
        openai_client = project_client.get_openai_client()

        conversation = await openai_client.conversations.create()

        response = await openai_client.responses.create(
            conversation=conversation.id,
            extra_body={
                "agent": {
                    "name": agent_name,
                    "type": "agent_reference"
                }
            },
            input=user_query
        )
        
        return response.output_text



class FoundryAgentExecutor(AgentExecutor):
    """Foundry Agent Executor Definition."""

    def __init__(self):
        self.agent = FoundryAgent()

    async def execute(
        self,
        context: RequestContext,
        event_queue: EventQueue,
    ) -> None:
        query = context.get_user_input()
        if not context.message:
            raise Exception('No message provided')

        # If your agent does not support streaming, just call invoke_agent
        result = await self.agent.invoke_agent(query)
        message = TaskArtifactUpdateEvent(
            context_id=context.context_id,  # type: ignore
            task_id=context.task_id,  # type: ignore
            artifact=new_text_artifact(
                name='current_result',
                text=result,
            ),
        )
        await event_queue.enqueue_event(message)

        status = TaskStatusUpdateEvent(
            context_id=context.context_id,  # type: ignore
            task_id=context.task_id,  # type: ignore
            status=TaskStatus(state=TaskState.completed),
            final=True,
        )
        await event_queue.enqueue_event(status)

    async def cancel(
        self, context: RequestContext, event_queue: EventQueue
    ) -> None:
        raise Exception('cancel not supported')
    

from a2a.types import (
    AgentCapabilities,
    AgentCard,
    AgentSkill,
)

skill = AgentSkill(
    id = "foundry_agent_skill",
    name = "Responses API from Foundry Agent",
    description = "Responses API from Foundry Agent",
    tags = ["foundry agent"],
    examples = ["hi, how are you?", "can you tell me something about GenAI and LLMs"]
)

public_agent_card = AgentCard(
    name = "Foundry Demo Agent",
    description = "Foundry Demo Agent to Show A2A Usage with Microsoft Foundry",
    url = "http://localhost:8080",
    version = "1.0.0",
    default_input_modes=['text'],
    default_output_modes=['text'],
    capabilities=AgentCapabilities(streaming=False),
    skills = [skill]
)

from a2a.server.request_handlers import DefaultRequestHandler
from a2a.server.tasks import InMemoryTaskStore

request_handler = DefaultRequestHandler(
    agent_executor = FoundryAgentExecutor(),
    task_store = InMemoryTaskStore()
)


from a2a.server.apps import A2AStarletteApplication

server = A2AStarletteApplication(
    agent_card = public_agent_card,
    http_handler = request_handler
)

import asyncio
import uvicorn

config = uvicorn.Config(
    server.build(),
    host="0.0.0.0",
    port=8080,
    loop="asyncio",
)

server_instance = uvicorn.Server(config)

await server_instance.serve()