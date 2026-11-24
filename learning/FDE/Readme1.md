Source :: Master AI Agents in 30 days: build 8 real-world projects with OpenAI Agents SDK, CrewAI, LangGraph, AutoGen and MCP.

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

- Software Engineer
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

## 9. What is AI Agent? Definition

![alt text](images1/aidefinition.png)

### Agentic Sytem types

- Workflows
  - Static in nature
  - predefined code path of execution
- Agents
  - Dynamic in nature
  - Open ended, no fixed path of execution

![alt text](images1/{2BD82283-6048-474C-8B7F-C645F681AED9}.png)

Example :

- Deep Research Workflow : Research-Review-Report
- GPT Agent : Openended in nature

## 10. AI Agentic Design Patterns

- 5 Design Patterns for workflows
  - Prompt Chaining
  - Routing
  - Parallelization
  - Orchestrator-Worker
  - Evaluator-Optimizer
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

- Monitoring (Observability & Evals - Using Evaluator-Optimizer design pattern Also called - LLM as a Judge)
- Guardrails

### Problem : LLMs are good at generating believable content

1. `Focus on the Business Problem`, rather than providing the solution. As many business problems can be solved without Agentic AI Solution

2. `Focus to how the uccess will be measured`: LLMs are good at generating `plausible` believable content, that might be incorrect. So to measure the success of our Agent, we have to run it against a confusion metrix.

3. Start Simple with
   - 1 Agent
   - 1 LLM
   - 1 Goal

We are developing AI Agents, not using Agents (claude code), so can not complaint for hallusination, we have to code for it.

![alt text](images1/{6667262F-5DED-45B4-883E-F892A671AAD0}.png)

## 12. LLM Providers:

LLM Providers:

- OpenAI - GPT
- Anthropic - Claude
- Google - Gemini
- DeepSeek -
- Groq
- Ollama - Qwen

Inferance Providers

- Ollama
- OpenRouter

Note

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

![alt text](images1/{9711CEE4-3243-41EB-8912-1F6B111DFED3}.png)

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

![alt text](images1/{44BC5CD3-2488-4396-9B91-CCFCA2E11A00}.png)

![alt text](images1/{6946C1F8-90C9-4FCD-881A-8E3F27D90829}.png)

![alt text](images1/{DDE850C9-AB4E-42E0-8827-3A670A72FEAB}.png)

![alt text](images1/{895DFDAF-F552-489E-9D22-EFFB83351DFB}.png)

Ensure use different model for LLM as a Judge (Evaluator-Optimizer (EO) Design Pattern)

![alt text](images1/{1776D44E-3A37-4AD0-92C1-1B41E9C0F2F7}.png)

![alt text](images1/{6156D84C-AD44-4174-899E-6B8CEE118470}.png)

![alt text](images1/{3FA5F4D0-C12C-48D1-9099-8962AC77F84C}.png)

## 17. The Agent Landscape Frameworks, Runtimes, Tools and Builders

![alt text](images1/{8EE8DC48-AB8B-4943-96B0-AFE66BA4598A}.png)

![alt text](images1/{B6AF35B4-6940-4C51-A1C9-B3FA10C24A67}.png)

![alt text](images1/{BDEC0134-8A39-4C09-B039-4BA1A81E3457}.png)

## 18. How Tool Calling Works : The Truth Behind AI Agent Autonomy

![alt text](images1/{71DDE83A-8586-41D9-9917-FA465007D03F}.png)

![alt text](images1/{1C22AE78-3337-453D-9E92-7ABCDE416F00}.png)

![alt text](images1/{C6D711F4-17C4-4FCE-925B-187FCC267255}.png)

![alt text](images1/{103B1234-D646-4D62-8E69-2CD41D7885EF}.png)

## 19. Start the Digital Twin : Read a PDF and Master System Prompts

![alt text](images1/{6DBBFC0E-A1ED-4672-988D-40EB0A3CB557}.png)

![alt text](images1/{CBF01A99-D3E3-4B91-8E80-355B89D6168D}.png)

![alt text](images1/{4EDAE6E2-915B-441D-BF34-FFC4FEA7A675}.png)

