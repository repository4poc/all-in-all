import * as cdk from "aws-cdk-lib";
import { Construct } from "constructs";
import * as lambda from "aws-cdk-lib/aws-lambda";
import * as path from "path";
import * as apigateway from "aws-cdk-lib/aws-apigateway";
import { NodejsFunction } from "aws-cdk-lib/aws-lambda-nodejs";

// Define an interface for the stack properties that includes the DynamoDB table
interface LambdaStackProps extends cdk.StackProps {
  table: cdk.aws_dynamodb.ITable;
}

// Define the LambdaStack class that extends cdk.Stack
export class LambdaStack extends cdk.Stack {
  // Define a public readonly property for the Lambda integration
  public readonly sayHelloLambdaIntegration: apigateway.LambdaIntegration;

  // Define the constructor for the LambdaStack class
  constructor(scope: Construct, id: string, props?: LambdaStackProps) {
    super(scope, id, props);

    // Create a Lambda function that will be integrated with the API Gateway
    const sayHelloLambda = new NodejsFunction(this, "SayHelloLambda", {
      runtime: lambda.Runtime.NODEJS_20_X,
      handler: "handler",
      entry: path.join(__dirname, "lambda-handler", "lambda-handler1.ts"),
      environment: {
        TABLE_NAME: props?.table.tableName || "",
      },
    });

    // Create a Lambda integration for the API Gateway using the created Lambda function
    this.sayHelloLambdaIntegration = new apigateway.LambdaIntegration(
      sayHelloLambda,
    );
  }
}
