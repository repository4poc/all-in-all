import { Context, SNSEvent } from "aws-lambda";
import dotenv from "dotenv";
import path from "path";

// Load environment variables from .env file
dotenv.config({
  path: path.resolve(__dirname, "../../.env"),
});

try {
  if (!process.env.SLACK_WEBHOOK_URL) {
    throw new Error(
      "SLACK_WEBHOOK_URL is not defined in the environment variables.",
    );
  }
} catch (error) {
  console.error("Error loading environment variables:", error);
  process.exit(1); // Exit the process with an error code
}
const webhookUrl = "UPDATE THIS URL FROM .ENV FILE";

async function handler(event: SNSEvent, context: Context) {
  for (const record of event.Records) {
    await fetch(webhookUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        text: `Received SNS message: ${record.Sns.Message}`,
      }),
    });
  }
}

export { handler };
