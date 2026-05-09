import os
from flask import Flask, request, jsonify
from openai import OpenAI
from dotenv import load_dotenv
from azure.identity import DefaultAzureCredential
from azure.ai.projects import AIProjectClient
from anthropic import AnthropicFoundry

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