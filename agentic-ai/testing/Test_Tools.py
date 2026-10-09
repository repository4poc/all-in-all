from Tools import (
    get_current_time,
    get_random,
    generate_password,
    read_text_file,
    load_documents
)

from AIPlanner import choose_tool

print("get_current_time : "+ get_current_time())

print(get_random())

print(generate_password())

print(read_text_file("data/Notes.txt"))
print(read_text_file("data/ShoppingList.txt"))

documents = load_documents("data/knowledge")
for name,text in documents.items():
    print("-"*20)    
    print(name)
    print(text)
    print("-"*20)


print(choose_tool("What is the current location of moon"))