- System Prompt
- Tools
- Memory
- Conversational History

![alt text](images1/{3CEBBDEA-4B58-4510-A349-FE16D51B8A95}.png)

## 20. Build a Chat UI with Gradio and Define your First Agent Tool

![alt text](images1/{B145666D-FFC5-40E8-9A54-F0D4E6CBD7F3}.png)

![alt text](images1/{369050A8-1421-44C9-A80A-800A4223353D}.png)

```

from openai import OpenAI
from dotenv import load_dotenv
import os
import gradio as gr

load_dotenv()

client = OpenAI(
    base_url=os.getenv("BASE_URL"),
    api_key=os.getenv("API_KEY")
)


def chat(message, history):
    messages = [
        {
            "role": "system",
            "content": "You are an AI Agent"
        }
    ]

    # Add previous conversation history
    for item in history:
        if isinstance(item, dict):
            messages.append({
                "role": item["role"],
                "content": item["content"]
            })
        else:
            # Older Gradio history format: (user, assistant)
            user_message, assistant_message = item
            messages.append({"role": "user", "content": user_message})
            if assistant_message:
                messages.append({"role": "assistant", "content": assistant_message})

    # Add current user message
    messages.append({
        "role": "user",
        "content": message
    })

    response = client.chat.completions.create(
        model=os.getenv("MODEL"),
        messages=messages
    )

    return response.choices[0].message.content


demo = gr.ChatInterface(
    fn=chat,
    title="My Chatbot",
    description="Ask me anything!"
)

demo.launch(inbrowser=True, share=True)

```

![alt text](images2/{FC9A7882-78C4-4464-ADD1-FD8A83115449}.png)

## 22. Context Engineering for AI Agents : Memory, Tools and RAG

Context Includes:

1. Prompts - System & User & Assistant
2. Structured Output
3. Tools
4. RAG
5. Memory - Short/Long Term

![alt text](images1/{E7E9A52A-CC71-4BFD-9B55-42BDDE97D7D1}.png)

## 23. Add Tools to your Agent with Pushover Notification

![alt text](images1/{E6AC9CFB-D2BA-49BE-ACC9-08AFE7E4F5CF}.png)

![alt text](images1/{7EF65FCA-8602-4037-ADC2-0C9383894686}.png)

![alt text](images1/{6F592BB5-D40E-4105-B1FD-E593493249AD}.png)

![alt text](images1/{48109B2A-E0AC-4905-B4B7-126FA2D5E8A4}.png)

## 26. Refactor the Python Notebooks into Python Modules and Run it

![alt text](images1/{3CFDA98D-6371-4728-B095-84974A635DD2}.png)

![alt text](images1/{E5948628-9DB4-4637-9624-6DA80F226A4F}.png)

![alt text](images1/{B073C762-6163-427F-8311-1CC53297B321}.png)

![alt text](images1/{9B216847-794A-496D-87A4-634B05A765F5}.png)

![alt text](images1/{6EA4E305-AF9C-48AC-A748-1335866099E7}.png)

```
uv run app.py
```

![alt text](images1/{82EE55B0-37B2-4D8E-A285-0FC4108FD79D}.png)

## 27. Deply your Agent to HuggingFace spaces

![alt text](images1/{F2EA0F48-A032-4CCF-B3FE-44B04D3CFFD1}.png)

## 29. OpenAI Agent SDK Intro and Async Python (asyncio) Explained

![alt text](images1/{FB328E0C-C9FF-4F45-B288-4E0CA3BBA59D}.png)

![alt text](images1/{EC9307F0-38DB-4117-AF53-2A341E756D32}.png)

![alt text](images1/{F1FDDB3D-FE64-408E-96BF-B82C1C53F5AB}.png)

`Preemptive Multi-Taskiing`

![alt text](images1/{A3AF74BC-3F41-431F-A257-63969C3C458C}.png)

## Gradio vs Streamlit

![alt text](images1/{1355D85D-5A42-4A09-A455-6EB42E5CE308}.png)

## 30. OpenAI Agents SDK Core Concepts : Agents, Trace adn Runner: run

