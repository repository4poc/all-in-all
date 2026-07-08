import * as cdk from "aws-cdk-lib";
import { Construct } from "constructs";
import * as dynamodb from "aws-cdk-lib/aws-dynamodb";

interface DynamoDbStackProps extends cdk.StackProps {
  stageName?: string;
}

export class DynamoDbStack extends cdk.Stack {
  // Define a public readonly property for the DynamoDB table
  public readonly table: dynamodb.ITable;

  // Define the constructor for the DynamoDbStack class
  constructor(scope: Construct, id: string, props?: DynamoDbStackProps) {
    super(scope, id, props);

    // Create a DynamoDB table with the specified properties
    const table = new dynamodb.TableV2(this, "Order", {
      tableName: "Order",
      partitionKey: { name: "OrderId", type: dynamodb.AttributeType.NUMBER },
      removalPolicy: cdk.RemovalPolicy.DESTROY,
    });

    // Assign the created table to the public readonly property
    this.table = table;
  }
}
