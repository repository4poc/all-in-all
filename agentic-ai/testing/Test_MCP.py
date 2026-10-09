import asyncio
from mcp_client import connect, discover_tools, execute_tool


async def main():
    client = await connect()

    async with client:

        tools = await discover_tools(client)

        for tool in tools:
            print("Tool Name : ", tool.name, ", Tool Description : ", tool.description)

        result = await execute_tool(client, tool.name)
        print("Current time:", result.content[0].text)


if __name__ == "__main__":
    asyncio.run(main())

