import * as cdk from "aws-cdk-lib";
import {
  CodePipeline,
  CodePipelineSource,
  ShellStep,
} from "aws-cdk-lib/pipelines";
import { Construct } from "constructs";

// Define the AwsCICDStack class that extends cdk.Stack
export class AwsCICDStack extends cdk.Stack {
  // Define the constructor for the AwsCICDStack class
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    new CodePipeline(this, "MyPipeline", {
      pipelineName: "MyPipeline",
      synth: new ShellStep("Synth", {
        input: CodePipelineSource.gitHub("repository4poc/all-in-all", "main"),
        commands: ["npm ci", "npm run build", "npx cdk synth"],
        primaryOutputDirectory: "infrastructure_cdk/infra_project/cdk.out",
      }),
    });
  }
}
