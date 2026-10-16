from langgraph.graph import (
    StateGraph,
    START,
    END
)
from planner import planner
from state import AgentState

def build_graph(tools):
    builder = StateGraph(AgentState)
    planner_node = create_planner_node(tools)
    tool_node = create_tool_node(tools)

    builder.add_node(
        "planner",
        planner_node
    )

    builder.add_edge(
        START,
        "planner"
    )

    builder.add_conditional_edges(
        "planner",
        route_after_planner,
        {
            "tool":"tool",
            "finish": END
        }
    )

    
    builder.add_node(
        "tool",
        tool_node
    )

    return builder.compile()



def create_planner_node(tools):

    def planner_node(state: AgentState):
        action = planner(
            state,
            tools
        )

        print("\nPlanner Selected:")
        print(action)

        return {
            "actions": state["actions"],
            "action": action,
            "observations": state["observations"],
            "current_step": state["current_step"],
            "finished": action == "FINISH",
            "final_answer": state["final_answer"]
        }

    return planner_node

def route_after_planner(state: AgentState):
    if state["action"] == "FINISH":
        return "finish"
    return "tool"

def create_tool_node(tools):
    def tool_node(state: AgentState):
        
        action = planner(
            state,
            tools
        )

        print("\nPlanner Selected:")
        print(action)

        return {
            "actions": state["actions"],
            "action": action,
            "observations": state["observations"],
            "current_step": state["current_step"],
            "finished": action == "FINISH",
            "final_answer": state["final_answer"]
        }

    return tool_node    