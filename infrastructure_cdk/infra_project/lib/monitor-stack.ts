import * as cdk from "aws-cdk-lib";
import { NodejsFunction } from "aws-cdk-lib/aws-lambda-nodejs";
import { Construct } from "constructs";

// Define the MonitorStack class that extends cdk.Stack
export class MonitorStack extends cdk.Stack {
  // Define the constructor for the MonitorStack class
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    // Create a Lambda function for the Slack webhook handler
    const webhookLambda = new NodejsFunction(this, "WebhookLambda", {
      entry: "lib/lambda-handler/slack-webhook-handler.ts",
      handler: "handler",
    });

    // Create an SNS topic for the alarm notifications
    const alarmTopic = new cdk.aws_sns.Topic(this, "AlarmTopic", {
      displayName: "Alarm Topic",
      topicName: "alarm-topic",
    });
    alarmTopic.addSubscription(
      new cdk.aws_sns_subscriptions.LambdaSubscription(webhookLambda),
    );

    const apigateway4XXAlarm = new cdk.aws_cloudwatch.Alarm(
      this,
      "APIGateway4XXAlarm",
      {
        metric: new cdk.aws_cloudwatch.Metric({
          namespace: "AWS/ApiGateway",
          metricName: "4XXError",
          period: cdk.Duration.minutes(1),
          unit: cdk.aws_cloudwatch.Unit.COUNT,
          dimensionsMap: {
            ApiName: "api-gateway",
          },
        }),
        threshold: 1,
        evaluationPeriods: 1,
        alarmName: "APIGateway4XXAlarm",
      },
    );

    // Create an SNS action for the alarm and add it to the alarm's actions
    const topicAction = new cdk.aws_cloudwatch_actions.SnsAction(alarmTopic);
    apigateway4XXAlarm.addAlarmAction(topicAction);
    apigateway4XXAlarm.addOkAction(topicAction);
  }
}
