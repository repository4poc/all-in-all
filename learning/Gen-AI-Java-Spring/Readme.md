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

![alt text](images/project_create.png)

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

## Streaming response

![alt text](images/{F436F0EB-95D8-4AC4-85B9-F9134D7C0B5C}.png)

![alt text](images/{7D5BEAF2-A7F0-4289-9BF6-EC406CB3B33A}.png)

## Spring AI Response Types

1. ChatClientResponse
   - Return the context[] - Tools context

     ![alt text](images/{7D1D84FD-1B19-463F-952F-87A12EE42139}.png)

   ![alt text](images/{229E0878-201F-4C91-9CC6-18107A05C0A4}.png)

2. Content

   ![alt text](images/{0419DAA5-67EF-4255-9585-411F1B6EB12B}.png)

## Structured Output

![alt text](images/{BDF91932-5605-42DE-BA02-749DA371B43F}.png)

![alt text](images/{0ECEC1B4-8D55-45BB-A03F-8E0377180990}.png)

![alt text](images/{61B54ECA-3712-4213-99EF-7269BF7C66B9}.png)

![alt text](images/{70736748-215B-41EA-8BD7-7CAFB16AFF2C}.png)

![alt text](images/{CFC67261-46F4-4685-874C-3AB92397FF6D}.png)

![alt text](images/{E344EFD3-605C-4000-83B4-E83C0B5CF68E}.png)

## Chat Options

1. **Default Chat Client Option in application.yml**

   ![alt text](images/{BEEE0563-D320-44AC-975C-02A0525BE8C6}.png)

2. **Chat Client Options applicable to all endpoints in AIProviderConfig.Java**

   ![alt text](images/{B055C30E-82A3-4B7F-B734-72180C4DE042}.png)

3. **Chat Client Options applicable to specific endpoints**

   ![alt text]({CEEA03A3-A3C6-4E3F-BF86-92576625E738}.png)

Some Chat Options are generic

- Temperature
- Model Name
- Max Token

Some are specific to LLM Providers and the models

![alt text](images/{FCB391B5-BD54-47DF-94E2-EDCA248235BA}.png)

**OpenAI Token Calculator**

![alt text](images/{EFCBA053-9978-48F2-AE13-7F5EA54CAA74}.png)

![alt text](images/{755B728F-0DB8-4A97-9BEF-1B5F90FCA2EE}.png)

![alt text](images/{06A9550A-8506-436D-846A-925B00FE49FE}.png)

**Temperature**

It controls the Randomness/Non-deterministic level.

![alt text](images/{78582F26-E159-4CF0-AC8E-305D7BF98A1D}.png)

![alt text](images/{C66C91A1-91A4-4923-9A6F-00919B939A4A}.png)

![alt text](images/{9482BF36-7249-4382-AE97-586C284D7F9D}.png)

With Temperature = 0 : Randomness is disable, we consider highest probability values, so get the same response everytime.

With Temperature = 2 : Randomness is maximum, we consider lowest probability values, so get different and creative response everytime.

We can control the temperature Or Randomness via User Prompt as well

![alt text](images/{8DABF6CF-7E3F-477A-9381-80EAFF8DD6A9}.png)

![alt text](images/{9EB4A446-0572-4EC1-94AF-BE1CF05B501F}.png)

**Top-p and Top-k**

![alt text](images/{907B20B8-C06A-4030-9318-8295728A348D}.png)

![alt text](images/{8D166019-3A29-4ED1-9566-EFC4D4AEF04A}.png)

![alt text](images/{1DFAC8A8-5F95-40E1-AF22-B08A5C9879DD}.png)

![alt text](images/{B1DD6CC9-CC37-4927-AF5D-27D66F825F05}.png)

Temperature (0-2)
Top-P (0-1)

Temperature(2) = Top-p(1)

Temperature(2) + Top-p(0.1) = Temperature(0.1) - Min. Randomness/Deterministic

Temperature(2) + Top-p(0.9) = Temperature(0.9) - More randomness/Non-Deterministic

**Almost same result every time**

![alt text](images/{F2897055-5956-4349-9EEB-788B8E16155D}.png)

**Top-K**

![alt text](images/{4157CB9E-E1CE-42CF-B1F2-3207715152A9}.png)

