from openai import OpenAI
from dotenv import load_dotenv
import os

load_dotenv()

def build_tool_description(tools):
    description = ""

    for tool in tools:
        description += f""""
                        Tool:{tool.name}
                        Description: {tool.description}
                        """
    return description

def planner(state,tools):

    tools_description = build_tool_description(tools)

    prompt= f"""
                    You are an AI planner.
                    Your job is to decide ONLY the next task and the best tool to complete it.
                    User Request:
                    {state["user_request"]}
                    Tasks:
                    {state["tasks"]}
                    Completed Actions:
                    {state["actions"]}
                    Previous Observations:
                    {state["observations"]}
                    Intermediary Results:
                    {state["results"]}
                    Available Tools:
                    {tools_description}
                    Instructions:
                    1. Choose only ONE pending task.
                    2. Select the best available action for that helps complete a pending task.
                    3. Never repeat a completed action unless necessary.
                    4. Use previous observations and intermediate results
                    5. If an action failed, reconsider the current plan.
                    6. If all tasks are completed, return FINISH
                    7. Return exactly:
                    TASK: <task>
                    TOOL: <tool>
                    8. Do not explain your answer.
                    Next Decision:
                    """

    client = OpenAI(
        base_url=os.getenv("BASE_URL"),
        api_key=os.getenv("API_KEY")
    )


    messages = [{
        "role": "system",
        "content": "You are an AI Planner."
    }]


    messages.append({
            "role":"user",
            "content":prompt
        })

    response = client.chat.completions.create(
        model=os.getenv("MODEL"),
        messages=messages
    )    

    return response.choices[0].message.content