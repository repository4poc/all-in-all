from openai import OpenAI
from dotenv import load_dotenv
import os

load_dotenv()

def choose_tool(question):
    planner_prompt= f"""
                    You are an AI planner.
                    You return the tool to use based on the question.
                    Available tools
                    1. get_current_time
                        Use when the user ask for the current date and time
                    2. generate_password
                        Use when the user ask for a secure password
                    If no tool is requried, return 
                    none

                    Return only the tool name
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