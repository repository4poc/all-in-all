## AI Assistant

A software application that understand your instructions and provide useful responses.

## Popular AI Assistants

- ChatGPT (By OpenAI)
- Gemini (By Google)
- Claude (By Anthropic)

## AI Assistants can do

- Answer your Question
- Write an Email
- Generate Program
- Translate language
- Summarize document
- Brainstorm Idea

## AI Assistant vs AI Agent

AI Assistant don't take decide or action on its own

## AI Agent

AI Agent is an AI Assistant with additional abilities:

1. Planning
2. Using Tools
3. Remembering Information
4. Taking actions on its own

## How does AI Assistant Answer Questions

1. You send a question/instruction to an AI Assistant through a progam (client application)
2. It receives your question/instruction
3. Send your question to LLM/AI Model
4. AI Model understand your question/instruction and generate the response
5. The response is send to your progam (client application)

## AI Model

- It is the brain of an AI assistant
- It has already learnt from enormous amount of books, articles, blogs, websites and publically available information
- When you ask a question, the model `predicts` the most appropriate answer based on this knowledge.
- Here we will use AI model, and do not train AI model
- Types of AI Model
  - Language Models
  - Embedding Models

## Where does the AI Model runs

1. `Cloud Models`: The AI model runs on remote servers and your program sends request to them over the internet via API
   - Models from OpenAI, Google, Anthropic, Groq and others
2. `Local Models`: The AI model runs on your own machine.
   - Ollama (An Opensource AI Model)

### Advantage of Local Models

- Free (Opensource)
- Private (Installed on your machine)
- Great for learning

## What is an API

- API (Application Program Interface)
- Cloud Models are accessible via API, your program make call to these APIs with Questions/Instruction, these model understand the question and provide useful response.

## Setting up development environment

1. Python (Programming Language)
2. VS Code (IDEditor)
3. Ollama (AI Model)
4. GIT (Version Control System)

## Install Ollama

Ollama is a platform, that downloads and manage AI models for you.

There are many Opensource AI Model available.

https://ollama.com/library

We will use

- Qwen3:4b

It provides a balance between speed and quality.

```
ollama pull qwen3:4b
```

```
ollama run qwen3:4b
>>> hello
```

```
Ctrl+C
```

![alt text](images/ollamatest.png)

once the AI Model is downloaded, it need not to download again

## Create Project folder and virtual environment

The virtual environment keep libraries separate from other python project libraries on your computer.

1. Create virtual environment

   ```
   python -m venv .venv
   ```

2. Activate virtual environment

   `Windows`

   ```
   .venv/Scripts/activate
   ```

   `Linux`

   ```
   source .venv/bin/activate
   ```

3. Validate

   ```
   (.venv)
   ```

## First AI Program

1. Create environment file

   `.env`

   ```
    ## Ollama - qwen3:4b
    BASE_URL=http//localhost:11434/v1
    API_KEY=ollama
    MODEL=qwen3:4b
   ```

2. Create `hello.py`

   ```
    from openai import OpenAI
    from dotenv import load_dotenv
    import os

    load_dotenv()

    client = OpenAI(
        base_url=os.getenv("BASE_URL"),
        api_key=os.getenv("API_KEY")
    )

    message = [
        {
            "role":"user",
            "content":"What is Artificial Intelligence?"
        }
    ]

    response = client.chat.completions.create(
        os.getenv("MODEL"),
        message)

    print(response.choices[0].message.content)

   ```

3. Install dependencies

   ```
   pip install openai
   pip install dotenv
   ```

4. Run

   ```
   python hello.py
   ```

## Experiment Time

If you rerun the program with same prompt, everytime you get a different answer.

So it is generating the answer on every run.

You application becomes a bridge between the User and the AI Model

AI-Powered/Enabled Application : Application with AI

## Build your first assistant

`Take input from console`

```
question = input("Enter your question")
```

## Conversational Loop

```
while (True) :
    question = input("Enter Question")

    message = [
        {
            "role":"user",
            "content":question
        }
    ]

    if (question.lower() == "quit") :
        print("Good Bye..")
        break

    response = client.chat.completions.create(
        model=os.getenv("MODEL"),
        messages=message
    )

    print(response.choices[0].message.content)
```

![alt text](images/{D1D58965-8BE2-42EA-ADCC-932BE41A3F98}.png)

## AI Model Limitation

AI Model has no memory of your previous messages.

![alt text](images/{73F93207-63A9-4877-A323-EFB710E3E8F3}.png)

## Prompt

A Simple instruction we give to the AI Model

## Conversation loop

Instead of asking one hardcorded question, our assistant repeatedly

- wait for user input
- send it to the AI Model
- Dispaly the response
- Wait for the next question

Leads to an interactive conversation.

## AI Assistant with memory

`Problem Statement`

AI Assistant donot remember the previous conversation

`Solution`

Pass entire conversation to LLM everytime

- Question 1
- Answer 1
- Question 2
- Answer 2
- ..
- ..

If we pass entire `conversation history`, our AI Assistant becomes smarter because it understand the `context`.

## How conversation memory works

Using List

#### index start from 0

```
messages = [
    {
        "role"="user",
        "content"=question
    },
    {
        "role"="assistant",
        "content"=Answer
    }
]
```

So everytime someone speaks we add another message into the list. The will give the Assistant `memory`, and it understand the context.

## Store conversation history using python list

1. Create empty list

   ```
   messages = List[]
   ```

2. `Append` User Message and Assistant Message `Object` into the List
   - Message `Object` has
     - (`role`) who is speaking
       - user
       - assistant
       - system
     - (`content`) What did he say

   So `conversation memory` is just a growing `list of messages`

   ```
   while (True) :
       question = input("Enter Question : ")

       if (question.lower() == "quit") :
           print("Good Bye..")
           break

       messages.append({
               "role":"user",
               "content":question
           })

       response = client.chat.completions.create(
           model=os.getenv("MODEL"),
           messages=messages
       )

       print(response.choices[0].message.content)

       messages.append({
               "role":"assistant",
               "content":response.choices[0].message.content
           })
   ```

   So everytime instead of question we are sending `conversation history (List of messages)` to AI Model

## Debugging coversation history

```
    print("====Conversation History====")

    for message in messages :
        print(message["role"].title())
        print(message["content"])

    print("===========================")
```

## Python comments

```
## Comments

```

## Coversation History Limiting

`Problem Statement`

Sending entire conversation history become expensive and make the application slow.

`Fix`

Use Techniques like

- Conversation Summarization
- Sliding windows
- Vector memory
- Long-term memory
- Knowledge retrieval

`Memory enabled Assistant` : Assistant with memory

## Artificial Intelligence

A technology that enables computers to perform tasks that usually require `human intelligence`.

`Problem Statement`

If you say "What is AI", you will get different answers from

- A School Teacher
- A Comedian
- A Lawywer

Similarly you assistant must behave differently like different expert.

`Solution`

System Prompt

`Example`

The Actor remains the same, only the role changes.
Here

- Action : AI Model
- Role: System Prompt (AI Job Description)

## System Prompt

The AI Model Provide the Intelligence, But the behaviour we decide using `System Prompt`

`System Prompt` set of permanent instruction that sets the behaviour of our assistant/Agent, So it be a specialized AI Assistant, not a generic one.

It comprises of :

- who are you `Set the personality`
- What you do `Set the behaviour`
- What tone should you use `Set the Tone`
- Who is your audience `Set the Audience`
- What should you avoid `Set the restrictions`
- Other criterias

eg

```
You are an experienced python trainer
Who explains programming concept in simple language
Your tone should be professional
Your audience is IT students
Always include one python example
Keep answer under 200 words


```

This gives the AI enough guidance to respond in a consiste

Unlike user messsages, the system prompt is not added everytime in the conversation. It is only added at the begining of the coversation.

Imagine an actor

- Today he plays a doctor
- Tomorrow he plays a king
- Next week he plays a police officer

Did the actor changes?
No

Only the role changed.

An AI Model works in exactly the same way.

- AI Model (Actor)
- System Prompt (Role)

```
{
    "role"="system"
    "content"="You are a friendly AI Assistant"
}

```

The system message always comes first. Because the AI should know its role before it reads the user's questions.

### General purpose AI Asssistant.

- Chatgpt
- Gemini
- Claude

are general purpose AI Asssistant.

## Dynamically choosing Assitant

Choose Assistant

- Teacher
- Python Expert
- Travel Guide
- Motivational Coach
- Interviewer
- Legal Advisor
- Career Counselor
- Chef
- Fitness Trainer

That's why AI Models are so powerful, you can set the behaviour and it will respond accordingly.

- AI Model (Provide Intelligence)
- System Prompt (Set Behaviour)

## Tools

- AI Model (Intelligence/Reasoning)
- Tools (For taking action and performing tasks)

## AI Assistant with Memory and Tools

AI Models are good at

- Explaining
- Writing
- Reasoning
- Summarizing
- Translating
- Brainstorming

But AI Model can not perform actions

