from datetime import datetime
from random import randint
import secrets
import string
from pathlib import Path
import numpy as np

## Current datetime
def get_current_time():
    result = datetime.now().strftime("%d-%m-%Y %H:%M:%S %p")
    return result

## random number between 1 and 6 inclusive both
def get_random():
    return randint(1,6)

## Generate password
def generate_password(length=16):
    characters = string.ascii_letters + string.digits + string.punctuation
    return ''.join(secrets.choice(characters) for _ in range(length))

## Read a text file
## with ensure the file is closed after read automatically
## read() read the entire file and store it in a single string.
def read_text_file(filename):
    try:
        with open(filename,"r") as file:
            content = file.read()
            return content
    except FileNotFoundError:
        return "Error: File not found..."


def load_documents(path):
    documents = {}
    folder = Path(path)

    for file in  folder.glob("*.txt"):
        documents[file.name] = file.read_text(
            encoding="utf-8"
        )

    return documents

def cosine_similarity(vector1, vector2):
    v1 = np.array(vector1)
    v2 = np.array(vector2)
    return np.dot(v1, v2) / (np.linalg.norm(v1) * np.linalg.norm(v2))

