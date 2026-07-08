import { Construct } from "constructs/lib/construct";
import { Stage, StageProps } from "aws-cdk-lib";
import { DynamoDbStack } from "./dynamodb-stack";

export class PipelineStage extends Stage {
  constructor(scope: Construct, id: string, props: StageProps) {
    super(scope, id, props);

    // Create an instance of the DynamoDbStack and specify the AWS account and region
    new DynamoDbStack(this, "DynamoDbStack", {
      env: { account: "127112147701", region: "eu-north-1" },
      stageName: props?.stageName,
    });
  }
}