- Read your local files
- Check your computer clock
- Open a web browser
- Send emails
- Access to DB
- Perform guaranteed accurate calculations

## AI Tools

Humans uses tools to perform tasks

- What is the current time (Watch)
- What is the forcast (Website)
- Send Email (gmail/yahoo mail server)
- Perfrom complex calculation (Calculator)

Similarly AI Assistants uses tools, to perform some specific task

`Tool`

A piece of software that performs a specific task.

`AI model does not become smart at math because we give it a calculator`

`instead`

`AI becomes smart enough to know : "I should use the calculator"`

So AI Agent should be smart enough to understand what can not be done by AI Model and should specific tool instead.

AI `Reasons` and `Decide` what tool should be used, and the tool Perform the actual task`

`So the answer are not generated using AI Models instead computed using tools, that are much more reliable`

## Building AI Assistant with Tools

Many `AI Frameworks` support `automatic tool calling (ATC)`

- In this the AI model itself decide which tool to use and when, rather than our program decides which tool to call and when.

## Building our first tool: Current Date and Time

`Tool` A python function that performs only one specific task.

## Connecting AI Assistant to Tools

```
If user ask "What is the current time"

This question should go to tool
```

```
If user ask "What is AI"

This question should go to AI Model
```

## Refactoring

Improving the structure of your code without changing what it functionality.

## AI Assistat to read files.

Your AI Model does not read a file, its your AI Assistant who reads the file and send the content to AI Model

## Reading your first text file with Python

`Professional software should fail gracefully`, users appreciate clear error messages much more than long technical error traces.

So we use `Exception Handling`

```
def read_text_file(filename):
    try:
        with open(filename,"r") as file:
            content = file.read()
            return content
    except FileNotFoundError:
        return "Error: File not found..."
```

Here the read_text_file Tool just read the file, it does not do

- summarization
- answer question

Just one specific task, this is how tools should be designed. One specific task per tool.

Later AI Assistant will combine many simple tools to solve complex problems.

## Sending file content to AI Model

```
question = "Summarize the content. "
```

```
    messages.append({
            "role":"user",
            "content":question + read_text_file("data/Notes.txt")
        })

```

In AI Engineering, the process is same for all kind of files.

1. Read the data (`Retrieval`)
2. Prepare a good prompt
3. Send data + prompt to AI Model (`Agumented`)
4. Let the AI reason/think over the data (`Generation`)
5. Presend the result

`Problem Statement`

Our solution works for small files only, not suitable for very large files or group of files.

`Solution`

RAG (Retrieval Argumented Generation)

## Ask Questions about a document

```
messages = [{
    "role": "system",
    "content": (
        "You are an QA Agent"
        "You are given a document content"
        "You Answers the user's question using only the information present in the document given to you"
        "Your tone should be professional. "
        "Keep answers under 200 words. "
        "Your audience is students. "
        "If the answer is not available, say: "
        "'Sorry, I can not answer the question.'"

    )
}]
```

```
    messages.append({
            "role":"user",
            "content":question + read_text_file("data/Project.txt")
        })
```

Don't `hallucinate`

To ensure reliability of AI Assistant, ensure you explicitly mention if it does not find the answer, don't guess. Just say "I dont know.."

In AI Engineering, the process is same for all kind of files.

1. Read the data
2. Prepare a good prompt
3. Send data + prompt to AI Model
4. Let the AI reason/think over the data
5. Presend the result

So createing the prompt is the main thing.

`Problem Statement`

If you have a document size 750 pages to answer from. We can not send the entire file to AI Model

- `AI Model has Context Limit` (Token Limit they can accept), so the Prompt become too large to fit in a single request.
- `It become expensive and slowdown` the process.
- In case we have multiple files and we want to answer from those files and don't know which file, page and paragraph has the answer.

`Solution`

RAG (Retrieval Argumented Generation)

It is one of the most foundational techniques in modern AI application.

Here instead of sending the entire document to the AI Model, we retrieve only the most ` relevant pieces of information` before asking the model to answer.

## Why do we need RAG

1. `Prompting` : User sends Question
2. `Retrieval` : Get the relavant pieces of Information
3. `Argumented` : Send Questtion + Relevant pieces of information to AI Model
4. `Generation` : AI Model Generate the response and send back.

Terms like

For `Retrieval`, we use

- `Embeddings`
- `Vector Database`
- `Similarity Search`

## Building a Knowledge base with multiple documents.

1. User ask `Question`
2. RAG
   - Search the Knoledge base
   - Find only the relevant information
3. Send the `Question` + `Relevant Information` to AI Model
4. AI Model Generate the response and send to User

- Knowledge Base (Set of documents)

## Search Types

- Key Based Search - Exact word matching
- Context/Meaning Based Search - Meaning matching
- Hybrid Search

## Simple Key-based Retriever

So the keywords in the File content and if it is there the documents content is returned.

`Limitation`

- Keyword `Programming Languate` =/= `Coding Lanaguage`
- If there are 2 documents having `database` keyword, which one to return, just return the first match, without considering which is better

In professional systems, we use better approaches for `Retrieval`. Instead of searching for exact words, rather searching for Context/Meaning.

## Embedding and Sementic Search

- `What is Programming Language`
- `Explain Coding lanaguage`

These both has same context/meaning.

Professional RAG systems compare `meaning`, not `keywords` exact words

`Problem Statement`
How they compare meaning

`Solution`

Using `Embeddings` : Converting the text into numerical representation

`Embedding` = `Text` ---> `Numeric representations`

These `embeddings` allow the computer to measure `sementic similarity` rather than keyword matching.

So `Embedding` solves the limitation of `Keyword Search`

`Human searches for meaning, rather then words`

Example

Delhi => (Latitude , Longitude )
Gurgaon => (Latitude , Longitude )

So if two cities having close (Latitude , Longitude ) , we say they are geographically close.
Embedding works in similar way, except instead of placing cities on the map, they place meaning in a `mathematical space`
So words and sentences with similar meaning end up together.
So words and sentences with different meaning end up far apart.

## How Embedding represent Meaning

`Embedding` : Process of Converting Text into Numeric Representation (A list of floating Point numbers) called `Vector`

Simple list of numbers that represent some text.

Python
[0.21,-0.55,0.87,....]

Programming Language
[0.23,-0.53,0.85,....]

If two words are `sementically similar`, their vectors are also similar

`Vector`

The List of `floating point numbers`

[0.21,-0.55,0.87,....]

[0.23,-0.53,0.85,....]

`Computers are good at comparing numbers, thats why we convert text into vectors`

Once text becomes number, we can answer questions like

- What all documents have these two sentences?
- What document is closest to the user's question

`Keyword Search` - Compare Keywords

`Embedding Search` - Compare vectors

### Do we create embeddings ourselves

No, like we dont build `language model`, we use pre-trained `embedding model`

Popular Buzz words

- 768-dimensional embeddings
- 1536-dimensional embedding

These dimensions describe how many values are in the vector.

[1,2,3] has 3 dimensions

## Generate Embedding with Ollama

Install `Embedding Model`

```
ollama install nomic-embed-text:latest

ollama list
```

`Chat Model`

- Generate Text
- Used for chatting
- GPT Models

`Embedding Model`

- Generate Vectors
- Used for searching
- Text-Embedding Models

```
client = OpenAI(
    base_url=os.getenv("BASE_URL"),
    api_key=os.getenv("API_KEY")
)
```

```
embedding = client.embeddings.create(
    model="nomic-embed-text",
    input="pyton is programming language"
)

print(embedding.data[0].embedding)
```

```
response = client.chat.completions.create(
    model=os.getenv("MODEL"),
    messages=messages
)

print(response.choices[0].message.content)
```

## From embedding to sementic search

1. Text Embedding of `Question` (1536 dimentional vector)
2. Text Embedding of each document
   - `Document 1` -> Text Embedding (1536 dimentional vector)
   - `Document 2` -> Text Embedding (1536 dimentional vector)
   - `Document 3` -> Text Embedding (1536 dimentional vector)

Compare these Vectors for `Sementic Similarity`, closest match

So `Keyword matching is replaced with Vector Comparison`

## Finding the most similar document with Cosine Similarity.

Question : What is a Python Programming Language

Suppose

```
Document    Similarity Score

Python.txt      0.91
Mysql.txt       0.45
Weather.txt     0.44
```

Higher the Similarity Score, More similar the meaning.

The technique most commonlty used is called `Cosine Similarity`, which compare two vectors

Cosine Similarity Score : 0-1

```
1.00     Almost identical
0.90     Very Similar
0.70     Fairly Similar
0.30     Weakly related
0.00     Unrelated
```

You dont need to calculate this score yourself, python libraries `pip install Numpy` can do it for you.

## Cosine Similarity Implemantation using Numpy

```
def cosine_similarity(vector1, vector2):
    v1 = np.array(vector1)
    v2 = np.array(vector2)
    return np.dot(v1, v2) / (np.linalg.norm(v1) * np.linalg.norm(v2))
