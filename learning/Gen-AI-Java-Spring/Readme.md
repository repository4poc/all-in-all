## Source

https://www.udemy.com/course/complete-genai-with-java-spring-ai-llms-rag-ai-agents/learn/lecture/55319467#overview

## JDK 25 installation

![alt text](images/{E5941B54-DDA4-4A6D-AEAC-3A0FBF3726CC}.png)

Download the zip file

Set Environment Variable

- `Path` to .../bin
- `JAVA_HOME`

- java --version

## Maven 3.9.16 instalation

Download the zip file

Set Environment Variable

- `Path` to .../bin
- `MVN_HOME`

- mvn --version

## Which of the following functionalities provided by Spring AI to modern GenAI applications?

![alt text](images/{A338E140-9A52-4FE9-8ED7-EB1F1181670A}.png)

## Spring AI

- A Framework for integrating AI-powered services into Spring Boot Application

## Spring AI Framework Features

https://spring.io/projects/spring-ai

- Multi-modularity : Support for all major AI Model providers
- MCP - To integrate with external systems
- Based on Spring a mature and reliable framework
- Flexibility to choose providers and LLMs
- Observability : Provides insights into AI-related operations.
- Structured Output : Mapping of AI Model output to POJOs.
- Tools/Function Calling - permits the model to request the execution of client-side tools and functions, thereby accessing necessary real-time information as required.
- AI Model Evaluation - Utilities to help evaluate generated content and protect against hallucinated response.
- ChatClient API - Fluent API for communicating with AI Chat Models, idiomatically similar to the WebClient and RestClient APIs.
- Support for Chat Conversation Memory and Retrieval Augmented Generation (RAG).
- Spring Boot Auto Configuration and Starters for all AI Models and Vector Stores - use the start.spring.io to select the Model or Vector-store of choice.
- Prompt Templates - allow reusable and structured prompts
- Streaming Response : Support real-time token-by-token AI response.
- Easy integration with Spring projects

## Supported AI Providers

- OpenAI (GPT models, chat completing, embeddings)
- Google Vertex AI (For enterprise-grade AI models)
- DeepSeek API (Optimized models for various applications)
- Ollama (running models locally without external API calls)
- Hugging Face (Integration with Open-Source LLMs)

## Create an account in Open AI

- Create An Account with $5
- Create an API Key
- export OPENAI_API_KEY=\* \* \*

**Windows**

- set Environment variable OPENAI_API_KEY=\* \* \*

**Linux**

- ~/.bash_profile

## UserCase : Meeting Notes Summary

![alt text](images/{315AA946-BEA2-4D00-9E1F-AE1B0659E295}.png)

## One Shot Prompting

User Prompt with one Example to
clerify the task for the model

![alt text](images/{727F2998-A33F-4AC5-900A-F3A086AFCE54}.png)

## System Messsage

- Restrict the context to provide a guardrail

**System Prompt**

```
You are a helpful assistant that summarize any given content. Ensure the summary is concise, informative, and captures the key points.Use a friendly and approachable tone while maintaining professionalism.Do not answer anything other than the summarization. If the question is not about summarization respond with 'I can only help with summarization tasks
```

## Applying guardrails using System Messages

Restrict to summarization related queries

![alt text](images/{6A51BDB5-9396-44DA-840A-3A8930C91135}.png)

![alt text](images/{4A4B5FC2-9AFD-446F-A0EE-C2710FD7BDBF}.png)

![alt text](images/{09ECC5BA-F9A4-4493-99C5-448ECAD0FECC}.png)

## Content vs ChatClientResponse

ChatClientResponse not only send you the result, It also provide details like

- PromptTokens (System + User Token)
- CompletionTokens (Response Token)
- TotalTokens
- Model Used
- Metadata

Userful in monitoring tools

![alt text](images/{4A4B5FC2-9AFD-446F-A0EE-C2710FD7BDBF}.png)

## Tokens in AI

- Basic Unit of text a LLM Process.
- AI Providers bill you based on Input Token and Output Token Usage.

## In-Place Prompt Template

