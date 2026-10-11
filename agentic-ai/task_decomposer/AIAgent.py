
import asyncio
from mcp_client import connect, discover_tools
from planner import planner
from openai import OpenAI
from dotenv import load_dotenv
from executor import execute_action
from loop import run_agent_loop
from formatter import format_answer


async def main():

    load_dotenv()

    #1. Connect to MCP Client
    client = await connect()

    async with client:
        
        #2. Discover MCP tools
        tools = await discover_tools(client)

        print("----Loading MCP Tools---")
        for tool in tools:
            print(tool.name, tool.description)
        print("------------------------")

        #3. User Input
        user_request = input("Enter Question : ")

        #3. Run Agent loop
        state = await run_agent_loop(
            user_request,
            planner,
            execute_action,
            format_answer,
            tools,
            client
        )

        print("Final Answer : ",state["final_answer"])

if __name__ == "__main__":
    asyncio.run(main())