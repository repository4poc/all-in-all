## Lambda Functions

- Equivalent to "Azure Function App"

![alt text](images/{25F195A3-5704-430F-ABB7-DD8EFAF0EE83}.png)
![alt text](images/{4D013079-A4B8-4976-A2B9-D4CCF5DC1500}.png)

- In both EC2 and BeanStalk, we provision instances.
- In Serverless, the underlying infra is completely managed by the cloud and don't have any access to it

![alt text](images/LamdbdaFunc.png)

## Lambda Key Concepts

![alt text](images/{D78C7A2D-8BB6-4547-A8AD-A1C8D05251A6}.png)
![alt text](images/{9A57554C-8977-46DE-94CE-BB19C1533115}.png)
![alt text](images/{A2A009F4-03C0-4BDD-A1F6-A9387B25A6E8}.png)
![alt text](images/{6FDFC4E4-8F3B-451F-A4DC-0AAF80DDE702}.png)

## Lambda Cold Start

![alt text](images/{0E357E8D-D9E3-4841-9405-0487E46A19F6}.png)
![alt text](images/{81A0FF2A-E635-4AFF-B7A6-8EBA2C41F4D1}.png)
![alt text](images/{00198743-E6DD-475B-97E4-5F93EF2708E5}.png)

## Lambda Functions Pricing

![alt text](images/LambdaPricing.png)
![alt text](images/{C418F1D3-866F-4F54-9885-FF96101B6FEA}.png)

## How to create an AWS Lambda Functions

Pre-requisites :

- Install AWS Lambda Templates

```
dotnet new install Amazon.Lambda.Templates

dotnet tool install -g Amazon.Lambda.Tools
```

- Install AWS Serverless Application Model CLI Tool
  - Tool created by AWS assist in creating and running serverless applications
  - https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/install-sam-cli.html

```
sam --version
```

## How to login to AWS

- Create a user having both portal and cli access with useradmininstrtor role
- Generate the Access Key, for CLI login

CLI Authentication

```
aws configure --profile dev
```

![alt text](images/{03EA2A7D-7842-4332-80C3-76AF206129CC}.png)

```
aws sts get-caller-identity
```

![alt text](images/{A2BC95C3-BF8C-48C7-A0E1-6CEF69602204}.png)

**Connect to the AWS profile**

![alt text](images/{0FC53953-B284-4AB3-A5CA-24C2B7B1D096}.png)

**Create lambda SAM Application**

![alt text](images/{848F611B-5BBC-4B56-8D40-EF64F0E1EECA}.png)

![alt text](images/AWSLambdaFunction.png)

**Build the aws lambda code**

```
sam build
```

![alt text](images/{1F24738E-8B18-4BCA-957B-3BF5922809AD}.png)

![alt text](images/{91D54E02-E8D4-4A19-805B-F573A32D24AA}.png)

```
sam local start-api

- Make sure docker desktop is running
```

![alt text](images/{D553F512-F3D6-47D5-A71D-9430A9D81C59}.png)

![alt text](images/{E0590030-AA05-416A-8FD1-7721A5AE3386}.png)

```
sam deploy
```

![alt text](images/{78DA378B-F569-4FD6-82F3-8659FD6453FD}.png)
![alt text](images/{702BC9FB-62BA-4413-9F80-7DDB5AF4D70C}.png)
![alt text](images/{9AD880AC-EC93-45A0-A218-148B728D35F2}.png)
![alt text](images/{8A067905-27B9-4AF2-850F-D41B16DFD6AC}.png)
![alt text](images/{91E15D37-DAAC-49A4-AA73-1602E5CAEF01}.png)

**Live Logs**

![alt text](images/{3C28E0ED-3B83-4D46-8E01-F40E638AF0E5}.png)

## Step Functions

![alt text](images/{FAEDD774-0FB2-4B50-B34F-B88D438ADACA}.png)
![alt text](images/{E5AD114E-6C6A-4FA0-99FF-AE5D562EF789}.png)

| AWS                     | Azure equivalent                  |
| ----------------------- | --------------------------------- |
| AWS Step Functions      | Azure Logic Apps                  |
| Lambda                  | Azure Functions                   |
| Step Functions + Lambda | Durable Functions                 |
| EventBridge             | Event Grid                        |
| SQS                     | Service Bus Queue / Storage Queue |
| SNS                     | Event Grid / Service Bus Topic    |

| AWS Step Functions feature        | Azure equivalent        |
| --------------------------------- | ----------------------- |
| Workflow orchestration            | Azure Logic Apps        |
| Code-based workflow orchestration | Azure Durable Functions |

**Minimum Memory Available to AWS Lambda function : 128MB**

![alt text](images/{BE8F5D6C-8C12-46A0-A8FF-1D37FA2E2EE1}.png)
Lambda functions are designed for lightweight tasks and have limitations on execution time and memory, making them unsuitable for processing a large batch job of 150GB. Use Aws Batch instead
