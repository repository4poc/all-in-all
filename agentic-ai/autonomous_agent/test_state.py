from state import *

question="What is the current time and secured password"

state = create_state(question,5)

print("----Initial State----")

print(state)

add_action(state,"generate_password")
add_observation(state,"generate_password","&jdg738728")

print("----Updated State----")

print(state)

next_step(state)

print("----Updated State----")


print(state)

print("----Updated State----")

finish(state,"33wyuw26^&&&\n10/10/2026 00:15 AM")

print(state)
