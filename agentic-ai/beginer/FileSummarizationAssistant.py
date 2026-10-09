from openai import OpenAI
from dotenv import load_dotenv
import os
from Tools import read_text_file

load_dotenv()

client = OpenAI(
    base_url=os.getenv("BASE_URL"),
    api_key=os.getenv("API_KEY")
)


messages = [{
    "role": "system",
    "content": (
        "You are an experienced summarization expert"
        "Who summarized the content provided. "
        "Your tone should be professional. "
        "Keep answers under 200 words. "
        "Your audience is students. "
        "You only answer summarization-related questions; otherwise say: "
        "'Sorry, I can only answer summarization-related questions.'"

    )
}]

while (True) :
    question = input("Enter Question : ")

    if (question.lower() == "quit") :
        print("Good Bye..")
        break

    messages.append({
            "role":"user",
            "content":question + read_text_file("data/Notes.txt")
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