```

## Sementic Search

```
Question
|
Embedding
|
Compare with
|
Document 1 (Cosine Similarity Score : 0.40)
|
Document 2 (Cosine Similarity Score : 0.80)
|
Document 3 (Cosine Similarity Score : 0.70)
|
V
Choose document 2
```

So there are diff. search types

- Keyword search
- Sementic search (comparing the embeddings)

In production environement, you might use Vector Databases like:

- FAISS
- ChromaDB
- Qdrant

But the basic idea is same

1. Convert text into embeddings using `Embedding Model`
2. Compare the embeddings
3. Retrieve the closest matches
4. Give those matches to the `Language Model`

### Key Points

- `Cosine Similarity` give us a practical way to measure closeness.
- High `Cosine Similarity Score` indicate more relevent information.

## RAG Application

Our application follow these steps

1. Load the Documents `once` and generate embedding using `Embedding Model` using Python
2. User enter Question
3. Generate Question's embedding using `Embedding Model` using Python
4. Compare with all the document's embeddings using `Cosine Similarity` using Python
5. Find the document with Highest `Cosine Similarity Score` using Python
6. Send `Question` + that `Document` content to `Language Model`
7. Generate final answer and display it.

## AI Agent

Till now our program is deciding to call a specific tool, based on the question

In, AI Agent, Language Model decide which tool to call.

![alt text](images/{8080BAE4-9998-443F-B826-3468DF8BF39D}.png)

AI Assistant - Responds
AI Agent - Decides

Our Application has

1. Memory
2. Personality
3. Tools
4. File Reading
5. RAG
6. Decision Making (Still Missing) - AI Should decide which tool to call

`Problem Statement`

How many days are left until New year's Day

`Steps`

1. Find today's date
2. Find the date of New year's Day
3. Calculate the difference
4. Explain the result

Here the Agent decides and combine tools required to reach the goal.

Similar to how human solve a problem.

The AI Agent will receive

- The user request
- A list of tools along with instructions what each tool does.

It's first job is not to answer the user

It's first job is

- `Planning` : Decide which tools should it use
- `Execution` : Execute the tools to generat the result.

So the separation between `planning` and `Execution` is one of the defining chracteristics of modern AI agents

Many Frameworks make this process look magical.But the logic is straightforward.

```
User Goal (Question)
|
V
Plan (Planner decides which tool to choose)
|
v
Choose Tools
|
V
Execute Tools
|
V
Generate Result
|
V
Respond
```

A toolbox does not become useful because you have many tools, it becomes useful when you know

- `which tool to use`
- `when to use and`
- `in which order.`

The same is true for AI Agent.

Agentic AI is fundamentally about

- `Decision Making,`
- `Workflow Orchestration`

  Not just adding more tools and powerful language models

## Building the Agent Brain

The Agent brain job is `planning` - how to solve the problem

The brain does not solve the problem, it decide how to solver the problem

- Should I call a specific Tool,MCP
- Should I send the question to language model

```
User Question
|
V
Planner (How to solve the problem, shoule I call specific tool/MCP, should i send it to LLM, In which Order I should perform actions)
|
V
Tool Name
|
V
Python
|
V
Execute Tool
|
V
Result
|
V
Language Model
|
V
Final Result
```

`Is the Planner also an LLM `

- Yes

So you go to LLM twice, first for asking the tool and second to give answer in a natural way.

`Planner` Decides what to do

`Assistant` Explain the result to the user (Behaviour)

- Different Prompts
- Different Responsibilities
- Same Underlying Model

This demonstrate one of the strengths of LLM, the LLM Model can perform multiple roles depending on the instructions it receives.

## Building our first Planning Agent

In Agentic AI, the Planner is the component responsible for deciding how to solve a problem, what steps to follow, and which tools to use.

Main job of the Planner

The Planner has two important responsibilities:

1. `Planning`: Break a complex problem into smaller, manageable steps `Task Decomposition`
2. `Tool selection`: Decide which tools, APIs, or other agents are needed to complete each step.

Think of the Planner as the brain of an AI agent. It does not necessarily execute every task itself. Instead, it creates a strategy and guides other components to complete the task.

AIPlanner.py

```
from openai import OpenAI
from dotenv import load_dotenv
import os

load_dotenv()

def choose_tool(question):
    planner_prompt= f"""
                    You are an AI planner.
                    You return the tool to use based on the question.

                    Available tools
                    1. get_current_time
                        Use when the user ask for the current date and time
                    2. generate_password
                        Use when the user ask for a secure password
                    If no tool is requried, return
                    none

                    Return only the tool name
                    """

    client = OpenAI(
        base_url=os.getenv("BASE_URL"),
        api_key=os.getenv("API_KEY")
    )


    messages = [{
        "role": "system",
        "content": (
            planner_prompt
        )
    }]


    messages.append({
            "role":"user",
            "content":question
        })

    response = client.chat.completions.create(
        model=os.getenv("MODEL"),
        messages=messages
    )

    return response.choices[0].message.content

```

```
print(choose_tool("What is the current time"))
```

Output

```
get_current_time
```

## Let the AI Speak naturally

```
get_current_time
```

returns

```
02-08-2026 H:4522 AM
```

We could simply print it, But that feels robotic

Instead, lets ask the assistant to presend it naturally

![alt text](images/{DC9B9820-1701-46B3-B99E-E92BD224B91E}.png)

![alt text](images/{7FB0E36C-394B-4A70-97E5-226B720DF66D}.png)

`Problem Statement`

Our planner can choose one tool, what if the user question require multiple tools

- Generate a password and save it in the file
- Read the meeting notes, summarize them, and send an email

Those tasks require several actions performed in sequence.

`Solution`

Multi-Step Planner

`Multi-Step Planner` plans a sequence of actions and behave much like humans solving complex problems.

## MCP (Model Context Protocol)

To use `external Tools` in our AI Agent.

- Email Tool
- Calander Tool
- Stock Market Tool
- Weather Tool

Think of MCP (Model Context Protocol) a Protocol - `Open standard` that allow our AI Agent or Applicatoin to discover and use `external Tools` in a consistent way.

External capabilities like

- Tools
- APIs
- Databases
- Documents
- Knowledge Sources

So our Agent will be able to

1. Discover available tools
2. Understand what each tool does
3. Choose an appropriate tool
4. Call the tool through a standard interface
5. receive the results.

So tool will be on the remote MCP Server, not in the Agent application.

## Understand MCP Architecture

Every MCP System has 3 major components

```
AI Agent
|
V
MCP Client
|
V
MCP Server
|
V
Tools
```

1. AI Agent Components are:
   - Assistant
   - Planner
   - Memory
   - RAG
   - Tools Selection

   The Agent never call external tools directly, instead it talks to `MCP Client`.

2. MCP Client : Its Job is to call MCP Server to
   1. Discover available tools and ask for tool description
   2. Send Tool request and Receive tool result

   The Client knows how to speak to the MCP Server

   The Agent does not need to worry about protocol details.

   `AI Agent <--> MCP Client <--> MCP Server <--> Tools`

3. MCP Server : It owns the tools
   - Weather tool
   - Calander Tool

   The first question MCP Client asks to MCP Server is

   `What tools do you have?`

   The MCP Server replies with a `List`

   Whenever MCP Client calls a Tool, MCP Server runs it and return the result.

So our Agent does not know how a specific tool works, just know there is a specific tool (calculator) available.

Just like using Web API

```
User Question
|
V
AI Agent
|
V
Planner
|
V
Weather Tool is needed
|
V
MCP Client
|
V
Call Weather Tool
|
V
MCP Server
|
V
Execute Weather Tools
|
V
Result
|
V
MCP Client
|
V
AI Agent
|
V
User
```

So user does not see MCP Client/Server, It only sees final result.

You can have multiple MCP Servers in an Agent

- Weather MCP Server
- Calander MCP Server
- Stock Market MCP Server

Each Server is specialized in a specific domain.

Suppose someone develops a new MCP Server for

- Stock Market
- Youtube
- Gmail
- AWS
- Docker

`Do you need to rewrite your Agent?`

No

You simply connect another MCP Server to your AI Agent and instantly gains the new abilities.

This is one of the biggest advantages of standardization.

## Build your first MCP Server

1. Install MCP SDK

   ```
   pip install fastmcp

   pip show fastmcp
   ```

2. Create `data_mcp_server/server.py`

   ```
    from datetime import datetime
    from fastmcp import FastMCP

    mcpserver = FastMCP("Time Server")


    @mcpserver.tool()
    def current_time():
        """Return the current date and time."""
        return datetime.now().strftime("%d-%m-%Y %H:%M:%S %p")

    if __name__ == "__main__":
        mcpserver.run()
   ```

   - @mcpserver.tool() decorator tell the server: This function is available in MCP clients
   - """Return the current date and time.""" is Tool description (DocString)
   - The MCP Framework can expose it to AI Agents.
   - The Planner can read it and decide: This tools look like correct tool.
   - So clear description is very important

![alt text](images/{CB97DAC0-EE7E-444C-9BFF-102F01D71A34}.png)

## Build MCP Client

The responsibilities of MCP Client are

- Connect to the MCP Server
- Discover available tools
- Execute a selected tool
- Return the results to the AI Agent

1. Create `data_mcp_client/client.py`

```
from fastmcp import Client

