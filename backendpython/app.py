import os
from flask import Flask, request, jsonify
from dotenv import load_dotenv
from azure.identity import DefaultAzureCredential
from azure.ai.projects import AIProjectClient
from azure.ai.projects.models import (
    WebSearchPreviewTool,
    PromptAgentDefinition,
    MCPTool,
    AzureAISearchTool,
    AzureAISearchToolResource,
    AISearchIndexResource,
    AzureAISearchQueryType,
)


app = Flask(__name__)
load_dotenv()

@app.route('/api/agent/ask', methods=['POST'])
def foundry_openAI_Agent():
    print("----------------------")

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
    project_client = AIProjectClient(credential=DefaultAzureCredential(), endpoint=FOUNDRY_PROJECT_ENDPOINT)


    agent =  project_client.agents.create_version(
        agent_name = "rag-agent",
        definition = PromptAgentDefinition(
            model = "gpt-5-deployment",
            instructions = "You are a Also a travel assistant. Help users plan their trips, find flights, hotels, and provide travel advice.",
            tools = [azure_ai_search_tool]
        )
    )

    print(f"Created agent with ID: {agent.id} and name: {agent.name} and tools: {[tool['type'] for tool in agent.definition.tools]}")
 

    data = request.get_json(silent=True) or {}
    question = data.get("question", "")

    return jsonify({
        "status": "success",
        "question": question,
        "answer": "Agent response will go here"
    }), 200

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=9000)