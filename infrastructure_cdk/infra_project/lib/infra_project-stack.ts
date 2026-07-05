import * as cdk from "aws-cdk-lib/core";
import { Construct } from "constructs";
import * as sqs from "aws-cdk-lib/aws-sqs";
import { Fn } from "aws-cdk-lib/core";

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

  // import { Fn } from "aws-cdk-lib/core";
  private getStackName() {
    const stackId = this.stackId;
    // Stack ID
    // arn:aws:cloudformation:eu-north-1:127112147701:stack/InfraProjectStack/c0c271d0-7888-11f1-80c8-0e9dba8833f7
    const stackFCDN = Fn.select(5, Fn.split(":", stackId));
    // stack/InfraProjectStack/c0c271d0-7888-11f1-80c8-0e9dba8833f7
    const stackName = Fn.select(1, Fn.split("/", stackFCDN));
  }
}