![alt text](images1/{6CC737ED-4B96-44EE-BA55-4906B0A5BA0F}.png)

![alt text](images1/{C26B6936-F532-4339-ADA2-FC2549F35B90}.png)

![alt text](images1/{BC19F238-D51A-4098-A00F-F66D2EFC8720}.png)

![alt text](images1/{8D50B974-5955-4716-A716-D9727B00849B}.png)

## 31. Build your first Agent with OpenAI Agents SDK: Runner and Tracing

![alt text](images1/{C52663EB-5933-416B-811C-562A86877869}.png)

![alt text](images1/{3E47CBCC-130E-4830-B40B-7C3783845E32}.png)

![alt text](images1/{09ACA5C5-7BA2-486B-B1F3-F3381B76D2DB}.png)

![alt text](images1/{691A0CD6-CB85-4248-942A-4001DC535D69}.png)

![alt text](images1/{59DFE32A-478C-4275-B5B4-34F034ED304E}.png)

![alt text](images1/{E92DD503-A181-4C17-95A3-2B4C1067A999}.png)

![alt text](images1/{DF5D3655-692E-4692-81C9-CEAF51228E8F}.png)

![alt text](images1/{57F90EDB-174B-428A-A8A6-32D6CEDC80CF}.png)

![alt text](images1/{B402E1AE-18AC-485B-830F-8CE974CFC081}.png)

## 32. Add Tools to an Agent with function_tool Decorator

![alt text](images1/{EDC507A3-126C-4FE1-8FB0-27736778185A}.png)

![alt text](images1/{FE886BBA-5580-4434-BD17-5C40C1E5A1B0}.png)

![alt text](images1/{74182587-BC61-42C9-8BA9-8F346A8DDD96}.png)

![alt text](images1/{19F9D7B3-F9D5-490A-BA97-A55B98949B39}.png)

## 33. Adding Memory to Agents with Sessions and SQLiteSession

### Approach 1

![alt text](images1/memory1.png)

### Approach 2

![alt text](images1/{268DA6AD-9701-4E06-82DF-68068F2AE214}.png)

![alt text](images1/{B8777E25-E863-402C-A3EB-811AC44097E3}.png)

- You can use sessionId
- You can use Compression to compress the coversation history
- You can ask the user to have fresh session after a particular conversion count

## 34. Build a Multi-Agent Sales Team and Orchestrate Agents Three Ways

![alt text](images1/{BF631A1B-762A-42CE-B6F3-4ACA9D0DF1F7}.png)

## 35. Setup Email sending Tools for Your AI Sales Agents

Popular Email Platforms

- SendGrid
- Resend
- Using SMTP Server of Gmail, MS 365, Outlook

![alt text](images1/{929DD078-D86C-4198-B7D3-FF02BB03B5CD}.png)

![alt text](images1/{2CF5C206-D3FE-4B35-B694-B613E0C2FAA9}.png)

![alt text](images1/{F9F1C719-0B27-42F2-A7A1-35D74A4EF0C2}.png)

![alt text](images1/{3996A1F1-E31D-4407-84B5-6523A5857976}.png)

![alt text](images1/{33703242-4685-4C62-990C-C9155B9111E4}.png)

![alt text](images1/{EF361C50-1C09-45D9-9ACB-4729DD907EDA}.png)

![alt text](images1/{80396C23-BA9F-4DBB-928B-B7A9F99B96D1}.png)

![alt text](images1/{43A353D8-5E4E-443B-8595-35A52315DB7B}.png)

## 36. Agent Orchestration by Code vs by LLM in the OpenAI Agent SDK

![alt text](images1/{0EF3B8E3-3FB0-4ED7-BBE3-ABE0BDC61CBE}.png)

In most production systems, Orchestration by Code is preferred over Orchestration by LLM

- Predicatable
- Deterministic

![alt text](images1/{B8AD3A77-8087-4527-9E21-2E4EF41DE4B6}.png)

- Cloude Code
- Codex
- Cowork

Full Autonomy

## 37. Orchestrate Multiple Sales Agents by Code with asyncio.gather

