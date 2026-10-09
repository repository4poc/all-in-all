from openai import OpenAI
from dotenv import load_dotenv
import os
from mcp_client import connect, discover_tools, execute_tool

load_dotenv()

def build_tool_description(tools):
    description = ""

    for tool in tools:
        description += f""""
                        Tool:{tool.name}
                        Description: {tool.description}
                        """
    return description

def planner(question,tools):

    print("-"*20)
    for tool in tools:
        print(tool.name, tool.description)
    print("-"*20)


    tools_description = build_tool_description(tools)

    print("tools_description",tools_description)

    planner_prompt= f"""
                    You are an AI planner.
                    You return the tool to use based on the {question}.
                    Available tools {tools_description}
                    Instructions
                    1. Select the best tool.
                    2. Reply only with the tool name.
                    3. Do not explain
                    4. If no tool is requried, return none
                    """

    client = OpenAI(
        base_url=os.getenv("BASE_URL"),
        api_key=os.getenv("API_KEY")
    )


    messages = [{
        "role": "system",
        "content": (
            planner_prompt
        )
    }]


    messages.append({
            "role":"user",
            "content":question
        })

    response = client.chat.completions.create(
        model=os.getenv("MODEL"),
        messages=messages
    )    

    return response.choices[0].message.content