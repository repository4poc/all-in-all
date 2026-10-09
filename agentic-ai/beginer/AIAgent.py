from openai import OpenAI
from dotenv import load_dotenv
import os
from AIPlanner import choose_tool
from Tools import get_current_time


load_dotenv()

client = OpenAI(
    base_url=os.getenv("BASE_URL"),
    api_key=os.getenv("API_KEY")
)


messages = [{
    "role": "system",
    "content": (
        "You are an AI Agent who call functions and presend the out of the function to user in a natural way "
    )
}]

while (True) :
    question = input("Enter Question : ")

    if (question.lower() == "quit") :
        print("Good Bye..")
        break

    tool = choose_tool(question)
    if tool == "get_current_time":
        result = get_current_time()

    messages.append({
            "role":"user",
            "content":f"""
                        The user asked: {question}
                        The result is : {result}
                        Answer the user naturally.
                        """
        })

    response = client.chat.completions.create(
        model=os.getenv("MODEL"),
        messages=messages
    )    

    print(response.choices[0].message.content)

    messages.append({
            "role":"assistant",
            "content":response.choices[0].message.content
        })