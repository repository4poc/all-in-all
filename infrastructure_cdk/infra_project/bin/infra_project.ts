#!/usr/bin/env node
import * as cdk from "aws-cdk-lib";
import { InfraProjectStack } from "../lib/infra_project-stack";
import { AwsSolutionsChecks } from "cdk-nag";
import { App, Validations } from "aws-cdk-lib";
import { LambdaStack } from "../lib/lambda-stack";
import { ApiGatewayStack } from "../lib/api-gateway-stack";
import { DynamoDbStack } from "../lib/dynamodb-stack";
import { MonitorStack } from "../lib/monitor-stack";
import { AwsCICDStack } from "../lib/aws-ci-cd-stack";

const app = new cdk.App();
/*
// Create an instance of the InfraProjectStack and specify the AWS account and region
new InfraProjectStack(app, "InfraProjectStack", {
  env: { account: "127112147701", region: "eu-north-1" },
});

// Create an instance of the DynamoDbStack and specify the AWS account and region
const dynamoDbStack = new DynamoDbStack(app, "DynamoDbStack", {
  env: { account: "127112147701", region: "eu-north-1" },
});

// Create an instance of the LambdaStack and specify the AWS account and region
const lambdaStack = new LambdaStack(app, "LambdaStack", {
  env: { account: "127112147701", region: "eu-north-1" },
  table: dynamoDbStack.table,
});

// Create an instance of the ApiGatewayStack and pass the Lambda integration from the LambdaStack
new ApiGatewayStack(app, "ApiGatewayStack", {
  env: { account: "127112147701", region: "eu-north-1" },
  lambdaIntegration: lambdaStack.sayHelloLambdaIntegration,
});

new MonitorStack(app, "MonitorStack", {
  env: { account: "127112147701", region: "eu-north-1" },
});
*/
new AwsCICDStack(app, "AwsCICDStack", {
  env: { account: "127112147701", region: "eu-north-1" },
});

app.synth();
/*
// Enable cdk-nag checks for best practices and security compliance
 
Validations.of(app).addPlugins(new AwsSolutionsChecks(app, { verbose: true }));
*/
