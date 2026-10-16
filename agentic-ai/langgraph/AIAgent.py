
import asyncio
from mcp_client import connect, discover_tools,disconnect

from graph import build_graph

from state import AgentState



async def main():
    
    client = await connect()

    async with client:    

        tools = await discover_tools(client)

        print("\nAvailable Tools")
        print("----------------")

        for tool in tools:
            print(tool.name)
            print(tool.description)

        graph = build_graph(tools)

        user_request = input("\nUser : ")

        initial_state = {
            "user_request": user_request,
            "current_step": 0,
            "max_steps": 5,
            "actions": [],
            "observations": [],
            "finished": False,
            "final_answer": ""
        }

        result = graph.invoke(
            initial_state
        )

        print("\nFinal State")
        print("-----------")
        print(result)

if __name__ == "__main__":
    asyncio.run(main())