async def connect():
    """
    Connect to the MCP Server.
    """
    client = Client("../date_mcp_server/server.py")
    await client.__aenter__()
    print("Connected to the MCP Server..")
    return client

async def disconnect(client):
    """
    Close the the MCP Server.
    """
    await client.__exist__(None,None,None)


async def discover_tools(client):
    """
    Retrieve all tools from the server.
    """
    tools = await client.list_tools()
    return tools

async def execute_tool(client,tool_name,arguments=None):
    """
    Execute a tool.
    """
    if arguments is None:
        arguments = {}
    result = await client.call_tool(
        tool_name,arguments
    )
    return result
```

- you have to expicitly call disconnect(), to stop server processes and release all resources.

`Should the Planner know which server owns which tool`

No

`Should the client connect to every server separately`

Yes

`With Planner there is no hardcoded for chosing and executing the tool`

So AI Agent

- Discover external capabilities
- Understand tool descriptions
- Select the correct tool
- Execute the tool
- Present the result naturally

`Our AI Agent Perform 6 Steps`

1. Connect to MCP Server via MCP Client
2. Discover List of available tools
3. Call Planner (User Input , List of available tools)
   - return the best fit tool based on question using `LLM`
4. Agent then Execute the best fit tool
5. Pass Result to `LLM` to generate natural response.

`AI Agent`
![alt text](images/{B0078579-1506-4FDA-8B34-D973A7855236}.png)

`Planner`
![alt text](images/planner.png)

`Problem Statement`

What if we need to use two tools to respond to our questions.

`Fix`

Autonomous AI Agent , that use multiple tools (via `multi-stage planner`)

## Build Multiple MCP Servers

![alt text](images/{C80100F6-CA75-4A9F-9E33-D897232B911E}.png)

![alt text](images/{B89DBB87-FD03-44AE-B892-728681730EAC}.png)

![alt text](images/{E9FD41EA-847D-45D2-BB24-505CDEB1F893}.png)

![alt text](images/{5FC23916-9614-4786-8128-7963B48A4F6B}.png)

![alt text](images/{FD0B2D76-45BA-4F66-BDC1-519E71EE9854}.png)

![alt text](images/{C897D6A7-1B1E-484E-8C51-B6112A890CFF}.png)

## Build fexible MCP Client

![alt text](images/{2C8801B8-86DE-4980-9A40-48E2D86A0909}.png)

![alt text](images/multipleMCP.png)

![alt text](images/{040FF96E-BF58-4A26-911B-4B2011109C65}.png)

![alt text](images/{4D0BE894-1CEC-4ED3-BB33-7F1753823AA3}.png)

![alt text](images/{5815C02B-2B72-49F7-BBEF-AA0A962C9335}.png)

## Building Flexible MCP Client Registry with Multiple Servers

![alt text](images/{05AA0B30-4D43-4636-BA48-770F60915F53}.png)

![alt text](images/{2AE1042E-3954-443C-A5E7-7E45F3C6E988}.png)

![alt text](images/{6E58E368-B257-48BE-B5C9-464DD0BF7050}.png)

![alt text](images/{0A7444C0-5841-41D5-870B-1E683727BA7E}.png)

![alt text](images/{5A074063-180C-44DC-8970-B9EF5468114A}.png)

![alt text](images/{EF3FCDEE-CBD1-402E-A54C-0F77B4377E94}.png)

![alt text](images/{A7C65D80-EB6C-4FB0-A82D-24E5A33BAA69}.png)

![alt text](images/{76672EF6-655A-480B-8835-1312F5CDD022}.png)

![alt text](images/{EE010B71-9070-4DF7-AA04-02DFD95DF3AE}.png)

![alt text](images/{F8AF707D-BFF9-4AB1-A8BD-42EF1C31BDDA}.png)

## Demo

![alt text](images/{5D39D89C-0559-46DF-A96B-2233A5B9637F}.png)

![alt text](images/{245D24B1-B41C-4838-8C1F-FE6EE85FB76C}.png)

![alt text](images/{7F2AE726-460A-4268-88EF-9C7E3D53509B}.png)

![alt text](images/{2F926F0E-55EC-4C06-BF96-47DF51700358}.png)

![alt text](images/{88367E8C-9F1F-408B-87BB-9FF819AAEC01}.png)

## Upgrade Planner to handle tool arguments

![alt text](images/{F0250DC6-1C7B-4044-945A-BC75A2D741DB}.png)

![alt text](images/{5DDEC286-56CC-4093-ABBC-223A0FA6E825}.png)

![alt text](images/{A5BC2B25-803F-480C-B98F-63D1AD95F36B}.png)

So here the planner will return a dictionary, instead of just a tool name.

![alt text](images/{F7253EC6-672C-4A15-9591-2ACB4E99F713}.png)

![alt text](images/{76A86CEC-F948-42FD-A687-FCC1C827DAD7}.png)

![alt text](images/{D58ED969-942F-42E0-B87A-B913C3124410}.png)

![alt text](images/{EBA5CF4F-99F4-410D-B049-25A39D394552}.png)

![alt text](images/{DA3B4028-F742-42B3-BF20-C5DE9FE9112A}.png)

![alt text](images/{6D339D02-7B75-4AAA-B563-EF9AA3DE5CF3}.png)

![alt text](images/{4C8EF051-C686-4DAB-8E9A-16D7A5708A82}.png)

![alt text](images/{2F4422CF-468B-48E5-A764-470514D0B8CA}.png)

![alt text](images/{1A1ADEE0-CA68-4838-91F5-B409FD69AD9D}.png)

![alt text](images/{802848FF-37D4-44C7-8409-12A000F7D643}.png)

### `{{expression}} vs {expression} Inside the prompt`

Inside the prompt the single curly braces expression will be evaluated, the first will be used as a JSON.

`{expression}` => Value

`{{expression}}` => {Value}

so

`{{key:value,key:value}}` => {key:value,key:value}

![alt text](images/{81DC2A21-BC79-4FFE-8569-9B5F982E040C}.png)

So intead of retunig "Finnish" we are returning JSON.

![alt text](images/{1EBA5168-01D3-4F40-B14D-182C3F1348B3}.png)

![alt text](images/keywordSearch.png{7AF1C83D-7EC5-4D67-A074-F7B86351E1A9}.png)

![alt text](images/{2192C8BB-25D6-4CF8-99B7-21A593A9B905}.png)

![alt text](images/{AEEA704D-1C4C-4A12-88E7-DAFB466EA5AF}.png)

Earlier it was taking tools instead of tool_Registry
`action` : returned by the planner - tool - argument

![alt text](images/{7DA45643-8E74-4868-BA77-BEFEE4947FAF}.png)

![alt text](images/{1AA871D3-7EC4-4769-BA04-FFB8B1E6A5C2}.png)

### Upgrade Agent Loop

![alt text](images/{09D236A4-82FA-4F42-A5DD-6C5077CD7BCC}.png)

![alt text](images/{C8474A32-81D8-425B-B360-F2B1DABDD1A6}.png)

![alt text](images/{90F6B1D8-F0F3-4CAD-AFF7-D40039E40E03}.png)

![alt text](images/{AD8A6BB8-4A7A-4AF3-B9DF-8F87F3B00903}.png)

### Upgrade Runner

![alt text](images/{4425AFF8-3713-49F7-ADE8-D331EF4DA5AD}.png)

![alt text](images/{269A91AE-220A-4CAD-B895-B9C1BD03C9A7}.png)

![alt text](images/{CB0CA67D-411F-4142-9405-AFF3F6C5883F}.png)

![alt text](images/{9CBB1184-A1F7-4273-A4D6-8F0181C8021A}.png)

![alt text](images/{D125C80E-0D7E-4DC8-8F8D-D3532B248BB9}.png)

![alt text](images/{9C75B1ED-EE79-4FA3-A536-993B4D131CEA}.png)

![alt text](images/{5286865A-443C-4F74-88BA-9E865DDE202B}.png)

![alt text](images/{920FF23B-1EEE-43BD-858F-ABB1459687B2}.png)

![alt text](images/{0DF5FDAE-EF20-4E34-9332-1B88963EA80B}.png)

## Understand MCP Tools and Resources

There are 3 MCP primitives (tool, resource, prompt)

1. Tool

![alt text](images/{1A3794CE-DCBC-4368-B0F0-A217291CE6D6}.png)

2. Resource

![alt text](images/{13D8E126-E237-4180-859E-ED88ABC2B981}.png)

![alt text](images/{102806F8-41BA-4908-BFAD-7D5089AB3623}.png)

![alt text](images/{1E33523C-0FCB-4B5E-A501-DAF74FBA0F5E}.png)

![alt text](images/{6D8E1788-2CB5-467C-B759-C812A69301A3}.png)

![alt text](images/{60B812AC-F05C-4658-8D7A-857F032CD622}.png)

![alt text](images/{98648E2C-FFD6-42F8-95BF-F68549522457}.png)

Ensure the close the server, so use finally{}

![alt text](images/{B28F427B-A31D-405C-982E-4C7084336A7E}.png)

3. Prompt

![alt text](images/{2D0EFCB9-8562-4D74-8FCB-09F2D45F8540}.png)

![alt text](images/{D3F8CDA3-0323-4E8D-9329-1B4022235D6D}.png)

![alt text](images/{A715CFBD-9315-491C-A4F4-97D981C5D2CA}.png)

![alt text](images/{7373D313-C1C2-4F86-8774-55666102A62D}.png)

## Understanding Remote MCP and HTTP Transport

![alt text](images/{D89302F2-87A9-4559-B5D5-FF24B4E75EB6}.png)

![alt text](images/{AA8444B8-510F-4CDE-94D8-F9D8B0816E98}.png)

![alt text](images/{DE881795-8A1B-4E69-A2DC-9CFB266909E1}.png)

![alt text](images/{8929A2F8-19D6-4731-A5F5-8DFAC593291C}.png)

![alt text](images/{2600E764-2D07-4165-8217-84450F926C93}.png)

![alt text](images/{8A258799-B1E0-480B-BA90-552EE1474968}.png)

![alt text](images/{8548E945-82B2-469E-B045-05014365DD92}.png)

![alt text](images/{D7C2B694-C8A9-44CD-9CC9-3AEA653228FC}.png)

![alt text](images/{A558B037-E91E-443E-86C1-BB74990AE3AA}.png)

## Demo

![alt text](images/{A14EA31C-2D07-40CE-97F0-7E1FB6C6B279}.png)

![alt text](images/{A44BC1DC-EDA4-4C6A-9BEA-51FDD040239C}.png)

![alt text](images/{15CF94CF-BA9E-4D04-9AA7-D5013E5BC333}.png)

## Authentication and Authorization in Remote MCP

![alt text](images/{32764091-1C57-49CD-A002-83FA91A3CC32}-1.png)

![alt text](images/{AF32ED16-2412-432D-9D31-CA6546F5A540}.png)

![alt text](images/{5AD67F3D-4196-4E40-A690-07A7ACAD7819}.png)

![alt text](images/{9AD1B14D-0988-47BF-A448-DC97AAEACCC2}.png)

![alt text](images/{9958222A-6B6E-4FE8-9CE3-17BF478501C4}.png)

![alt text](images/{CE2DA418-C87C-4526-82A0-89CC18EA396E}.png)

## Autonomous AI Agent

![alt text](images/{32764091-1C57-49CD-A002-83FA91A3CC32}.png)

It has `multi-stage planner`, where each stage might use a different tool`)

