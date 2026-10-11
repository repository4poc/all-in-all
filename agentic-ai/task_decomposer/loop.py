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

    #4. Start the loop
    while state["current_step"] < state["max_steps"]:

        print("Step:", state["current_step"] + 1)

        if all_tasks_completed(state):
            answer = format_answer(state)
            finish(state,answer)
            print("Update State - After finish", state)
            break        

        # Ask the planner for the next action
        # We are not storing all the tools in the state, rather planner will decide which tools to
        # use based on the user_request.
        decision = planner(state,tools)
        # decsion = {TASK: "***", TOOL:"***"}
        print("Planner selected: ", decision)

        # Check whether the goal is completed
        if decision.strip().upper() == "FINISH":
            answer = format_answer(state)
            finish(state,answer)
            print("Update State - After finish", state)
            break

        # Extract task and tool
        task, tool = parse_decision(decision)

        if tool == 'none':
            answer = format_answer(state)
            complete_task(state,task)
            print("Update State - After finish", state)
            next_step(state)
            continue            

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