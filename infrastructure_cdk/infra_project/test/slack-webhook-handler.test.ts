import { handler } from "../lib/lambda-handler/slack-webhook-handler";

const snsEvent = {
  Records: [
    {
      Sns: {
        Message: "Test message from SNS",
      },
    },
  ],
};

handler(snsEvent as any, {} as any);