![alt text](images1/{8134F050-2996-42C7-BE57-FDD576D301CF}.png)

![alt text](images1/{0835BB3C-3111-4C8D-9C4B-E553C2DD85AA}.png)

![alt text](images1/{E17CC882-5D9D-40C1-8252-B1FDE0B0FA31}.png)

![alt text](images1/{44569737-7F20-4FB8-A8B1-5196D86A9F7A}.png)

- asyncio.gather , agents run in parallel

![alt text](images1/{E614FC17-B247-4D87-9244-D02B65BBD30C}.png)

![alt text](images1/{521EDE1D-9ECF-436D-908A-D4A977EC5651}.png)

![alt text](images1/{B86506BE-2975-4E63-AE3D-26AFA8564BC1}.png)

## 38. Orchestrate Agents as Tools with a Sales Manager Agent

![alt text](images1/{A2904883-6470-40D2-BBF3-26880CD78523}.png)

![alt text](images1/{54DB2285-38D3-40AC-958F-09E999160237}.png)

![alt text](images1/{B5424BF5-7130-45B7-ABF0-B47C0C419B09}.png)

![alt text](images1/{8A09AF46-1DD2-44C1-B3DE-56EE0EC09FD1}.png)

## 39. Orchestrate Agents with Handoffs in the OpenAI Agents SDK

Ignore

## 40. Use Any LLM with the OpenAI Agents SDK: Gemini, Groq adn More

![alt text](images1/{B904C675-E2F3-4736-82E6-BDD20976F791}.png)

![alt text](images1/{35D34640-3457-4CAB-B1BE-855A5E7E99F9}.png)

![alt text](images1/{985A668A-634B-458F-A548-BE5B8A032FAB}.png)

![alt text](images1/{24957463-7054-4DEE-8706-77BF57234CF2}.png)

![alt text](images1/{0570C4F0-4B50-4DE5-B39C-D7D1CC92A623}.png)

![alt text](images1/{D072B647-9B04-4DBD-A35A-6331BD617320}.png)

## 41. Structured Outputs with pydantic in the OpenAI Agents SDK

![alt text](images1/{7B2FBF26-F03E-462E-94D1-7516DAE158BB}.png)

![alt text](images1/{B680970C-B085-4DF9-ABCB-F9EB9AAC90FA}.png)

![alt text](images1/{634CDA7C-AC3C-40F7-8EA3-0F7632E6F6F4}.png)

## 42. Guardrails

![alt text](images1/{753B2F05-1123-4B50-8B3F-F67BE4033F2A}.png)

![alt text](images1/{55D62689-1021-4FB6-A56C-ACBDC19AEC2E}.png)

## 42. Build Guardrails for AI Agents Plus Sandboxes and MCP

Ignore

## 43. Plan a Deep Research AI Agent with the OpenAI Agent SDK

![alt text](images1/deepresearchAgent.png)

![alt text](images1/{36FCC16F-D7C3-4F76-BEBF-B4A7AE77CA14}.png)

## 44. Build a Web Search Agent with OpenAI Agent SDK Hosted Tools

![alt text](images1/{9BAE3477-91B8-4125-A4EB-8242FF9E9F1A}.png)

![alt text](images1/{0630EFE2-BB5B-451D-9407-C181D247A98C}.png)

Drawback:

- Charge you
- OpenAI specific (vendor locking)

## 45. Build Planner, Writer and Email Agent with Structured Outputs

![alt text](images1/{F1E0D898-623F-49FD-B5E7-7831C261FA34}.png)

![alt text](images1/{A6504830-7220-4D39-BD74-3FBE76F7A5FF}.png)

![alt text](images1/{07804665-ECB8-43CF-A0E4-481FE271F80D}.png)

![alt text](images1/{7E919F90-C1DC-4D49-9BA9-AB2E152F5AE4}.png)

![alt text](images1/{CED6A22A-A3C2-4592-B9A6-786F0452AE0F}.png)

## 46. Run Full Deep Research Agent Orchestrated by Code

![alt text](images1/{977DF211-7C49-42F6-AB2D-53CAC9B4B424}.png)

