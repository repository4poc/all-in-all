## OpenRouter

It is a browser-based platform that lets developers access many different AI models through one API.

![alt text](images/{9088DD6C-9EBD-497D-8880-B084B574229B}.png)

- Login and Create a Key

  ![alt text](images/{849C3898-F9AD-45C9-8B39-D4BBF35F862A}.png)

For example, you can access models from providers such as:

- OpenAI (GPT)
- Anthropic (Claude)
- Google (Gemini)
- Meta (Llama)
- DeepSeek

Instead of integrating separately with every AI provider, you can use OpenRouter as a common gateway.

![alt text](images/{3BF60D07-2982-4523-B3F8-A3E4DC4101DE}.png)

![alt text](images/{305EAC01-A6D6-48B6-A2CE-E168E57A4327}.png)

![alt text](images/{A3E2887E-FC9E-4140-B330-54D8B5EB44E2}.png)

```
import os
from google.colab import userdata
from openai import OpenAI

OPENROUTER_API_KEY = userdata.get("OPENROUTER_API_KEY").strip()

OPENROUTER_BASE_URL="https://openrouter.ai/api/v1"
MODEL="openrouter/free"

client = OpenAI(base_url=OPENROUTER_BASE_URL,api_key=OPENROUTER_API_KEY,max_retries=3,timeout=30)

messages = {
    "role": "user",
    "content": "Who is Narendra Modi!",
}

response = client.chat.completions.create(
    model=MODEL,
    messages=[messages],
)


print("Answered by Model: ", response.model)

print("response : "+ response.choices[0].message.content.strip())

```

```
Answered by Model:  dots-studio/dots-3-note-preview:free
```

## Instant Gratification

![alt text](images/{AC464B86-9A54-4245-B666-7F6FEA2F6E64}.png)

![alt text](images/{3A8BFDD2-CC64-4570-81C4-10352DE7971F}.png)

![alt text](images/{85FE43AE-DA10-4C8F-AD04-ED88F5480485}.png)

## Agent Harness

![alt text](images/{7FAE006E-63AE-4DEB-B0C9-ABC91E532BF8}.png)

![alt text](images/{94E2AA6F-8A0F-4D52-BF8F-818AC6433158}.png)

![alt text](images/{5B975215-9D47-4260-B674-891D37C65B93}.png)

![alt text](images/{9C131F5E-AA80-4DD3-AC83-050810DF3B17}.png)

![alt text](images/agentharnessing.png)

![alt text](images/{5A35DBDA-7060-4F4B-93C3-E726386FD95F}.png)

## 2. AAn LLM Solves a Maze : Tool Calling and an Agent Loop from Scratch

```
from bs4 import BeautifulSoup
import requests

@mcpserver.tool()
def read_web_page(url:str = MAZE_URL):
    """Read a web page in the maze and return the text on the page, without the HTML"""
    print(f"Reading web page:{url}")

    if not url.startswith(MAZE_URL):
        return f"Sorry, this tool can only read pages in the maze. Please ask for a URL that start with {MAZE_URL}"

    response = requests.get(url,timeout=20)
    page = BeautifulSoup(response.text, "html.parser")

    return page.body.get_text(separator="\n", strip=True)
```

## 3. Roadmap AI Engineer to AI Architect and AI FDE

![alt text](images/{9502F1DB-2D42-4E03-8485-BCC63B7369F8}.png)

## 4. What is AI Forward Deployment Engineer = AI Solution Architect

![alt text](images/{3407B34D-0514-4E02-8541-E09E1B806772}.png)

![alt text](images/{94FC0065-F7B3-40C2-B0B0-1F5F268FCA34}.png)

## 5. The Skills, Tools and Experience you need to become an AI FDE

![alt text](images/{4DE956BA-B15C-4B5F-A3E0-616ADA26CF35}.png)

![alt text](images/{BF505FAA-68D2-4141-A67B-96CC73561240}.png)

![alt text](images/{1BE934AB-AB4E-40BD-9262-EDC92E22A436}.png)

## 6. Environment Setup

- VSCode
- Git
- Node
- uv

uv is a fast Python package and project manager. It is an alternative to tools such as pip, venv,

![alt text](images/{8C685181-B58D-47C7-871E-BC8EA750DCA1}.png)

1. Install uv

```
winget install --id astral-sh.uv -e
```

2. Create a project

```
uv init multi-agent-app1
cd multi-agent-app1
```

3. Create a virtual environment

```
uv venv
```

4. Install your dependencies

```
uv add fastmcp openai python-dotenv requests beautifulsoup4
```

5. Run your application

```
uv run python runner.py
```

Why use uv?

- Much faster package installation than traditional pip in many situations.
- Manages Python versions.
- Creates and manages virtual environments.
- Tracks dependencies in pyproject.toml.
- Uses uv.lock to help reproduce the same dependency versions.

![alt text](images/{CC149CA3-A050-4A1F-B2D3-0D559C8D3A30}.png)

## 7. Clone the repo

![alt text](images/{40AEDB70-D8EF-4383-9DB5-E9D12CC3A77F}.png)

![alt text](images/{3C4B3695-5A74-4E4F-8762-D7F2149763CE}.png)

### Install Extension

- Python (From Microsoft)

  ![alt text]({88CF136A-33AA-48A3-A1F5-068E4F0206EF}.png)

- Jupyter (From Microsoft)

  ![alt text](images/{B046E8B1-FDB6-4434-B522-C3CF38DC969C}.png)

## 8. Tool to render the URL content
