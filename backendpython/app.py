import os
from flask import Flask, request, jsonify
from openai import OpenAI
from dotenv import load_dotenv

# Initialize Flask app
app = Flask(__name__)

# Load environment variables from .env file
load_dotenv()

# Load environment variables for Azure OpenAI
AZURE_OPENAI_API_KEY = os.getenv("AZURE_OPENAI_API_KEY")
AZURE_OPENAI_ENDPOINT = os.getenv("AZURE_OPENAI_ENDPOINT")
AZURE_MODEL_NAME = os.getenv("AZURE_MODEL_NAME")

# Initialize OpenAI client
openai_client = OpenAI(api_key=AZURE_OPENAI_API_KEY, base_url=AZURE_OPENAI_ENDPOINT)

# API endpoint for chat
@app.route('/api/chat', methods=['POST'])
def chat():
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
    

# Run the Flask app
if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5000)