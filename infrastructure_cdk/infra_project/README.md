## Project Overview

This is a AWS CDK based project that deploy the AWS resources as IaC using TypeScript.

The `cdk.json` file tells the CDK Toolkit how to execute your app.

![alt text](images/{5A010B16-D361-4AD0-B4DA-192E9E2DF751}.png)

## CDK (Cloud Development Kit)

- Cloud Formation is an IaC Enginer in AWS
- It is an absraction over Cloud Formation
- An IaC solution Provided by AWS
- AWS Cloud Formation, organizes and manage resources into groups called 'Stac'

![alt text](images/{5257665C-E3D7-4450-BB62-652779D2CF6D}.png)

## Useful commands

- `cdk bootstrap` CloudFormation stack setup
- `cdk diff` compare deployed stack with current state
- `cdk synth` emits the synthesized CloudFormation template into `cdk.out` folder
- `cdk deploy` deploy this stack to your default AWS account/region

- `npm run build` compile typescript to js
- `npm run watch` watch for changes and compile
- `npm run test` perform the jest unit tests

![alt text](images/{8B004D73-9C39-448C-B4F6-3C27AF1F0497}.png)
![alt text](images/{C4C39C1A-62EC-4BA3-ACB2-61005F5EB009}.png)

To Manually run Test

```
 ts-node test/slack-webhook-handler.test.ts
```

![alt text](images/{29FF1AFC-B540-47A3-AB2D-EDBB45D0CEA8}.png)
![alt text](images/{94039DEF-209F-4AA3-8EBB-7E1F9697F7B4}.png)