![alt text](images1/{5A712A55-B3BF-4BE5-AB69-6470B047FC5E}.png)

![alt text](images1/{11DED383-4549-4CAF-A086-658D138D928C}.png)

![alt text](images1/{46D2B301-B529-430E-8D5A-BDBC56CB3283}.png)

## 47. Turn your deep research agent into python modules for deploy

![alt text](images1/{903EDB58-CBB8-44F6-8FAB-54BE5A1C674C}.png)

## 49. Deploy to hugging face

![alt text](images1/{1A518632-C10A-4B5A-919D-C6D91B5165C9}.png)

## 69. LangChain Ecosystem

LangChain is a Company,which has multiple tool ecosystem.

![alt text](images1/{5D067764-F9F9-40A8-9BD3-18ED484937AA}.png)

![alt text](images1/{41FCC107-E7CC-4E36-A20F-4900B1B89C68}.png)

![alt text](images1/{EEB08DF4-C675-4212-A8C7-E42840F3EDFE}.png)

## 70. LangChain Core Basics: ChatOpenAI, Invoke, Stream and the Tool Decorator

![alt text](images1/{09030DD5-5504-4172-AB3C-B7B4A2C101A6}.png)

![alt text](images1/{7A9C1661-2EF8-4123-A9A6-4D8878D7140A}.png)

## 71. Bind Tools and Structured Outputs

```
class Person(BaseModel):
    name: str = Field(description="Person Name")
    age: str = Field(description="Person age")
    birthplace: str = Field(description="Birth palce of the person")

llm = ChatOpenAI(model = os.getenv('MODEL'),base_url=os.getenv('base_url'),api_key=os.getenv('api_key'))

llm_with_structured_output = llm.with_structured_output(Person)

messages = [
     {"role":"system","content":"You are an AI Agent"},
     {"role":"user","content":"Tell me about Narendra Modi"}
]

response = llm_with_structured_output.invoke(messages)
print(response)
```

```
name='Narendra Modi' age='76' birthplace='Vadnagar, Gujarat, India'
```

## LangGraph : GraphState, Nodes, Edges and Reducers

![alt text](images1/{EF1D5173-1CBD-43AD-BDD7-202675FD6639}.png)

![alt text](images1/{B99B426A-B604-4C9F-B33C-5626246667D0}.png)

Edge Types:

- Straight Edge
- Conditional Edge

![alt text](images1/{CFF00514-8484-4D29-9499-FB8ED47E0113}.png)

![alt text](images1/{93B44A6A-3077-4543-8DAA-6A6F70B063A7}.png)

![alt text](images1/{437A443E-3804-4B84-9CE3-B5683F0475CD}.png)

## 75. LangSmith

![alt text](images1/{656AEEC8-185D-4235-ABA7-92A7FE527848}.png)

![alt text](images1/{DD03357E-FD66-422D-821E-9E768B1B7A60}.png)

## 76. LangChain create_agent

![alt text](images1/{2FA678AA-7781-43F7-90DA-3F7D9A73DF45}.png)

![alt text](images1/{9E13C331-F845-4E4D-8641-1994E1FE1391}.png)

![alt text](images1/{8184C2AD-8AA4-4A22-99E2-0DB2B7778404}.png)

![alt text](images1/{B3701354-BC46-4913-9228-AA22C7E9D0B2}.png)

## 77. Build a simple Agent with LangChain using create_agent

```
import os
from dotenv import load_dotenv
from pydantic import BaseModel,Field
from datetime import datetime

from langchain_openai import ChatOpenAI
from langchain_core.messages import HumanMessage, SystemMessage, ToolMessage
from langchain_core.tools import tool
from langchain.agents import create_agent
from langchain.agents.middleware import wrap_tool_call
from langgraph.checkpoint.memory import InMemorySaver
from langchain_mcp_adapters.client import MultiServerMCPClient

load_dotenv(override=True)

agent = create_agent(
    model=f"openai:{os.getenv('MODEL')}",
    system_prompt="You are an expert AI Agent"
)

messages = [
    {
        "role":"user",
        "content":"How are you"
    }
]

response = agent.invoke({"messages":[
    {
        "role":"user",
        "content":"How are you"
    }
]})
print(response["messages"][-1].content)

response = await agent.ainvoke({"messages":[
    {
        "role":"user",
        "content":"How are you"
    }
]})
print(response["messages"][-1].content)

```

