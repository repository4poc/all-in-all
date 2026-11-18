## 1. Build your first Autonomous AI Agent with n8n (Low Code or No-Code Tool)

- n8n platform on web for AI Agentic development with low/no code
- Just need account onto it
- 14 days trial

![alt text](images1/{BBD968BC-8C09-40A3-AA23-38CAAA4B61AF}.png)

## 2. Course Roadmap

![alt text](images1/{FDEFE6EB-4812-4701-9D71-7763B2295E7B}.png)

![alt text](images1/{5A8E07CA-A0A5-41AC-90AE-D136AF03CA72}.png)

## 3. Path to become Proficient AI Engineer

![alt text](images1/{147D258D-BDC8-4A35-97AF-AE6719B9A27F}.png)

![alt text](images1/{105C1C83-CE88-4BC9-9466-1F6EDF16E128}.png)

## 4. Environment Setup - UV, API Key, Costs , GitHub Repo

- UV is python project and package management tool, make environment setup very easy

![alt text](images1/{4C06FCCD-4167-4A9C-A16F-57C9CB06FA71}.png)

AI Engineer wear two hats

- SOftware Engineer
- Data scientist

## 5. Windows Setup - VSCode, Run UV

- Install VS Code with extensions, not plugins
  - Python
  - Jupyter Notebook

- Run UV init to initialize a project

```
> uv --version

> uv init <project-name>

> uv venv

> uv add fastmcp

> uv sync

```

## 7. Setup OpenAI API Key and .env

Use Jupyter Notebooks (.ipynb) - The excecution order matters,

- Select Kerner in the .ipynb

![alt text](images1/{1221FB88-8FF1-4782-B466-81E23C5F359F}.png)

## 9. What is AI Agent? Definition, Workflows vs Agents Explained

![alt text](images1/aidefinition.png)

![alt text](images1/{2BD82283-6048-474C-8B7F-C645F681AED9}.png)

Example :

- Deep Research Workflow : Research-Review-Report
- GPT Agent : Openended in nature

## 10. AI Agentic Design Patterns - Chaining, Routing and Orchestrator

- 5 Design Patterns for workflows
- 1 Design Pattern for Agents

Help you design the solution, How to solve the problem

![alt text](images1/{C97F5698-42A8-4669-8B6B-6A7FAC742162}.png)

![alt text](images1/{0ED61CEF-262C-4871-A7B7-254E56E4F164}.png)

Eg: Customer Support system, where request route to different LLM based on the type of user request like finance query, HR query.

- Specialized LLMs for specific domain , using separation of concerns

![alt text](images1/{ED23392C-0F41-4E0C-891B-37592EE018B6}.png)

![alt text](images1/{70051EF9-E545-41CD-AC51-B6B4B84079FC}.png)

Here Orchestrator and Synthesizer both uses LLM

![alt text](images1/{07064376-E61A-44B0-8785-EA03716D6480}.png)

Also Called `LLM as a Judge`

![alt text](images1/{11DC389F-08C1-4278-B416-A7601332AAB3}.png)

## 11. Agentic AI - Risks, Guardrails, Evals and Traps to Avoid

![alt text](images1/{EE9A8DB9-3A4C-46FB-BEDF-FA527F6EFBCB}.png)

Risk Mitigations Techniques:

- Observability
- Evals : Using Evaluator-Optimizer design pattern(Also called - LLM as a Judge) can be used
- Guardrails :

![alt text](images1/{6AEE27C7-28DC-43BC-9CEC-DB9D689513F0}.png)

### Problem : LLMs are good at generating believable content

- `Focus on the Business Problem`, rather than providing the solution. Many can be solved without Agentic AI Solution

- LLMs are good at generating believable content, that might be incorrect. So to measure the success of our Agent, we have to run it against a metrix. So besides `Focus on the Business Problem`, we also have to understand how we will evaluate the `Success`

`LLM` are Models that generate plossable (Believable) tokens

![alt text](images1/{8E4FF14E-3EC7-47E0-BB33-7A90AC608A7D}.png)

Start with

- 1 LLM
- 1 Agent
- 1 Goal

![alt text](images1/{19AB26C1-D05E-4347-91EE-B6671BC54D35}.png)

We are developing AI Agents, not using Agents (claude code), so can not complaint for hallusination, we have to code for it.

![alt text](images1/{6667262F-5DED-45B4-883E-F892A671AAD0}.png)

## 12. LLM Providers: OpenAI, Anthropic, Google, DeepSeekAI, Groq & Inferance Providers : Ollama, OpenRouter

![alt text](images1/{6FF9FA90-F7B1-4CE5-978E-92270D828667}.png)

- Grok is a LLM by xAI

![alt text](images1/{AF302AD7-505F-4307-91C5-08C40BD22CC9}.png)

## 13. Setup multiple LLM API Keys and Generate a Question to Ask

![alt text](images1/{DF8D003B-B0EC-41BF-BE5B-E9C9DD1F63B5}.png)

```
from openai import OpenAI
from dotenv import load_dotenv
import os

load_dotenv()

print(os.getenv("BASE_URL"))

client = OpenAI(
    base_url=os.getenv("BASE_URL"),
    api_key=os.getenv("API_KEY")
)

question="who is modi"

messages = [
    {
        "role": "user",
        "content": question
    }
]

response = client.chat.completions.create(
    model=os.getenv("MODEL"),
    messages=messages
)

print(response.choices[0].message.conent)
```

- All providers support OpenAI client

![alt text](images1/{F76862A6-8B37-41B0-9725-8C0978BE284C}.png)

`reasoning_effort`

![alt text](images1/{952CC33F-013C-4BE9-8B5F-E97CEF44B619}.png)

## 15. Install Ollama and run Local LLMs : Qwen, Llama, GPT-OSS, Gemma

![alt text](images1/{D0963887-7BC5-44BD-811F-0322873CC61C}.png)

![alt text](images1/{1ECB7930-166C-4802-A325-46424E396064}.png)

- Qwen - From AliBaba

![alt text](images1/{726667D7-0D0F-4B93-9FDA-19BEECF60E35}.png)

- models with `cloud` run on cloud

```
ollama ls
ollama pull <LLM>:<Version>
ollama run <LLM>:<Version>
ollama rm <LLM>:<Version>

```

## 16. LLM as a Judge, Rank Multiple LLMs with an Orchestration Flow

```
name = ["varinder", "rajinder"]
age = ["42", "35"]

for name, age in zip(name, age):
    print(f"Name: {name} | Age: {age}")
```

Name: varinder | Age: 42
Name: rajinder | Age: 35

![alt text](images1/{DDE850C9-AB4E-42E0-8827-3A670A72FEAB}.png)

Ensure use different model for LLM as a Judge (Evalutor-Optimizer Design Pattern)

![alt text](images1/{6156D84C-AD44-4174-899E-6B8CEE118470}.png)

![alt text](images1/{3FA5F4D0-C12C-48D1-9099-8962AC77F84C}.png)