There is no topK() method in OpenAI, in Ollama, Google Geminin

![alt text](images/{17065A82-2425-4B9D-BB76-412504D8A0B7}.png)

![alt text](images/{306A1BEE-B975-4FB0-B94F-CC1DEC9452F5}.png)

**Frequency Panelty and Presence Panelty** - Specific to OpenAI

![alt text](images/{CCEF4B58-8C3A-4983-909B-85F6BFC5E201}.png)

![alt text](images/{FD167F0C-9BA1-43BC-9688-E56007ECA0A2}.png)

![alt text](images/{2152BAE6-E832-4C21-9A32-19A0F904A135}.png)

![alt text](images/{5489DEB6-32F8-467B-B76F-6D5EE65D5EDE}.png)

![alt text](images/{2C001DE0-6354-4662-84D8-5DDCEE22975C}.png)

## Spring AI Advisors

![alt text](images/{8DE922E8-A300-4496-8D54-DE8D56970E11}.png)

Advisor can intercept the req/req for

- Logging
- Enforce Validation and Security
- Similart to middleware or Filters
- Executed in a chain in the order they are added, before the actual LLM model is invoked
- The last Advisor in the chain is always
  - ChatModelCallAdvisor OR
  - ChatModelStreamAdvisor
    Depending on the call type

![alt text](images/{510A35D2-2494-4882-AFEC-C3BEFD33B8CF}.png)

![alt text](images/{7F79A3E7-73D4-4814-89DE-C71DBC6A30D9}.png)

So with Advisor, you can handle cross-cutting concerns - Authentication, Logging, Exception Handling, Tracing, Guardrails, Caching, Retries, Prompt Transformation

### Around Advisor Architecture

![alt text](images/{C3F82510-50C0-47FE-B51E-C70EE4BAF109}.png)

![alt text](images/{73E623CD-9CD3-4416-B5BB-5FFDEAB0E5F2}.png)

The first Advisor in the chain is

- last to process the request
- First to process the response

### LoggerAdvisor

![alt text](images/{4803D29C-34E9-456E-B400-8F6D4377E20B}.png)

![alt text](images/{A2EB2FED-7523-4D2C-B369-653209111C85}.png)

![alt text](images/{4F346D20-FB0E-4BE4-A7BC-DE4DA3AC907A}.png)

![alt text](images/{B573B3C3-9ECE-4E94-B507-389A9CA3A802}.png)

![alt text](images/{AC2D8714-C372-4C13-9FC8-69B6C261EF7C}.png)

![alt text](images/{C68314EF-7331-481A-928D-60DA2D2BC425}.png)

If we set the Log Level to INFO, it will not list the DEBUG Logs

![alt text](images/{4D0E651A-6EB3-4EB8-A69D-7F7F04B0FCB8}.png)

In production set the Advisor log Level to Off

![alt text](images/{3F3ACC02-F8DA-4548-A6F5-E993F5CC22C3}.png)

### SaftGuardAdvisor - SpringAI Build-In Advisor

Detect sensitive content in the prompt, and Return with failure message if found

1. Add Bean in the AIProviderConfig.Java

   ![alt text](images/{DFA52216-F069-4B6C-926C-D6F03F03B8E1}.png)

2. Return Failure message

   ![alt text](images/{BAD6D3E9-D7E5-4B72-A1E6-7F797C69E663}.png)

3. Add the SafeGuard Advisor during ChatClient instantiation.

   ![alt text]({A6F1CAA1-0CCE-4C6D-9C7A-9E386CE99F45}.png)

   The Advisor executes from Right to left in the list, so loggerAdvisor --> SaftGuardAdvisor, If we put it in reverse order the loggerAdvisor will never be able to log the sensitive words in the log.

   SafeGuard Advisor has Order=0, so it will be executed based on the place we place the advisor. `Loser the order, higher the priority`

   Always have alls in try..catch

   ![alt text](images/{FEF4328E-A223-44B6-8020-ABA97EA3457F}.png)

   In reality all the exceptions should be send to Queue.

   `Problem Statement` If any exception happes, the is no Exception Handling Advisor, so we should place ErrorHandlingAdvior at the very first place before loggingAdvisor.

