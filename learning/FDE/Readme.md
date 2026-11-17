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

## 18. Recap

![alt text](images/{33B64140-23BF-46B5-8B07-1D2E4C59BD0B}.png)

![alt text](images/{C0B24C0C-9F0F-43BB-93B5-EBFBF35D9375}.png)

## 19. Vibe Coding, The Coding Agent Landscape and the rise of Claude Code

### Vibe Coding

A way of building software by describing what you want in normal language and letting AI write much of the code for you.

![alt text](images/{D46B9457-5285-4968-AEFB-E01D17D596F0}.png)

![alt text](images/{0FF08905-A044-4B2B-99B4-CD7C87F5E870}.png)

## 20. Choosing and installing Coding Agent

![alt text](images/{5591F315-8EE2-42FE-BCBC-335683CD34C7}.png)

![alt text](images/{AFF7E3B1-EC28-4578-A84C-1025F281D55F}.png)

![alt text](images/{7E2FE7AE-9E90-4074-8B37-002032EA2CB7}.png)

https://academy.claude.com/courses/claude-code-101/installing-claude-code

![alt text](images/{76895D04-11C3-4F19-B2A3-D2B27B1D3993}.png)

![alt text](images/{AD380CF1-4E6A-4F89-A5BF-59BE2457C060}.png)

## 21. Working with Coding Agent

![alt text](images/{4CD54A40-0DA4-46DA-8672-C6C4CCA5D1AE}.png)

All coding agent look for file `AGENTS.md`

![alt text](images/{75E745A6-B1B7-4E8F-8A8D-92931406B85E}.png)

Instead of putting in coding standards, just mention where it can find it like

`For Coding standards look into docs directory and look into codestandards.md file`
`For Frontend look into the frontend-codingstandards.md`
`For Backend look into the backend-codingstandards.md`

This strategy is called `Progressing disclosure`, break down files rahter than having everything in one file.

![alt text](images/{AACB408F-FA19-4707-83E8-EF1AC0D6F8B6}.png)

`Decision Fatigue` : Manual Mode leads to that

## 22. Working with Claude Code

![alt text](images/{2038E37C-21C8-47E2-8457-1DDB8EC73B36}.png)

![alt text](images/{1D0E05B7-011F-43C4-ADC0-AA2408790D9C}.png)

![alt text](images/{F450E6A5-90DC-4EB2-92CB-E9B731B782DE}.png)

![alt text](images/{5B30A475-B040-4E1F-AD41-3E0A5D1C7365}.png)

![alt text](images/{752F9632-00BB-44C9-8FFF-32D3CACDE5EC}.png)

![alt text](images/{C3F01355-11D4-4930-8A05-C7C9247906A3}.png)

```
        servers = [
            "time-mcp-server",
            "web-mcp-server",
            "https://learn.microsoft.com/api/mcp",
            {
                "name": "playwright",
                "url": "https://eastus.mcp.playwright.microsoft.com/playwrightworkspaces/7a707f88-790d-45bf-9cb5-a501a6ae2135/mcp",
                "headers": {
                    "x-api-key": os.getenv("PLAYWRIGHT_WORKSPACE_TOKEN")
                }
            }
        ]
```

## 23. ADLC (Agentic Development LifeCycle)

![alt text](images/{CC1B1ABA-8D79-4773-B807-F119C9E9E0B5}.png)

![alt text](images/{641C45A8-AB87-4DCF-BEF9-58C97D2903A3}.png)

![alt text](images/{25136543-DDF8-4284-877A-467D6F870415}.png)

![alt text](images/{6DFD64E4-63E7-4FC5-B97E-AE41A4D4CE6D}.png)

`AGENT.md`

![alt text](images/{E591141A-4BA2-486D-B0B0-BCF367F992B2}.png)

![alt text](images/{4A6CAA44-ACD5-4878-B93D-10D317E0410F}.png)

![alt text](images/{16358448-586E-4A17-B352-3BD2629D704F}.png)

### Spec Driven Development

`spec.md`

![alt text](images/{362B4C7A-84EF-4DA4-9045-A4D44421A57B}.png)

![alt text](images/{8E384316-F5D8-4941-8AB0-18545BCBBC6F}.png)

`brand.md`

![alt text](images/{AF73E949-717E-41B3-BB6A-4283BADB6A27}.png)

`techstack.md`

![alt text](images/{A64DC916-2FD2-4471-8F0A-6C0928A5FAC6}.png)

`process.md`

![alt text](images/{766B1166-E353-4052-8BB0-30F1394F5FBD}.png)

![alt text](images/{38C35D5F-CD59-40E7-B911-B696F8175047}.png)

`codestandards.md`

![alt text](images/{70846F50-9FEA-4EE6-B7A7-312238ABE64C}.png)

## 24. Hardening the repo

![alt text](images/{C4B7C298-70B2-4854-85D7-43A36F7FCEF3}.png)

![alt text](images/{9D740F3B-7778-4367-A2D8-45298D9050D9}.png)

![alt text](images/{0BA5F3FD-4E1E-4CC0-A73C-F9B4E67D5A58}.png)

![alt text](images/{B7B56498-65A9-4102-B083-9AD73551E9FE}.png)

## 25. Spec Driven LifeCycle

![alt text](images/{604D644A-2583-462A-B39E-E7FC3CF378A6}.png)

![alt text](images/{06E8F349-4364-4D8C-80CA-437510DC2D49}.png)

![alt text](images/{E359A7ED-8CA7-457E-97D1-96AF7A1B8372}.png)

## 27. Inference Basics

