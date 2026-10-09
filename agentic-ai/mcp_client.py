
import sys
from pathlib import Path
from fastmcp import Client
from fastmcp.client.transports import StdioTransport


async def connect():
    server_path = (
        Path(__file__).resolve().parent
        / "date_mcp_server"
        / "server.py"
    )

    if not server_path.is_file():
        raise FileNotFoundError(f"Server not found: {server_path}")

    transport = StdioTransport(
        command=sys.executable,
        args=["-u", str(server_path)],
        log_file=server_path.parent / "server.log",
        keep_alive=False,
    )

    return Client(transport)


async def discover_tools(client):
    return await client.list_tools()


async def execute_tool(client, tool_name, arguments=None):
    return await client.call_tool(tool_name, arguments or {})