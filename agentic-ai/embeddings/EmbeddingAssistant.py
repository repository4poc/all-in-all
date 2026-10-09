from openai import OpenAI
from dotenv import load_dotenv
import os

from Tools import cosine_similarity

load_dotenv()

client = OpenAI(
    base_url=os.getenv("BASE_URL"),
    api_key=os.getenv("API_KEY")
)



embeddings1 = client.embeddings.create(
        model=os.getenv('EMBEDDING_MODEL'),
        input="What is Java"

)    

print(embeddings1.data[0].embedding)

embeddings2 = client.embeddings.create(
        model=os.getenv('EMBEDDING_MODEL'),
        input="Java is a programming language"

)    

print(embeddings2.data[0].embedding)

print ("-"*20)
print(cosine_similarity(embeddings1.data[0].embedding,embeddings2.data[0].embedding))
print ("-"*20)