![alt text](images1/{8D3E7283-EFD9-4A10-9D09-ECD7D9D3C72F}.png)

![alt text](images1/{AAA341D9-8778-4CA7-BF6B-11345C8FBE51}.png)

## 79. Browser Automation with Node and Playwrite

### Install Node

![alt text](images1/{C4972F27-CAB6-4397-9D78-8E9FE141CA9F}.png)

![alt text](images1/{0334D135-51C1-491A-8C5E-C20B6372CE37}.png)

```
from IPython.display import Image, display

!npx -y playwright@latest screenshot --channel=chrome https://navbharattimes.indiatimes.com/ snapshot.png

display(Image(filename="snapshot.png"))
```

## 80. Build a Web Research Agent with create_agent and playwrite MCP

![alt text](images1/{2A475D51-33C2-439E-BB0D-4B01738A565D}.png)

![alt text](images1/{EC7E2AA6-1CF6-4A9C-B012-E6C4AA7EB8E7}.png)

```
import sys
import subprocess
from functools import partial

import os
import string
import secrets

from datetime import datetime
from dotenv import load_dotenv
from pydantic import BaseModel, Field

from langchain_core.tools import tool
from langchain.agents import create_agent
from langgraph.checkpoint.memory import InMemorySaver
from langchain.agents.middleware import wrap_tool_call

load_dotenv(override=True)

from langchain_mcp_adapters.client import MultiServerMCPClient

if sys.platform == "win32":
    print("Windows -------------------------")

    import langchain_mcp_adapters.sessions as mcp_sessions

    # Suppress MCP server stderr output on Windows
    mcp_sessions.stdio_client = partial(
        mcp_sessions.stdio_client,
        errlog=subprocess.DEVNULL
    )

else:
    print("Non Windows -------------------------")

client = MultiServerMCPClient({
    "playwright": {
        "transport": "stdio",
        "command": "npx.cmd",
        "args": [
            "-y",
            "@playwright/mcp@latest",
            "--isolated"
        ],
        "env": {
            "PYTHONUNBUFFERED": "1"
        }
    }
})

browser_tools = await client.get_tools()

print(f"Loaded {len(browser_tools)} tools")

for t in browser_tools:
    print(f"Tool: {t.name}")


browser_agent = create_agent(
    model=f"openai:{os.getenv('MODEL')}",
    tools=browser_tools,
    system_prompt="You are a web research assistant. Use the browser tools to complete the tak, then report clearly"
)

result = await browser_agent.ainvoke({
    "messages": [
        {
            "role":"user",
            "content": "Go to https://news.ycombinator.com and tell me the title of the three stories on the front page."
        }
    ]
})

print(result["messages"][-1].content)
```

## 86. SideKick Project

![alt text](images1/{EB0493C8-870C-4A79-9AB2-5EA560B9975B}.png)

![alt text](images1/{EDFA73F8-2A6F-48BA-8509-6247E9E9E0EA}.png)

## SideKick Project Implementation

![alt text](images1/{E00CEA1D-0DE3-4C1E-A70A-7E0018ABAFD4}.png)

![alt text](images1/{CE6939E2-ACFF-4418-B83C-E7AAE76D158B}.png)

![alt text](images1/{F5C2A923-DD4D-4E84-99C1-DB9FCBA264C7}.png)

---

---

---

---

---

---

---

---

---

---

---

## 109. Agent Frameworks and Agent Harness

![alt text](images1/{36C716C1-45F0-4445-863A-F40265C7AB10}.png)

![alt text](images/{3F0B7E16-069A-444A-8CB0-AB26B47E8D94}.png)

![alt text](images1/{ABA3C378-AEB6-42EA-84DB-24EBEB2A6054}.png)

## 110. What is MCP? Host, Client and Server Architecture Explained

