
import asyncio
from mcp_client import connect, discover_tools,execute_tool
from Planner import planner
from openai import OpenAI
from dotenv import load_dotenv
import os


async def main():

    load_dotenv()

    client = await connect()

    async with client:
        # Discover tools only after connecting
        tools = await discover_tools(client)

        for tool in tools:
            print(tool.name, tool.description)

        question = input("Enter Question : ")


        tool_name = planner(question,tools)

        print("Tool Name Chosen : ", tool_name)

        result = await execute_tool(client, tool_name)
        print("Result :", result.content[0].text)

        client = OpenAI(
            base_url=os.getenv("BASE_URL"),
            api_key=os.getenv("API_KEY")
        )


        messages = [{
            "role": "system",
            "content": (
                "You are an AI Agent who call functions and presend the out of the function to user in a natural way "
            )
        }]            

        messages.append({
                "role":"user",
                "content":f"""
                            The user asked: {question}
                            The result is : {result}
                            Answer the user naturally.
                            """
            })

        response = client.chat.completions.create(
            model=os.getenv("MODEL"),
            messages=messages
        )    

        print(response.choices[0].message.content)        

if __name__ == "__main__":
    asyncio.run(main())