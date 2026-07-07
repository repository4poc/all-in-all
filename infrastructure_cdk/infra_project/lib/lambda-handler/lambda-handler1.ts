import {
  APIGatewayProxyEvent,
  APIGatewayProxyResult,
  Context,
} from "aws-lambda";
import { v4 as uuidv4 } from "uuid";
import { S3Client } from "@aws-sdk/client-s3";
import { ListBucketsCommand } from "@aws-sdk/client-s3";

const s3Client = new S3Client({ region: "us-east-1" });

export async function handler(
  event: APIGatewayProxyEvent,
  context: Context,
): Promise<APIGatewayProxyResult> {
  console.log("Received event:", JSON.stringify(event, null, 2));

  const method: string = event.httpMethod;

  const command = new ListBucketsCommand({});
  try {
    const data = await s3Client.send(command);

    switch (method) {
      case "GET":
        console.log("GET method called");
        break;
      case "POST":
        console.log("POST method called");
        break;
      default:
        console.log(`Unsupported method: ${method}`);
        return {
          statusCode: 405,
          body: JSON.stringify({
            message: `Unsupported method: ${method}`,
          }),
        };
    }

    console.log("Success Bucket : ", JSON.stringify(data.Buckets));

    return {
      statusCode: 200,
      body: JSON.stringify({
        message: `The Lambda read the DynamoDB table! ${process.env.TABLE_NAME} with UUID: ${uuidv4()}`,
      }),
    };
  } catch (err) {
    console.log("Error", err);
    return {
      statusCode: 500,
      body: JSON.stringify({
        message: "An error occurred while processing the request.",
      }),
    };
  }
}
