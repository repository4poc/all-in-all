
import asyncio
from mcp_client import connect, discover_tools,execute_tool
from planner import planner
from openai import OpenAI
from dotenv import load_dotenv
from mcp_executor import execute_action
from loop import run_agent_loop
import os


def format_answer(state):

    load_dotenv()

    prompt = f"""
            The user asked:
            {state["user_request"]}

            Actions performed:
            {state["actions"]}

            Observations
            {state["observations"]}

            Answer the user naturally.

            Do not mention internal planning.
            Do not mention state.
            Do not mention tools. 
            """

    print(prompt)


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

async def main():

    load_dotenv()

    client = await connect()

    async with client:
        # Discover tools only after connecting
        tools = await discover_tools(client)

        print("----Available Tools---")
        for tool in tools:
            print(tool.name, tool.description)

        question = input("Enter Question : ")

        state = await run_agent_loop(
            question,
            planner,
            execute_action,
            format_answer,
            tools,
            client
        )

        print("Final Answer:")   
        print(state["final_answer"])

if __name__ == "__main__":
    asyncio.run(main())