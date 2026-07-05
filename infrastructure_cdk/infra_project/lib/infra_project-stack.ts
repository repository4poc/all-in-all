import * as cdk from "aws-cdk-lib/core";
import { Construct } from "constructs";
import * as sqs from "aws-cdk-lib/aws-sqs";

export class InfraProjectStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    // Define SQS resource
    const queue = new sqs.Queue(this, "InfraProjectQueue", {
      visibilityTimeout: cdk.Duration.seconds(300),
    });
  }
}