After everystep you look at the result, and decide `what to do next`.

It should continue until the task is completed.

This process of repeatedly thinking, acting, and observing is exactly how humans solve complex problems

AI Agent should work the same way.

```
Think
|
V
Act
|
V
Observe
|
V
Think
|
V
Act
|
V
Observe
```

Called `Agent Loop`

## What is Agent Loop

An Agent loop is a repetitive cycle in which the AI Agent:

1. Think about the current situation
2. Decide the next action
3. Execute the action
4. Observe the result
5. Decides whether another action is required.

The loop continue until the goal has been achieved.

## Design the Agent Loop

![alt text](images/{4151BFA7-B72C-43FD-9F97-87376414DDF7}.png)

### Agent State :

When the planner run for the second time, how does it will know what happened in the first interation, so it needs some form of working memory, That is called Agent State.

`Question` : provide me a secure password and current time

The state initially contains:

- Current Step: 0
- Actions: None
- Observations: None
- Finished: False

### Action Loop

Loop over

- Think (Planner)
- Act Action (AI Agent)
- Observer Result (AI Agent)

The Planner receives 3 pieces of information

- Question
- Agent State
  - Previous Action
  - Previous Observation

Based on that the planner decides the next action.

The AI Agent stores the result in

`Agent Loop`

`Iteration 1: Agent State` include

- Current Step: 1
- Actions: generate_password
- Observations: K#8Lm@4Pq
- Finished: False

`Iteration 2: Agent State` include

- Current Step: 2
- Actions: current_time
- Observations: 09/10/2026 10:12:21 PM
- Finished: False

`Interation 3: Action State` include

- Current Step: 3
- Actions: current_time
- Observations: 09/10/2026 10:12:21 PM
- Finished: `True`

Finally all the observations are combined into a user friendly response.

## Building the Agent State

`Agent State`

A dictionary containing everything the agent needs while solving the current task.

![alt text](images/{33B0E147-400A-41A8-B315-0B0DEA7FE560}.png)

Max_steps : `To avoid infinite Loops`

Once the request has been completed, the state is discarded and a new state is created for the next request.

## Implementing the Agent State.

`state.py`

```
def create_state(user_request,max_steps=5):
    return{
        "user_request": user_request,
        "current_step":0,
        "max_steps":max_steps,
        "actions":[],
        "observations":[],
        "finished":False,
        "final_answer":""
    }
```

This function is called, on every new request arrive.

```
def add_action(state,action):
    state["action"].append(action)
```

After selecting the generate password tool by the planner

```
"actions": [
    "generate_password"
]
```

Keeping track of the previous actions, helps the planner avoid repeating the same task.

```
def add_observation(state,action,observation):
    state["observations"].append(
        {
            "steps":state["current_step"],
            "action":action,
            "observation":observation
        }
    )
```

This makes debugging much easier and provide valuable context for the planner

At the end of every iteration, we increate the step count

```
def next_step(state):
    state["current_step"] =  state["current_step"] + 1
```

This function determines whether the Agent Loop should continue.

Execution stops when either

- The Task has been completed
- The maximum number of iterations has been reached. This prevents the accidental infinite loops

```
def can_continue(state):
    return(
        not state["finished"]
        and
        state["current_steps"] < state["max_steps"]
    )
```

The function mark the task as completed

```
def finish(state,answer):
    state["finished"]=True
    state["final_answer"]=answer
```

## Building the Agent Loop

![alt text](images/{9C8644AC-D383-43AF-8B48-D9A9691DB4C0}.png)

To avoid program running forever, our state contains max_steps

- It checks: can_continue(state) : The loop stops either the agent finishes the task or the max. number of steps has reached. `Very Important Safety mechanism for autonomous system.`

- We did not use LangGraph, CrewAI, AutoGen. We built the fundamental agent architecture using Python, LLM, MCP Architecture.

## From Next Action to Task Decomposition

`Task Decomposition`

So we Bread Big Task(`Goal`) into multiple sub-tasks

This is one of very important idea in `Agent Engineering`

## Building LLM bases Task Decomposer

![alt text](images/{4CD8E06C-485E-4F3A-B331-5BEE7B73EF35}.png)

![alt text](images/{A8027F57-BACA-4844-A114-50BC341F0D90}.png)

```

def decompose_task(user_request):
    prompt = f"""
            You are an AI task Planner.
            Break the user's request into smaller tasks
            Instructions:
            1. Create only the task necessary to complete the request.
            2. Put the task in the correct execution order.
            3. Each task should be a short action.
            4. Return only task per line
            5. Do not number the tasks
            6. Do not explain anything.
            User Request
            {user_request}
            """

    client = OpenAI(
        base_url=os.getenv("BASE_URL"),
        api_key=os.getenv("API_KEY")
    )

    response = client.chat.completions.create(
        model=os.getenv("MODEL"),
        messages=[
            {
                "role":"system",
                "content":"You are an AI Task Planner."
            },
            {
                "role":"user",
                "content":prompt
            }
        ]
    )

    result = response.choices[0].message.content.strip()

    tasks = []

    for line in result.splitlines():
        line = line.strip()
        if line:
            tasks.append(line)

    return tasks

```

## Adding tasks to Agent State

```

def add_task(state,task):
    state["task"].append({
        "task":task,
        "state":"pending"
    })

def complete_task(state,task):
    for item in state["tasks"]:
        if item["task"] == task:
            item["task"] = "completed"
        return

def add_result(state,result):
    state["results"].append(result)

def get_pending_tasks(state):

    return [
    item["task"]
    for item in state["tasks"]
    if item["status"] == "pending"
    ]

def all_tasks_completed(state):
    return all(
        item["status"] == "completed"
        for item in state["tasks"]
    )
```

![alt text](images/{7E71BECC-B726-4B71-8ABD-F2E7AC32F6B7}.png)

## State-Aware-Planner (Advanced Agent Planning)

![alt text](images/{BC9F7184-0505-483F-AE13-EFB789F9066D}.png)

![alt text](images/{FBB96108-73CD-4FDC-9AB3-FA9FD820781F}.png)

![alt text](images/{CB46EE45-6889-4F2C-A118-7DFCB017719B}.png)

So our Agent :

![alt text](images/{AF283D4F-1B90-4451-8004-64FB35FE69C8}.png)