### Custom ErrorWrapperAdsvisor

`Step 1`: Create a class with interfaces `CallAdvisor` and `StreamAdvisor`

![alt text](images/{286993CF-4650-4876-901D-231ECC6A1CC5}.png)

`SLF4J` (Simple Logging Facade for Java) is used to provide a common logging API that works with different logging frameworks (such as Logback, Log4j 2, java.util.logging, etc.) without tying your application to any specific one.

Decouples your code from a logging implementation

![alt text](images/{1B0C8CF9-4ABF-46DF-870C-443848ED1646}.png)

![alt text](images/{1162B43E-CB09-4CAC-A57C-92E8039CF4E8}.png)

![alt text](images/{2FDBE434-B4FD-49A8-9814-23C303B9EE34}.png)

`Before`

![alt text](images/{6D2186B4-E341-48B0-A0D8-602D4F29989A}.png)

`After`

![alt text](images/{00918DBF-1285-4873-9A91-92FC4CCD0EC0}.png)

To set an advisor call first, irrespective of the position

![alt text](images/{4B5F5AB7-90BC-43FC-953B-E0C57B0192B8}.png)

To set an advisor call last, irrespective of the position

![alt text](images/{8F05FC7D-C63B-411F-AC2C-CA6BC4A8DC4C}.png)

In case the Advisor does not call the Next Advisor
![alt text](images/{B1313278-255A-4011-995E-4A3CFBEBD156}.png)
![alt text](images/{61AA6005-927E-40BC-BDBA-20E1E5D8E121}.png)

![alt text](images/{C52E94D0-FCDC-426C-BA17-77B5D1B730B0}.png)

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

**Prompt Engineering - Techniques **

1. Zero-shot prompting: Asking the model to perform a task without prior examples
2. One/Few-shot prompting: providing examples to guide the model
3. Chain-of-thought prompting: Encouraging reasoning step-by-step.
   ![alt text](images/{72658787-8F08-4E8D-A402-31D82D91CDE1}.png)
4. Choose correct LLM model: As each model is trainsed on different datasets and for different inputs.
5. System Message : Set the role or context for the AI

   ![alt text](images/{EC3FA992-6877-4370-ACDF-E932050468C4}.png)

6. Prompt Templates :
   - Create a tempalte and parameterize it.
   - Templating and parameterising the prompts for more seure and better maintainable prompts
   - Can Reusable templates with diff. versions

     ![alt text](images/{A3145178-285D-4C0C-8AF7-8001F1271475}.png)

     ![alt text](images/{905AEC9D-D254-4697-9AE3-E0B42A8FF11C}.png)

     ![alt text](images/{B00E5721-776A-4D97-8D96-44D51EB0A177}.png)

7. Temperature & Top-p & Top-k Tuning:
   - Adjusting creativity and randomness in responses

     ![alt text](images/{9B325D62-C89D-4609-BAEE-74B95A95447E}.png)

     ![alt text](images/{BC2ABA41-3551-4796-81BB-33811B503CF1}.png)

8. Returning multiple results :
   - Instructing LLM to return multiple results to choose amoung them.
     ![alt text](images/{41B8EDC8-08FE-41FE-AC60-A5083BC89091}.png)
     ![alt text](images/{CE016FE8-1E7D-499E-BBC8-F6BCD8AAA633}.png)

9. Token Limit
   - For cost control
   - limit network bandwidth - less latency in responses considering the context windows size of the model because we cant exceed the size of context window will get an token limit errors

     ![alt text](images/{CABE42F3-7055-424F-BE49-5719DCFEDED7}.png)

     ![alt text](images/{7C8F555D-1B9B-4191-8A63-C9A8F4DB8FB7}.png)

**Prompt Engineering - Best practices**

1. Use `System Message`
2. Use `Multiple Responses`
3. Use `Prompt Template`
4. Use `One/Few Shot Prompting`
5. Use `Size Limit` to Limit the Output Token
6. Use `Temperature` to adjust the `creativity` in responses

## Quiz

![alt text]({BB4DA721-796E-4D40-9716-070B9D864661}.png)

![alt text](images/{A763F326-52B8-4FED-8CA0-CBBB06F1368C}.png)
