from openai import OpenAI
from dotenv import load_dotenv
import os


def format_answer(state):

    prompt = f"""
            The user asked:
            {state["user_request"]}

            Tasks:
            {state["tasks"]}            

            Actions performed:
            {state["actions"]}

            Observations:
            {state["observations"]}

            Intermediate Results:
            {state["results"]}            

            Give the user a natural and consistant answer.

            Do not mention
            - internal planning.
            - internal state
            - MCP
            - Tools
            - internal reasoning
            """

    load_dotenv()

    client = OpenAI(
        base_url=os.getenv("BASE_URL"),
        api_key=os.getenv("API_KEY")
    )    

    response = client.chat.completions.create(
    model=os.getenv("MODEL"),
    messages=[
        {
            "role": "user",
            "content": prompt
        }
    ]
    )
    return response.choices[0].message.content.strip()