![alt text](images/{C0914D28-78A0-42FB-9CD7-0828B2E511DF}.png)

![alt text](images/{1E5CCD7D-0453-4AC5-A620-A49659D056A8}.png)

![alt text](images/{1C27BD9E-C1F3-4792-A294-C36326BF3884}.png)

![alt text](images/{CA2F0500-3809-4637-BCA3-DE60C1CCCB71}.png)

## 38. Proprietary vs Open-Source and Open-Weight LLMs

![alt text](images/{7C5DFC3B-ED57-4AEB-BB92-5B82D32DB7FD}.png)

`Groq` : Inteferance Provider, run open-source model on your behalf.
`Grok` : Elon Musk LLM from X.ai from spaceX

`Openrouter`, does not run the inference itself, Just provide an API. It forward your request to one of many providers - one is Groq

![alt text](images/{CB5297D5-7D3A-44F4-B2FF-674445940DCD}.png)

- Workday
- MS Office365 for Company emails
  These are 3rd Party services like LLMs, so sercurity audit teams can audit these things to ensure they have proper controls in place to safeguard your data.

So its a technology dependency, so we have to trust like AWS. and if in case we cant tolerage our data leaving our site, then it is good ground to run Open-Source Model on premise.

![alt text](images/{26D66CF7-A0B8-4D47-9D6B-045CAB6B101A}.png)

![alt text](images/{E2579C45-0A3C-416C-B41E-5CC148B4551D}.png)

## Dense vs MOF (Mixture Of Experts) Models and Active Parameters

![alt text](images/{E182C7BD-BFE6-4098-955C-0190450E46E5}.png)

![alt text](images/{86245245-114D-4A26-BC19-622654CE4A11}.png)

![alt text](images/{41A3A498-9AEE-42E2-9021-EF6BB5F1B56E}.png)

MOE model are faster then Dense LLM

## 31. Ollama, LM Studio, IIama.cpp,vLLM and Choose Hardware to Run LLM Locally

![alt text](images/{93237EB1-7E9E-46E3-97B7-F567B98F1F84}.png)

![alt text](images/{58786257-5CB3-4487-9B96-6D71F02C7D83}.png)

![alt text](images/{78EC96C2-EFF9-4B03-89B5-32FACF72733A}.png)

![alt text](images/{E76DAAFA-FCEE-4339-97BC-B483AC66A735}.png)

![alt text](images/{6576EC6A-9E36-484E-867F-0B2C06393789}.png)

![alt text](images/{B69ACA48-695C-4DCB-AE8B-6F80EAC5C46F}.png)

Mac Mini - Qwen-27B ($ 2500)
Mac Mini - Alternative - Qwen-27B ($ 2000)

## 32. Install Ollama and Chat with Qwen 3.5 and Gemma 4 on your own computer

![alt text](images/{511C9E90-18B4-48CF-A6AA-F294DDD9CEE3}.png)

![alt text](images/{A8DE23A8-DBCD-4EE9-BFA5-3D9A326C8706}.png)

![alt text](images/{3A889264-D81F-481A-B138-192CC0534087}.png)

![alt text](images/{9FA7FBC0-C235-4344-A0A9-DEB497AA519C}.png)

![alt text](images/{7AFEEA22-988B-4F73-B0EE-5A365CC69161}.png)

## 33. OpenRouter Provider Routing, Token Pricing and the cost per claim

![alt text](images/{4A8E888F-5632-4999-B71A-72933E09E7E1}.png)

![alt text](images/{D86AB268-E0DF-4D8A-A267-9F05FCAD53F7}.png)

![alt text](images/{5416126E-99F9-4ED9-BC5F-484458354AF8}.png)

![alt text](images/{78DD0849-D611-4333-8B9E-0A063969A16B}.png)

![alt text](images/{ADCCDC10-AA8D-496D-AA0A-DBB23F38E19D}.png)

![alt text](images/{B38C2F1B-6190-4775-AC4E-8635CCF606B1}.png)

## 34. Testing DeepSeek, Kimi K3, GLM, MiMo, Nemotron and Mistral on Claims

![alt text](images/{2913741A-815A-47BB-8002-1BFDC7DBEEB5}.png)

![alt text](images/{9E4EEAED-643B-4188-8E42-17264AC188D5}.png)

![alt text](images/{3FDC0CBF-FCEA-4923-8CB5-8A3082E90EE4}.png)

![alt text](images/{66AE56B8-EFA4-4BCB-930D-AFA17B295D5B}.png)

![alt text](images/{BB384B32-B60E-4295-AB23-51F30CDCC28B}.png)

![alt text](images/{99D69FD4-62B9-4C8A-8A0E-4E8748B279EC}.png)

![alt text](images/{4A0B2EC4-9DB8-44DE-8456-111A489D5967}.png)

![alt text](images/{E6290FA5-AD14-4107-A824-ABBE083CF5E7}.png)

![alt text](images/{1CE4604F-1B4E-4047-819E-D95C3E161720}.png)

## 35. Practical Example must revisit

TDB

# 36. Recap of Open-Weight vs Proprietary Models, Distillation and Token Cost

![alt text](images/{F335B8F9-85D4-4C6A-9498-04C592F616E4}.png)

![alt text](images/{186857F6-D8AF-4606-A943-0E1D8CF7A37A}.png)

![alt text](images/{559110FA-5AE5-4A57-9684-4B3B91587DDD}.png)

![alt text](images/{739CB638-CBA3-4009-9CFA-72983FC4C689}.png)

## 37. Harness Engineering
