import * as cdk from "aws-cdk-lib";
import {
  CodePipeline,
  CodePipelineSource,
  ShellStep,
} from "aws-cdk-lib/pipelines";
import { Construct } from "constructs";
import { PipelineStage } from "./pipeline-stage";

export class AwsCICDStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const pipeline = new CodePipeline(this, "MyPipeline", {
      pipelineName: "MyPipeline",
      synth: new ShellStep("Synth", {
        input: CodePipelineSource.gitHub("repository4poc/all-in-all", "main", {
          authentication: cdk.SecretValue.secretsManager(
            "github_token_aws_cdk",
          ),
        }),
        commands: [
          "cd infrastructure_cdk/infra_project",
          "npm install",
          "npm run build",
          "npx aws-cdk@latest synth",
        ],
        primaryOutputDirectory: "infrastructure_cdk/infra_project/cdk.out",
      }),
    });

    pipeline.addStage(new PipelineStage(this, "Dev", { stageName: "Dev" }));
  }
}
