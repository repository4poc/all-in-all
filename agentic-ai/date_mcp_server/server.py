from datetime import datetime
from fastmcp import FastMCP
import sys
import secrets
import string

mcpserver = FastMCP("Time Server")


@mcpserver.tool()
def current_time():
    """Return the current date and time."""
    return datetime.now().strftime("%d-%m-%Y %H:%M:%S %p")


@mcpserver.tool()
def generate_password(length: int = 16) -> str:
    """Generate a secure random password."""
    if length < 1:
        raise ValueError("Password length must be at least 1")

    characters = string.ascii_letters + string.digits + string.punctuation
    return ''.join(secrets.choice(characters) for _ in range(length))


if __name__ == "__main__":
    print("Starting MCP Server", file=sys.stderr)
    mcpserver.run(transport="stdio")