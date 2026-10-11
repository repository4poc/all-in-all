
import sys
from pathlib import Path
from fastmcp import Client
from fastmcp.client.transports import StdioTransport

# 1. connect
async def connect():
    server_path = (
        Path(__file__).resolve().parent
        / "mcp_server.py"
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

# 2. discover_tools
async def discover_tools(client):
    return await client.list_tools()

# 3. execute_tool
async def execute_tool(client, tool_name, arguments=None):
    return await client.call_tool(tool_name, arguments or {})

# 4. disconnect
async def disconnect(client):
    await client.__exist__(None,None,None)