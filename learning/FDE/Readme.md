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

![alt text](images/{1A241D50-B5B0-476F-8556-AFE27AE406BB}.png)

## 2. An LLM Solves a Maze : Tool Calling and an Agent Loop from Scratch

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
- Manages Python versions. (CrewAI need python < 3.12)
- Creates and manages virtual environments.
- Tracks dependencies in pyproject.toml.
- Uses uv.lock to help reproduce the same dependency versions.

![alt text](images/{CC149CA3-A050-4A1F-B2D3-0D559C8D3A30}.png)

## 7. Clone the repo

![alt text](images/{40AEDB70-D8EF-4383-9DB5-E9D12CC3A77F}.png)

![alt text](images/{3C4B3695-5A74-4E4F-8762-D7F2149763CE}.png)

### Install Extension

- Python (From Microsoft)

  ![alt text](images/{88CF136A-33AA-48A3-A1F5-068E4F0206EF}.png)

- Jupyter (From Microsoft)

  ![alt text](images/{B046E8B1-FDB6-4434-B522-C3CF38DC969C}.png)

## 8. Tool to render the URL content

```
MAZE_URL = "https://maze.edwarddonner.com"

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

## 10. From GTP to ChatGPT

![alt text](images/{71C0B106-01DD-4DEB-85FD-443E57DFE9A7}.png)

![alt text](images/{4106007B-06F1-46EA-829D-DAC504C41654}.png)

1000 Token = 750 words in english

![alt text](images/{2BCE68FF-B4F5-41B7-B687-ECA42E46EAF8}.png)

Avoid `Pre-mature Optimization`, First priority is to achieve the goal/`Commercial Objective`, then go for optimizations.

`Commercial Objective`

- What you are trying to solve
- What is the accuracy of what you are getting.

So first solve for `accuracy` then looking out ways for `efficiency`

With Right Prompts and Right Answers, you can later use a cheaper model for cost efficiency. But later in the process. `Primary focus on accuracy`

## 11. Comparing LLMs

https://artificialanalysis.ai/

Do LLM benchmarking

![alt text](images/{5504908C-3085-4F86-842A-9F72CDD26BCA}.png)

![alt text](images/{B1F59C6B-779D-454E-8210-25BE8CEA8471}.png)

![alt text](images/{DDB86300-1F19-4356-A5F4-856893C4249F}.png)

![alt text](images/{9F36D934-8EAB-4814-B9C9-9A453EAD55ED}.png)

![alt text](images/{C1A976BF-78E4-41F6-A145-BDC9A6DD3623}.png)

![alt text](images/{C1147EB5-5C74-4E0B-A005-164593FB3B78}.png)

METR - Portfolio Task `Include Harness`

https://metr.org/blog/2025-03-19-measuring-ai-ability-to-complete-long-tasks/

![alt text](images/{2BF429E1-8163-448D-868A-137CFC9A78DC}.png)

## 12. What is AI Agent

![alt text](images/{BDDDDF6A-1626-473E-B8D3-18E7E3FC96D0}.png)

LLM : It generate tokens, it predicts the next token

![alt text](images/{ADB54662-5A65-411C-9735-612CB4700D64}.png)

![alt text](images/{E6039E28-8BE3-43E8-B89B-283BA8E95274}.png)

![alt text](images/{B11D8299-9BC7-48B8-8891-1E35C91BD438}.png)

![alt text](images/{F8AE058E-3CC4-4A7C-A22F-478E737BAF1F}.png)

![alt text](images/{7CEF3A48-27E7-4703-BAA6-A625FEB22579}.png)

![alt text](images/{21A3ED71-BBDD-47C8-8ED1-F5429FF16305}.png)

## 12. Undercover Project Overview

![alt text](images/{90455BED-4B40-4303-8BBE-7A642380C57B}.png)

![alt text](images/{42FC84A5-8DB0-41F5-B134-BABC731E7969}.png)

![alt text](images/{80B481AB-D322-478C-8503-2F26C0ADC4B4}.png)

## 14. Lab Preparation

![alt text](images/{D1FF0D27-F9C1-408D-989C-735B6CC39BA3}.png)

![alt text](images/{E4A75A99-C218-48CF-9417-6B7B69F89928}.png)

![alt text](images/{C07BBA1F-3B0D-480F-A9FE-F67AAEA1EBA1}.png)

![alt text](images/{A14372A8-5BEA-4A9C-8C28-4DE717C200D6}.png)

![alt text](images/{EA7450D6-9497-4DD2-A64B-3D09A3EDCC4E}.png)

## 15. Undercover

![alt text](images/{116D9282-5AF4-4F94-A17D-4044B4459AB4}.png)

![alt text](images/{935E6700-34C5-495C-90C9-297B2E21D7A6}.png)

![alt text](images/{62BF5709-B875-4484-B651-CCAB7A287761}.png)

![alt text](images/{35F7540B-997E-4320-807E-C069D04E0491}.png)

![alt text](images/{FCE214F4-2F76-4222-8434-AE9F9EE56DCE}.png)

![alt text](images/{3E413DA9-B2A0-4D07-82F0-80650133D1BC}.png)

![alt text](images/{CF9B05AA-9D43-4EED-87EB-F449B7DEE0ED}.png)

![alt text](images/{1CDDBB1A-743E-47F8-B6D2-659EA539CE4C}.png)

![alt text](images/{69CE6E1F-9A8C-49E1-870E-1A18A3F9E2FC}.png)

One of a `claim.json` Called a `Data Point`

![alt text](images/{887C2BE6-2777-4712-9A29-CFBD1F6F94F5}.png)

### Exploring Seed Data

![alt text](images/{D4AA7038-05FC-4212-BE78-DEC6E74FBEF1}.png)

![alt text](images/{354B1320-0BB8-4B12-9167-4536F1411143}.png)

![alt text](images/{CC149632-3444-45A1-9022-FCC63A0790EC}.png)

![alt text](images/{FACF8207-3456-457A-B567-3BE994B5819C}.png)

![alt text](images/{0D63E8E6-0555-4A03-85DC-F9BF97ED90A7}.png)

![alt text](images/{84B2F520-4D9A-4900-B86B-42FCDFD8489C}.png)

![alt text](images/{87584109-864D-4051-B93C-AA4331866541}.png)

![alt text](images/{A7344B6B-A68B-4BFA-A032-9B8A1E24B59B}.png)

## 16. Agent Implementation

![alt text](images/{5C5683E2-D3AE-4FB5-9230-A18103378A1E}.png)

![alt text](images/{AE5ED5DD-3957-437E-BBD5-C0A777D4665C}.png)

![alt text](images/{CF310989-46B7-45BD-9E07-6740EF0CA843}.png)

![alt text](images/{57120FF8-1FF3-4561-B467-446A5B5DFE4D}.png)

### Execute Agent

![alt text](images/{E00E543C-2599-4134-A57C-97B39D2844EF}.png)

### Evaluation : Running multiple models

![alt text](images/{2031B929-0A77-447B-A01C-2B7B6CC6C47D}.png)

### Testing and Evaluation

![alt text](images/{4D78A51F-DDB4-4765-ABA7-590DAC20C60B}.png)

![alt text](images/{C58FC426-81AB-412B-A1AE-41EEE8C08C93}.png)

![alt text](images/{7CF0E75B-0F56-43F8-8530-094D1756AD57}.png)

![alt text](images/{94583CE8-58CD-4944-880A-04F7353E36F1}.png)

Openai/gpt models use prompt caching by default, so if yuo call it multiple time, will cost lesser

![alt text](images/{E2A71F4B-6F7C-49E5-BBF9-9FCC7E44AD34}.png)

![alt text](images/{720B4029-6FFA-4AFF-B99B-E1139A472A94}.png)

![alt text](images/{999F34B9-FB2A-4B07-AAAF-24D344FEDB23}.png)

![alt text](images/{BF93C2CE-B3F4-4336-AF71-F11F09887F24}.png)

![alt text](images/{1D2AF6FB-5CA0-4107-80AD-DB3FF05A1F34}.png)

![alt text](images/{DB67E94C-1511-4EF4-94D9-741A98CAB018}.png)

![alt text](images/{56EACF67-6E2B-4506-8F89-E046A4263FEF}.png)

you have to tradeoff between accuracy, speed and cost

There is `Confusion Metrix` with `False Positive` and `False Negative`

It is good to accept claim which you should be rejected rather you reject a claim which you should have approved, as it impact customer and business.

So using the There is `Confusion Metrix` with `False Positive` and `False Negative` they want to check different tolerances against each one.

Like in case of Covid Test,

More Tolerance in case of false positive, than false negative

## Build Claim Agent with LangChain

![alt text](images/{F1CA4847-E986-417C-9AC0-CB7C8AD5EE67}.png)

![alt text](images/{11700E07-714A-4784-AC0D-D0B6B7938BA1}.png)

![alt text](images/{E65EB37F-8EC7-4C30-BF8E-F53AF2B78799}.png)

![alt text](images/{4E5E2D61-1EF5-4C5A-974F-6AD93F91142C}.png)

![alt text](images/{A2BB90CC-2475-4E08-B360-C0AA643AD337}.png)

![alt text](images/{2F891468-07DD-4408-9CB6-0D47ED17BA83}.png)

![alt text](images/{F30AF677-4FE3-4FD3-B4B0-A1EF92B3D591}.png)

![alt text](images/{653A6A73-7B16-42FC-881B-2047F7CCE5D0}.png)

![alt text](images/{FC44C921-82C0-4188-ACA6-F9C9A41CFE5C}.png)

![alt text](images/{05775B27-6215-46FF-8162-5637A03A55EB}.png)

![alt text](images/{77A29955-0021-4A1C-8014-A1D2B8F992F6}.png)

![alt text](images/{FAD520AF-F9D2-4C02-A1E6-56B81197F9CC}.png)

![alt text](images/{F17069B2-B9F9-4B5C-897B-E56291E22C6D}.png)

## Quiz

![alt text](images/{510ADD24-9425-46A9-9389-98C5775BCD1B}.png)

![alt text](images/{467ECB7D-1A5D-460F-AC42-5848255972C1}.png)

![alt text](images/{A33BB970-9E5E-47F2-B072-A741E0C23447}.png)

![alt text](images/{7A374D88-3314-451E-9A13-512373B1B56F}.png)

![alt text](images/{CB78FAFD-19B2-4858-8385-F0E430EAEE9B}.png)