```
User Request
|
V
Task Decomposition
|
V
Create Task
|
V
Agent State
|
V
|
V
Agent Loop
|
V
(P E O U) - Called `Dynamic Planning`
Plan (P - > Planner)
|
V
Execute (E -> Tools)
|
V
Observe (O -> State - Observations)
|
V
Update (U -> Stop (Finished=True or Max-Steps reached) or Iterate again)
|
V
Plan Again
|
V
.
.
.
|
V
Finish
```

This is called `Dynamic Planning`

![alt text](images/{3869B625-60A1-42EB-9B1B-4B944D35A00B}.png)

- An autonomous agent must have a stopping condition, to avoid it running forever (Finished = True or Max_iterations reached.).
- The decomposer breaks a large request into smaller tasks
- The tasks are stored inside the agent state
- The planner look at the tasks , state/actions and state/observatios, and decides what should happen next.
- The planner does not execute the tool, just decide what to do next (So Planner just return the next action)
- The executor execute the action via MCP/RAG/LLM.
- After executor, the result updated in state/observations.
- The state is updated
  - action
  - observations
  - results
  - current step
- The planner run again with (tasks,state) return next action(call Tool/Exit Loop on Finished)

This gives us a `goal-oriented autonomous loop`

- We also introduced `Dynamic replanning`: if something goes wrong, the agent should not blindly continue with the outdated plan, it should reconsider the situation and decide what to do next.

## Memory

![alt text](images/{18FA89E0-E0AF-4A0F-BFAD-3EFF2158E897}.png)

![alt text](images/{788E1FA6-3B8E-4778-9203-AFFE8F08BF12}.png)

![alt text](images/{23EE769A-E20F-44D4-B7C8-85C92ADF74E0}.png)

### Memory Engineering

![alt text](images/{34C89C32-6B7D-4373-8626-E8F7488FDD91}.png)

![alt text](images/{5780D512-0863-4FCE-9917-EE220976C076}.png)

![alt text](images/{A969D0DD-4AB2-4F5B-AE99-B5494ABA201E}.png)

![alt text](images/{4463232E-3937-4C4D-A09D-E5DFA6A8644B}.png)

![alt text](images/{481BF431-F8DC-47AA-8207-DDD5BDA90536}.png)

![alt text](images/{AC3E9741-885A-4450-BC83-FDAA3F5C160B}.png)

### Building Persistent Memory with SQLite

![alt text](images/{80A6B264-D4DF-42E7-917D-0C045E8E2082}.png)

![alt text](images/{29A4F051-6D81-4B9F-BA20-410E64B4C823}.png)

![alt text](images/{C29AE2DA-D886-4402-98F6-7AEEE0A6FEBB}.png)

![alt text](images/{878CBFB8-6857-432A-A734-7A1228CBEA4F}.png)

![alt text](images/{6AB8FFC0-ED8B-4573-8B75-3BB2EB4295EE}.png)

![alt text](images/{D115D34F-40CB-47AD-B473-D29117A6C61C}.png)

![alt text](images/{E4538664-A2E6-4570-ACE6-76868FAF07F4}.png)

![alt text](images/{3BBDC39C-3583-404B-B817-730D0616A2B4}.png)

![alt text](images/{EE919B7E-7CAF-4DA0-BE04-797B2BD337B8}.png)

## Advanced RAG

### Chunking

- Breaking documents into small pieces or chunks, create the embedding for each, and retrieve only the relevent chunks

- We should not split the documents randomly.

  ![alt text](images/{28787FA0-8854-407E-BFF0-E163359FCD49}.png)

## Chunking Strategy

- Different chunking strategies
  1. `Fixed size chunking with overlap`.

     ![alt text](images/{568E3513-0E83-415B-A362-54E3ECC8EE12}.png)

     ![alt text](images/{726A9C75-3465-4621-BC36-C9CAACA1C857}.png)

  2. `Paragraph based chunking`
  3. `Heading based chunking`

## Fixed Size chunking with Overlap

Fixed Size chunking with Overlap strategy with metadata

- Chunk Size : 200
- Overlap : 50

So

- Chunk :{
  data: <Chunk Content>,
  source: <FileName>,
  chunkID: <Chunk Content>,
  Embedding: <Embedding>,
  metadata: <Tag VALUE>}

- Chunk1 :{0-200,"file1.txt",1}
- Chunk2 :{150-350,"file1.txt",2}
- Chunk3 :{300-400,"file1.txt",3}

![alt text](images/{6D01AE40-A71C-4D06-872F-BBC835AA78A9}.png)

![alt text](images/{7B93E7A4-A6FB-4BEA-9EE6-EDA786FD50F3}.png)

![alt text](images/{C44C8E9C-2F14-48F1-9193-3AF25FD344AA}.png)

![alt text](images/{F0FC986A-9ECC-4114-82AB-239FDFC675CF}.png)

![alt text](images/{AB40D52F-3086-482E-AEC0-2B9913979BCC}.png)

## Generating embeddings for chunks

![alt text](images/chunk_embedding.png)

So we will do embedding of each chunk and store the vector in its chunk itself.

So the chunk will have

- Text (Called `Chunk`)
- Document Name (Called `metadata` )
- Chunk_ID (Called `metadata` )
- Embedding (Called `metadata` )

`Metadata ` Data about the data

![alt text](images/{715E238F-30DA-4F31-9CC2-AAD20D3F5080}.png)

![alt text](images/{8D8294E8-3484-453D-9E72-582E0D3B3269}.png)

![alt text](images/{2DE53FFE-7933-470D-9B40-2464761B2759}.png)

We dont give only one chunk, we should return top 3-5 chunks

`NumPy`

- `cosine similarity` is directly related to `NumPy` in AI and RAG applications.
- `NumPy` helps you calculate how similar Score between two vectors are mathematically.
- For more advanced text chunking, you might use `LangChain` or `LlamaIndex`

## Sementic Search

![alt text](images/{DC30FA16-D26D-4B52-88A7-D19D448C0F95}.png)

![alt text](images/{CEACC48D-DA1A-483B-AF06-F3CA32F0799A}.png)

![alt text](images/{AE1288EA-FA81-43B6-B2DB-B06A273FFFC6}.png)

## What protocol allows AI applications to communicate with Tools

- Model Contex Protocol

![alt text](images/{AA37EC5D-7434-44BF-A7B0-A318B9B6DFA6}.png)

![alt text](images/RAGSearches.png)

## Hybrid Search (Keyword Search + Sementic Search)

![alt text](images/{494FF2DA-32FD-4D07-BCAD-AE83B9362C1A}.png)

`Keyword Score`

```
Query : 50 words
Chunk Size: 200 words
Keyword matched: 10
Keyword Score = 10/50 = 0.2
```

`Sementic Score`

```
Score given by the cosine similarity between 0-1 both inclusive
```

![alt text](images/keywordSearch.png)

![alt text](images/{CAEEA617-EB1F-4926-87EF-F01B4EB83748}.png)

![alt text](images/{3FC8D121-3458-47A0-A4AD-3FD9E1A6F94E}.png)

![alt text](images/{F02A037B-F424-4E20-87EF-19FDD09BA6F2}.png)

![alt text](images/{7A3EEC4D-BBA2-4DA4-B9EA-8728A3FD4C47}.png)

![alt text](images/{3F235237-B80F-4C50-98EF-CF30B9E9ADAA}.png)

So we have

- sementic score
- keyword score
- combined score

So we can sort on the bases of any of these and get top_k chunks.

`Sementic_weight` and `Keyword_weight` in Hybrid Search

```
Final_Score = sementic_weight * sementic_search_score


Final_Score = Keyword_weight * keyword_search_score
```

![alt text](images/{1D971972-E7CE-4B34-B8C4-580B3B595480}.png)

## Re-Ranking - Finding most relevant results

![alt text](images/{772B4278-6741-4485-B621-93A1FFF01A04}.png)

![alt text](images/{7A00E03A-BF9E-42E4-B215-7CA13BE439A9}.png)

![alt text](images/{1537D645-92B9-4455-AB5B-89304A52B35A}.png)

Components use LLM Models:

1. Task Decomposition
2. Planning
3. Assistant - Giving the Answer Naturally
4. Re-Ranking

`reranker.py`

![alt text](images/{041C1F4F-28CD-4F98-B81F-6599707C8C01}.png)

![alt text](images/{EC097B9E-EA32-454E-A195-A53CF2FE069F}.png)

![alt text](images/{438A0F9C-943C-4BEC-8191-F6BEFDA11BAE}.png)

![alt text](images/{CF5E4F0A-D196-45B2-9B7F-6D996676C318}.png)

![alt text](images/{D36B358F-D32A-4E6E-A424-6B1F6A337AFC}.png)

![alt text](images/{A7AC1902-A57B-47BF-AAFF-6E37EC6270EE}.png)

## Building a vector store with ChromaDB and FAISS

![alt text](images/{10EB9AE1-C864-4FB9-90EA-63DD6B779F71}.png)

![alt text](images/{097B8776-3B53-4648-AC1E-CCA6031A2926}.png)

![alt text](images/ollamatest.png)

