import asyncio
from mcp_client import connect, discover_tools, execute_tool


async def main():
    client = await connect()

    async with client:
        print("Connected to MCP server")

        tools = await discover_tools(client)

        for tool in tools:
            print(tool.name, "-", tool.description)

        result = await execute_tool(client, "current_time")
        print("Current time:", result.content[0].text)

        result = await execute_tool(
            client,
            "generate_password",
            {"length": 20}
        )
        print("Password result:", result.content[0].text)


if __name__ == "__main__":
    asyncio.run(main())

