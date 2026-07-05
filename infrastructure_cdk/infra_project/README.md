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
