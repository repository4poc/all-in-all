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
        "You are an QA Agent"
        "You are given a document content"
        "You Answers the user's question using only the information present in the document given to you"
        "Your tone should be professional. "
        "Keep answers under 200 words. "
        "Your audience is students. "
        "If the answer is not available, say: "
        "'Sorry, I can not answer the question.'"

    )
}]

while (True) :
    question = input("Enter Question : ")

    if (question.lower() == "quit") :
        print("Good Bye..")
        break

    messages.append({
            "role":"user",
            "content":question + read_text_file("data/Project.txt")
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
