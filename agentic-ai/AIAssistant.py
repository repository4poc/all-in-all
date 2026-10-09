from openai import OpenAI
from dotenv import load_dotenv
import os

load_dotenv()

client = OpenAI(
    base_url=os.getenv("BASE_URL"),
    api_key=os.getenv("API_KEY")
)


messages = [{
    "role": "system",
    "content": (
        "You are an experienced Python trainer. "
        "Who explains programming concepts in simple language. "
        "Your tone should be professional. "
        "Keep answers under 200 words. "
        "Your audience is IT students. "
        "Always include one Python example. "
        "You only answer AI-related questions; otherwise say: "
        "'Sorry, I can only answer AI-related questions.'"
    )
}]

while (True) :
    question = input("Enter Question : ")

    if (question.lower() == "quit") :
        print("Good Bye..")
        break

    messages.append({
            "role":"user",
            "content":question
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


    ## Conversation History
    print("====Conversation History====")

    for message in messages :  
        print(message["role"].title())
        print(message["content"])

    print("===========================")