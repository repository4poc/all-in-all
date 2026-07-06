import * as cdk from "aws-cdk-lib";
import { Construct } from "constructs";
import * as apigateway from "aws-cdk-lib/aws-apigateway";

// Define an interface for the stack properties that includes the Lambda integration
interface ApiGatewayStackProps extends cdk.StackProps {
  lambdaIntegration: apigateway.LambdaIntegration;
}

// Define the ApiGatewayStack class that extends cdk.Stack
export class ApiGatewayStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props: ApiGatewayStackProps) {
    super(scope, id, props);

    // Create an API Gateway REST API
    const api = new apigateway.RestApi(this, "api-gateway");

    // Define the /sayhello resource and integrate it with the Lambda function
    const sayHello = api.root.addResource("sayhello");
    sayHello.addMethod("GET", props.lambdaIntegration);
  }
}