- User provided data in LLM Communication
- More secure, as user input is just a parameter instead of something that is asked to the LLM directly
- Also include the example, so the User need not to specify the one-shot example in the prompt.

![alt text](images/{190B676A-9FE2-4633-94EA-A6E87A6F6ED7}.png)

![alt text](images/{9979AA3E-238B-40E3-885A-4251817A5A78}.png)

## Spring AI - OpenAI API

Spring AI framework calls OpenAI API using HTTP Client

![alt text](images/{F707F256-0FE7-4EB8-993C-152B9FC7F7DA}.png)

![alt text](images/{4EE9A946-60F5-458D-A134-05D59B022B3C}.png)

```
Spring Boot application
        │
        ├── REST API
        ├── Security
        ├── Database
        ├── Messaging
        │
        └── Spring AI
              ├── ChatClient
              ├── RAG
              ├── embeddings
              ├── vector store
              ├── tool calling
              ├── MCP
              ├── memory
              └── observability
```

## Create Springboot Project

https://start.spring.io/

Spring Initializr

![alt text](project_create.png)

In modern Spring Boot applications, you almost always create a JAR, not a WAR.

JAR (most common)

Spring Boot includes an embedded server such as:

- Tomcat
- Jetty
- Undertow

So the application is self-contained:

This is the standard approach for:

- REST APIs
- Microservices
- Cloud deployments
- Docker containers
- Kubernetes

**Update Dependencies**

```
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-web</artifactId>
		</dependency>

		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-test</artifactId>
			<scope>test</scope>
		</dependency>
```

**Add Class**

```
@RestController
public class HelloController {

    @GetMapping("/hello")
    public String hello() {
        return "Hello from Spring Boot!";
    }
}
```

**How to run the SpringBoot Application**

1. Build Jar and Run it

```
mvn clean package

java -jar target/myapp-0.0.1-SNAPSHOT.jar
```

**Access The application**

![alt text](images/{4BD34252-570F-428E-8E78-38C0CE50BFDD}.png)

![alt text](images/{BC138668-4635-44B4-A12D-3A512AAEA37A}.png)

| Deployment style                  | Typical packaging |
| --------------------------------- | ----------------- |
| Docker                            | JAR               |
| Kubernetes                        | JAR               |
| AWS ECS/EKS                       | JAR               |
| Azure Container Apps              | JAR               |
| Standalone VM                     | JAR               |
| Traditional enterprise app server | WAR               |

**System Prompt**

```
You are a helpful assistant that summarize any given content. Ensure the summary is concise, informative, and captures the key points.Use a friendly and approachable tone while maintaining professionalism.Do not answer anything other than the summarization. If the question is not about summarization respond with 'I can only help with summarization tasks
```

## Prompt Engineering

- The process of designing prompts (inputs) to get the best possible response from an LLM

- It helps AI models to generate, relavant, accurate and context-aware responses

- Used in various applications like chatbots, content generation, and automation

**Prompt Engineering - Principles**

1. Clarity : Be clear about the task
2. Context : provide relavant background information (SYSTEM PROMPT)
3. Constraints : Set boundries (word limits, formats, etc) (GUARDRAILS)
4. Examples: Demonstrate expected output
5. Iterative Refinement: Experiement with different prompts for better results.

**Prompt Engineering - Techniques**

1. Zero-shot prompting: Asking the model to perform a task without prior examples
2. One/Few-shot prompting: providing examples to guide the model
3. Chain-of-thought prompting: Encouraging reasoning step-by-step.
   ![alt text](images/{72658787-8F08-4E8D-A402-31D82D91CDE1}.png)
4. Choose correct LLM model: As each model is trainsed on different datasets and for different inputs.
5. System Message : Set the role or context for the AI
   - Also used as a guardrail
6. Prompt Templates :
   - Create a tempalte and parameterize it.
7. Temperature & Top-p Tuning:
   - Adjusting creativity and randomness in responses
8. Returning multiple results
9. Limit Token usage
   - For input and output
   - For better performance, less latency
   - For cost control.
     ![alt text](images/{CABE42F3-7055-424F-BE49-5719DCFEDED7}.png)