![alt text](images/{D584772D-64EC-4BEE-9ACE-C1EE3E5A07C3}.png)

![alt text](images/{58806819-ECD7-41E4-B4A2-D4E03B579ED6}.png)

![alt text](images/{814FFF88-3FE0-4AA3-A7FE-E316987E6EAE}.png)

![alt text](images/{0C9154B7-5DD8-41F7-AA70-937A7513488D}.png)

![alt text](images/{30191C7B-CC02-4884-8424-0E9E1406C7F4}.png)

![alt text](images/{5D7ED57A-987E-4DC5-B88E-405B85B3D9F4}.png)

![alt text](images/{226A5762-0177-4131-A68C-732C4C752909}.png)

![alt text](images/{2334D6D7-77E7-4BB0-A015-13E805EAF8D0}.png)

![alt text](images/{9960E2D1-264E-4304-8576-34FCD3FA1304}.png)

![alt text](images/{EF0395D6-BE11-45F3-ACB6-ADC22370C767}.png)

## Building Sementic Search with Chromadb

![alt text](images/{0064E348-45C2-4986-94BC-A345B7CA6A7A}.png)

![alt text](images/{2344E59F-34A8-43BE-96FE-F66C910345E7}.png)

![alt text](images/{292C5561-266C-4D09-AA6A-199B598EFB0B}.png)

![alt text](images/{43A92159-3900-4BC6-B2FD-F597EB6AE416}.png)

![alt text](images/{C10E7A94-ED9C-4F86-B233-276D46AEFD68}.png)

## Evaluating and Improving RAG Retrival Quality

![alt text](images/{CCFCEAF6-B974-4A25-B617-1A875E5E0D2A}.png)

![alt text](images/{D2C0262D-B389-41D4-A017-75210E2BFF23}.png)

![alt text](images/{BCEF26A6-8521-44F8-B228-49EF6DB0A04F}.png)

![alt text](images/{E4E36AC0-7BF6-499F-8D0E-1A2020BCDE0E}.png)

![alt text](images/{FB9967D6-B173-43DA-97A8-754D532C0B96}.png)

![alt text](images/{121B6DE2-3706-4750-9E1D-FA5AC9D849C7}.png)

So in Production, `Evaluation Dataset` is created in advance for testing, where they know the answer of a particular questions lies in this chunkID

This is done to calibrate the values

- Top_K : How many candidate we should select
- Keyword_Weight and Sementic_Weight
- Size of chunk
- Overlap Size

![alt text](images/{2BE36A6E-0FEF-4FB1-AE32-E340FDEF4620}.png)

![alt text](images/{D6160936-0BE0-4945-873B-BDA39B9710D9}.png)

## Build the complete Advance RAG Retrieval Pipeline

![alt text](images/{F82922D5-C240-4F45-A1B7-C1D0E3D10943}.png)

![alt text](images/{FDB68927-991D-4096-8B5B-3FFDB50E8515}.png)

![alt text](images/{DFC741D8-C0A5-4C5B-85A3-FD23CEAD1BA5}.png)

![alt text](images/{918A5487-E302-4208-AC2F-BA6FF6591844}.png)

![alt text](images/{4112AEAB-0C60-4ECB-9D60-26219043BADB}.png)

![alt text](images/{45D601AB-F791-4A2D-81BD-AD7129D910F2}.png)

![alt text](images/{7622D901-9BC4-40A7-B4EE-4ACEB47E3379}.png)

![alt text](images/{11CDA222-DA8A-49F8-855A-5F610798EDAC}.png)

![alt text](images/{01353221-4841-455C-86FE-AC0AFC487A1C}.png)

![alt text](images/{8A1C7526-A83A-435E-9A00-BC691A7E755E}.png)

![alt text](images/{D3C92B8D-2577-4851-B97C-762770320CFA}.png)

`reverse=True` in decending Order

During Reranking we always provide the lesser number of items.

- Hybrid Search : Provide us the candidates
- Reranking : Narrows it further

## Build the final Generator for Advanced RAG

![alt text](images/{2512A32E-57C8-4DD4-8FFB-4D78F965CDA0}.png)

![alt text](images/{B3DA2235-6200-46FB-AE55-B8879DE97204}.png)

![alt text](images/{D7AE9F11-46F4-41F5-97F9-89C1716837A3}.png)

![alt text](images/{1FBD9135-CA42-47AC-B6FA-7103492FE95D}.png)

![alt text](images/{56D886A3-B735-4045-A2EF-0753CAEF3CE9}.png)

## Final Runner

![alt text](images/{3827616F-4725-461D-87B2-0AE152458D72}.png)

![alt text](images/{15D07259-8CD5-46A4-BE2E-711F9078E1D4}.png)

![alt text](images/{F0C792BA-70D8-481B-8DBD-64AFF713630E}.png)

![alt text](images/{1E441D7E-84D2-4AE8-8538-A968DB29F137}.png)

![alt text](images/{65FEA2E1-CF37-4B06-8703-65470BA7FBD2}.png)

![alt text](images/{ACBC4B79-DEFA-4BAC-B692-4FE20F6242EA}.png)

![alt text](images/{228FFFED-E324-4156-8029-BEEB9A343150}.png)

![alt text](images/{419EEC4C-303E-4272-BCEA-48C06B7DD1F0}.png)

![alt text](images/{2F29F31F-CED7-4F90-AA01-C00F4DAC2855}.png)

![alt text](images/{664D1CB8-8D86-4910-84C2-62B9B9ABAD9A}.png)

![alt text](images/{7B15DC99-56C1-4940-B595-1F1E789AAFAA}.png)

![alt text](images/{46A5304D-677F-447F-9678-A9C603B715DE}.png)

![alt text](images/{99835B6F-F967-4AE4-B8C9-83487B0079CC}.png)

![alt text](images/{DA9CCEBE-6021-4FF1-A50A-5C23EC43E01E}.png)

![alt text](images/{1B051140-1AD8-4D33-ABE9-B8A5B7F60D9B}.png)

![alt text](images/{68CF1F39-3394-4A2F-81CE-D3A9D952DED0}.png)

![alt text](images/{4DC2148C-B40C-4B2F-BF4B-AA75E58B99D6}.png)

Other better Options:

- Qdrant (best self-hosted vector database to evaluate for production RAG)
- PostgreSQL + PgVector (best when you already use PostgreSQL)
- ChromaDB (Good for POC and local apps.)

![alt text](images/{4A767333-E774-4397-A797-9D566377D1CF}.png)

For a Python multi-agent orchestrator with RAG, I would shortlist these three:

- Qdrant — a dedicated vector database for a production application.
- pgvector — if your enterprise already uses PostgreSQL.
- FAISS or hnswlib — if you want to build and manage your own vector-search layer.

## LangGraph

It represents a workflow as a Graph.

There are 3 concepts you should remember

1. State : The information travelling through our workflow
2. Node : A piece of work performed by the workflow and update the state
3. Edge : The connection that determines what heppens next.

![alt text](images/{6AA10510-034C-41A2-9498-B9554DAC58D9}.png)

![alt text](images/{DE4C3959-747A-4B09-9771-7E8353893A25}.png)

`State Schema`

## Install langgraph

```
pip install langgraph

pip show langgraph
```

`TypedDict` to describe the structure of the dictionary that travel through the graph.

![alt text](images/{2F7674B2-23D5-4CCC-BFE3-698BA212585A}.png)

`Node` : A function that receives current state and return updated state, so Node take only state as input not other argument.

## Create Planner node

`StateGraph` :The is main LangGraph class used to create a graph based on a shared state.

`START` This represents the entry point of the LangGraph. The graph starts from the START and moves to the first node.

`END` This represents the termination point

```
START (Node)
|
V
PLANNER
|
V
END (Node)
```

So the workflow is converted into Graph in LangGraph.

## Multi Agent Systems and Orchestration

We have two components:

- Manager : For Orchestration(Management). It decides
  - Which agent should work
  - What should happen next
- Worker: The Agent responsible for actually performing the task.

It is similart to, in a autonomous/Single Agent

- Planner : who decides
- Tools/MCP/RAG - Respobsile for actual task.

## Building specialized AI Worker Research, Coding, Review Agents

`ResearchAgent.py`
![alt text](images/{20F6F5EC-6514-4C93-923B-FC9A4483799C}.png)

`CodingAgent.py`

![alt text](images/{A3359413-9C26-41A7-B456-93EAF62BD3A4}.png)

![alt text](images/{BCC7DBDE-1DF1-43F6-8519-2DB74B112A3F}.png)

![alt text](images/{3A80FBC5-5E8D-4B19-82C6-7DC9C98EE4E1}.png)

`ReviewerAgent.py`

![alt text](images/{8EDD8705-3416-406B-B3E3-75A435BD5297}.png)

![alt text](images/{92402265-61B1-4F35-A35A-024FCC142C3A}.png)

![alt text](images/{3A80FBC5-5E8D-4B19-82C6-7DC9C98EE4E1}.png)

## Building Manager Agent - For Orchestration

`Manager.py`

![alt text](images/multiagentmanager.png)