![alt text](images1/{769B2998-6E9E-4D1F-AD48-FEB7732854AC}.png)

MCP covers

- Tools
- Resources
- Prompts

- LangChain provide prebuild tools

  https://docs.langchain.com/oss/python/integrations/tools

![alt text](images1/{A380E094-D8F8-46C6-8448-5C530E105C40}.png)

![alt text](images1/{EF71B9F5-C96F-4514-B427-50620384B606}.png)

## 111. How MCP Works: Local vs Remote MCP Server and Calling Tools

![alt text](images/{AE0DFEE0-1D48-4FCF-940F-D4421FC2D806}.png)

An AI agentic application can have multiple MCP clients, and each MCP client can connect to a different MCP server.

This is actually a common architectural approach when building agentic applications.

## 112. Exploring Node-Based MCP Servers and Tool Access

![alt text](images1/{2D52BB60-3EE7-45FF-904B-432F27F47FF0}.png)

![alt text](images1/{E125B0E8-FAD6-46C3-9C38-260E83847D29}.png)

![alt text](images1/{65510B38-033F-4ECB-BD15-8EF71E5F6E87}.png)

## 116. Why Build your own MCP Server and when not to

![alt text](images1/{99254FDD-6CFD-4084-B15D-A9D1CBFAB0BD}.png)

There is no use of using MCP, if you are using your own tool locally

## 116. MCP Marketplaces and Building an MCP Server with FastMCP

![alt text](images1/{31092296-2E07-403C-B225-0C0A2EAAD623}.png)

`https://glama.ai/`

![alt text](images/{211DDD07-8800-4B85-B9FD-B99F93B60056}.png)

`https://www.smithery.ai/servers`

![alt text](images1/{BDE2BE1E-CBEB-4617-AD24-F87CEA91AB9B}.png)

![alt text](images1/{681C3605-E05E-4A4B-98F9-720B46300172}.png)

![alt text](images1/{692290AD-B4F4-494E-990E-506372102D06}.png)

![alt text](images1/{17028DE0-C502-45B6-9E44-447AFAAB574F}.png)

![alt text](images1/{BF51E25E-B7D4-4752-AD4D-6D7DEAE0A7E2}.png)

## 119. Context Engineering for AI Agents with MCP Servers

![alt text](images1/{8F19E372-5FBA-46D4-9723-E6ED441E4598}.png)

![alt text](images1/{97FF94AE-1437-4F30-BF2D-5CAB09764F3A}.png)

![alt text](images1/{D63DF8FF-0AF5-467D-9040-D2C7F36EF4CB}.png)

## 120. Long-Term Memory and Web Search MCP Servers for AI Agents

![alt text](images1/{13CF24E6-4D4F-42D5-B0E6-FB19FF84390D}.png)

![alt text](images1/{64246E76-5FD2-4B5A-9CD5-A76FED529F20}.png)

![alt text](images1/{1BF7437E-129B-45DA-9C75-D7827D3FE086}.png)

![alt text](images1/{A84D49D0-91E0-486F-B46B-1AF9E573248A}.png)

![alt text](images1/{16B3D50B-F54B-4A32-9D30-6E1E7F985A3C}.png)

![alt text](images1/{6399811B-E839-4271-B79A-9D0E71A66321}.png)

![alt text](images1/{235A37F8-943B-4C85-ADF5-666A5D7E04E0}.png)

![alt text](images1/{38B1F5B2-B85E-4ADF-A682-4BAEE481AB1C}.png)

![alt text](images1/{E393A56B-2263-41FA-97A4-AABA870A866D}.png)

## 121. Agentic RAG with the Qdrant MCP Server for AI Agents

![alt text](images1/{2D1B276B-B6BE-4762-9DA0-965885769901}.png)

![alt text](images1/{4E2A2EBD-A443-46AE-83C4-DB0592BD1ADD}.png)

![alt text](images1/{B291DE52-02E5-4D82-9920-F374E91049EE}.png)

![alt text](images1/{7838B895-8F6B-41D3-9A69-71350BEF94E5}.png)

```

```
