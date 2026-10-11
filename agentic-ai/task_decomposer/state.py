def create_state(user_request,max_steps=5):
    return{
        "user_request": user_request,

        "tasks": [],
        "current_task": None,
        "results": [],
        "current_step":0,
        "max_steps":max_steps,
        "actions":[],
        "observations":[],
        "finished":False,
        "final_answer":""
    }

def add_action(state,action):
    state["actions"].append(action)

def record_observation(state,action,observation):
    state["observations"].append(
        {
            "steps":state["current_step"],
            "action":action,
            "observation":observation
        }
    )

def next_step(state):
    state["current_step"] =  state["current_step"] + 1 


def can_continue(state):
    return(
        not state["finished"]
        and
        state["current_steps"] < state["max_steps"]
    )

def finish(state,answer):
    state["finished"]=True
    state["final_answer"]=answer    

def add_task(state,task):
    state["tasks"].append({
        "task":task,
        "state":"pending"
    })

def complete_task(state,task):
    for item in state["tasks"]:
        if item["task"] == task:
            item["state"] = "completed"
    return   

def add_result(state,result):
    state["results"].append(result)

def get_pending_tasks(state):

    return [
    item["task"]
    for item in state["tasks"]
    if item["state"] == "pending"
    ]

def all_tasks_completed(state):
    return all(
        task["state"] == "completed"
        for task in state["tasks"]
    )