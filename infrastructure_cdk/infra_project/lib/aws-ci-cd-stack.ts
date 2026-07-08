import * as cdk from "aws-cdk-lib";
import {
  CodePipeline,
  CodePipelineSource,
  ShellStep,
} from "aws-cdk-lib/pipelines";
import { Construct } from "constructs";

export class AwsCICDStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    new CodePipeline(this, "MyPipeline", {
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
          "npx cdk synth",
        ],
        primaryOutputDirectory: "infrastructure_cdk/infra_project/cdk.out",
      }),
    });
  }
}
