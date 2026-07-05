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

    new cdk.CfnOutput(this, "QueueName", {
      value: queue.queueName,
      description: "The name of the SQS queue",
    });

    const queueNameParameter = new cdk.CfnParameter(this, "duration", {
      type: "Number",
      default: 120,
      minValue: 60,
      maxValue: 10 * 60,
      description:
        "The duration in seconds for the SQS queue visibility timeout",
    });

    new cdk.CfnOutput(this, "durationValue", {
      value: queueNameParameter.valueAsString,
      description:
        "The duration in seconds for the SQS queue visibility timeout",
    });
  }
}
