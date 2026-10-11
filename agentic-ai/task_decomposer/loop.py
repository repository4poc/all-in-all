from state import (
    create_state,
    add_task,
    add_action,
    record_observation,
    add_result,
    complete_task,
    next_step,
    can_continue,
    finish,
    all_tasks_completed
)

from decomposer import decompose_task


def parse_decision(decision):
    task = None
    tool = None
    
    for line in decision.splitlines():
        line = line.strip()

        if line.startswith("TASK:"):
            task = line[5:].strip()
        elif line.startswith("TOOL:"):
            tool = line[5:].strip()

    return task,tool

async def run_agent_loop(
        user_request, planner, execute_action, format_answer,tools, client
):
    print("------------------------run_agent_loop-------------------------------")
    #1. Create agent state
    state = create_state(user_request)
    print("Initial State",state)

    #2. Break the user's request into tasks
    planned_tasks = decompose_task(user_request)
    print("planned tasks",planned_tasks)


    #3. Store the tasks in state
    for task in planned_tasks:
        add_task(state,task)  

    print("Update State With Tasks ",state)

    #4. Start the agent loop
    while state["current_step"] < state["max_steps"]:

        if all_tasks_completed(state):

            print(
                "\n--- Step "
                f"{state['current_step'] + 1} ---"
            )

            answer = format_answer(
                state
            )

            finish(
                state,
                answer
            )

            break        

        print(
            f"--Step {state['current_step']+1}--"
        )

        # Ask the planner for the next action
        decision = planner(state,tools)
        print("Planner selected: ", decision)

        # Check whether the goal is completed
        if decision.strip().upper() == "FINISH":
            answer = format_answer(state)
            finish(state,answer)
            break

        # Extract task and tool
        task, tool = parse_decision(decision)

        print("Task: ",task, "Tool: ",tool)

        if not task or not tool:
            add_result(
                state, "Invalid planner decision"
            )
            next_step(state)
            continue

        state["current_task"] = task

        # Execute the selected tool
        observation = await execute_action(tool,state,client)
        print("Observation", observation)

        # Update state      
        add_action(state,tool)
        record_observation(state,tool,observation)

        # Save observation as intermediate result
        add_result(state, observation)

        print("Update State", state)


        # Mark selected task complete
        if not observation.lower().startswith("error"):
            print("Task Completed: ", task)
            complete_task(state,task)
            print("Update State", state)
        else:
            add_result(state, "The previous action failed. The planner must reconsider")

        print("Update State", state)

        next_step(state)

    return state