![alt text](images/{C4AC45FC-E168-40A9-8AD5-9C1A36D9C32C}.png)

![alt text](images/{F8084D1B-4AB8-4FD1-A7F7-12FAB01C3908}.png)

![alt text](images/multiagentflow.png)

## Build Executor, that call the manager

![alt text](images/{32A06F65-3C19-41E3-9B87-167DB7975AC3}.png)

![alt text](images/{0DF87C14-EE3A-4E02-B8F8-41181D8C3F30}.png)

![alt text](images/{FBE9EABD-D96E-4AA4-8712-04A2B9451F9F}.png)

![alt text](images/{A8F8649E-457B-4A07-ADEC-B6596E7EF422}.png)

## Making Multi-Agent Intelligent

`ManagerState`

![alt text](images/{EA740DB0-DE9A-4A18-BAD2-A1242A07F7E1}.png)

![alt text](images/{2CC6C7DA-8EB1-434B-B4D2-1559FA87CA34}.png)

![alt text](images/{43822BD4-FF47-4D87-83E7-26EB79D2B0FF}.png)

![alt text](images/{28787FA0-8854-407E-BFF0-E122359FCD49}.png)

![alt text](images/{6E491EC9-9BD0-4B8C-92E0-456EE993085A}.png)

![alt text](images/{48A38620-ACC5-4B78-8E1C-B6AE8C77B409}.png)

`Manager`- Chooses among agents
`Planner`- Chooses among tools

## Manager Loop

![alt text](images/{A35ABA4E-E1AC-45B3-8585-3D3BA7C17A4D}.png)

![alt text](images/{F29126C5-9EFE-406C-9461-2B218501F78D}.png)

![alt text](images/{DE3C62E8-79E3-47A6-B673-2F8269448B1C}.png)

![alt text](images/{CC1EFE23-2053-495E-BF77-B524E9187FC0}.png)

`main.py`

![alt text](images/{7B67A6CA-6364-40BF-A6BE-F036FB700174}.png)

![alt text](images/{8CC22431-4C40-41A7-A062-13BCDD640FA8}.png)

![alt text](images/{89C56FEA-4E81-40E6-99FB-1D8AF08C9195}.png)

## Making your AI Agent Production Ready

![alt text](images/{89247FDD-A7ED-4A9A-942E-B2AE5F14F8C4}.png)

![alt text](images/{DE9388A2-9EB3-4739-B821-8E7BB9A72AB1}.png)

![alt text](images/{F73AEEE1-3E34-46FE-A094-28129C591CA0}.png)

## Centralized Configuration

![alt text](images/{F0C95566-16BA-4192-A80E-660C94F49F89}.png)

![alt text](images/{1CD1E476-BFE9-4944-859D-A2853551AC85}.png)

![alt text](images/{5815E1B4-4A2B-4CBF-90D8-D740EA60A29E}.png)

![alt text](images/{B4E49EEB-3B1E-46FD-9833-22FBA489B407}.png)

![alt text](images/{8006F0D3-AF1E-4D44-BD78-9A679B074164}.png)

## Centralizing LLM Access in AI Agent

![alt text](images/{4C3780EB-7290-4C33-A4CC-CAFE1FCD78EB}.png)

![alt text](images/{E26AB203-5E82-4148-850D-BCDBF59DBB39}.png)

![alt text](images/{B55F7E23-8BC0-4E29-8A6A-C5EB97BAF878}.png)

![alt text](images/{2DB46710-C492-4251-AF26-7A493E341F48}.png)

![alt text](images/{B9A9C748-9913-4FF4-A8E2-1DDE0333B1B3}.png)

![alt text](images/{4586B750-A147-4D40-94CD-183B583A1A58}.png)

![alt text](images/{2112B7C5-7CE4-4155-9093-439739E16A79}.png)

## Building Observability with Logging

![alt text](images/{891E7399-E545-473F-A17C-A90FBDC05709}.png)

![alt text](images/{637DEA79-E956-434F-B8DA-8DEEC118341E}.png)

![alt text](images/{FB4C3E5F-D5DE-4341-805E-845BADD995C6}.png)

![alt text](images/{322BC2F3-895C-42E7-8030-5A5F6946714C}.png)

## Building Reliability with LLM Retries, Backoff and Timeouts

![alt text](images/{A03DF773-C6A7-44D7-A0C1-FE2A0377A406}.png)

![alt text](images/{5C965BB5-FDF4-4B88-9174-B4A70901996D}.png)

![alt text](images/{700E8045-141D-4EA8-8079-D6FA3448FDA4}.png)

![alt text](images/{A8E220C2-0B38-4160-B656-9B39C4834F5D}.png)

![alt text](images/{77F5D368-184F-44EA-B658-D2E31627F306}.png)

![alt text](images/{B7C636B5-B840-4B44-9264-18F6F9CD73B8}.png)

![alt text](images/{038F6C62-63F2-4B25-9CA7-638058007D29}.png)

![alt text](images/{47BEFA41-C4AD-458E-B645-B1C7B7311D09}.png)

![alt text](images/{6CB91B4D-84C7-4D6F-90FB-C412B6DFA443}.png)

![alt text](images/{BE7D45BC-7903-4C3B-9FDF-F937912F9EF4}.png)

![alt text](images/{1012C202-887E-4904-B2E5-73B950EE88CA}.png)

TIMEOUT is in seconds

![alt text](images/{2B57BF5C-99C6-4737-B12B-33EFB878EDDC}.png)

![alt text](images/{71C243B9-C0CB-48E7-8DA3-590F7D0DA3FE}.png)

![alt text](images/{56DB9D44-0B86-4608-800D-B4997B8078C2}.png)

## Security

![alt text](images/{13B72A77-A680-47FA-AF92-478F22F59924}.png)

![alt text](images/{211ED50C-6CDF-4BDC-A32F-85CB65523958}.png)

![alt text](images/{52B30228-3A5A-45AB-89F4-131C7B04F277}.png)

![alt text](images/{0D9110D0-159B-43BF-B7DF-0988164B2F8F}.png)

![alt text](images/{5537EB45-7B33-40E5-836F-61C2818D03BD}.png)

![alt text](images/{E4DBA6F3-56E6-4E4F-974B-C6316B1986C2}.png)

![alt text](images/{0E643F4B-A16E-4392-8078-D24A66EE0338}.png)

![alt text](images/{0249AADB-221B-41DC-8FA2-A819220311BA}.png)

![alt text](images/{1F123247-0357-489D-9532-3FA89DE4F0F2}.png)

![alt text](images/{131A9746-CEBA-40BD-B885-7545ACDA480D}.png)

![alt text](images/{07064843-A862-43F5-8162-A2FCCE28856C}.png)

## Safe File Access and Prompt Injection

![alt text](images/{A91E302C-84B5-426A-9269-1DBC94865FE8}.png)

![alt text](images/{4AB771B6-56DB-484A-A089-A9CA53B2025E}.png)

![alt text](images/{8E0049A0-C13A-4962-9BFB-26E8483B4DE4}.png)

![alt text](images/{9B8F0D7C-5AE9-4383-B9F7-734E73F87100}.png)

![alt text](images/{2EB5A81D-D733-4F00-AD42-E5DD3072797F}.png)

## Performance

### Parallel Execution

![alt text](images/{7DFA2EB4-5828-444B-8265-95494761C6E4}.png)

![alt text](images/{CF82D71E-3EA2-468D-8B86-7BD9683FE61A}.png)

### LLM Calls

![alt text](images/{07A46A2C-C2A4-4C01-A8CF-6D6FF60B375B}.png)

### Context

![alt text](images/{AE37CC97-DD92-4A8E-804B-B02E5501EDAD}.png)

![alt text](images/{4985102D-82AE-471D-BF60-B2F325921097}.png)

![alt text](images/{2DE58980-FC14-41E4-8F18-8E92B9815956}.png)

![alt text](images/{7E1B7E98-A78D-4D0E-B9D6-33032F59B475}.png)

### Refactoring

![alt text](images/{81A4B971-B15F-4B12-8AE1-9D07D058987F}.png)

![alt text](images/{BC9F4D30-EECA-4E27-A6D6-4BBC5046D9E1}.png)

![alt text](images/{FF1FD5C5-99CD-42A4-8D28-397553E8121C}.png)

## Final Production Architecture

![alt text](images/{212FB007-BBB2-4FA1-8ABB-F3511E7F132D}.png)

![alt text](images/{8D3B01A6-DA5E-488F-8EEC-497A2A019673}.png)

![alt text](images/{488ADBF6-C23C-42C0-BC93-2E9C10CDF917}.png)

![alt text](images/{C4093E0F-6B85-4A8D-89E0-CBE24DD66A9E}.png)

![alt text](images/{AE15B1DE-47CF-4CCE-AE56-48403ECB0DDE}.png)

![alt text](images/{E10B599C-1938-422D-9556-B2EB2EEB9F5F}.png)

![alt text](images/{792EFF0A-825A-4BA9-90F3-4ACA562571C7}.png)
