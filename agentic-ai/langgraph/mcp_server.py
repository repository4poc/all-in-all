from datetime import datetime
from fastmcp import FastMCP
import secrets
import string
from random import randint


mcpserver = FastMCP("Time Server")


@mcpserver.tool()
def current_time():
    """Return the current date and time."""
    return datetime.now().strftime("%d-%m-%Y %H:%M:%S %p")

@mcpserver.tool()
def generate_password(length=16):
    """Return the secured password."""
    characters = string.ascii_letters + string.digits + string.punctuation
    return ''.join(secrets.choice(characters) for _ in range(length))

@mcpserver.tool()
def get_random():
    """Return random number between 1 and 6 inclusive both"""
    return randint(1,6)

if __name__ == "__main__":
    mcpserver.run()