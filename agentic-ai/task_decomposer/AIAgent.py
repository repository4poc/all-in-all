
import asyncio
from mcp_client import connect, discover_tools
from planner import planner
from openai import OpenAI
from dotenv import load_dotenv
from executor import execute_action
from loop import run_agent_loop
from formatter import format_answer


async def main():

    load_dotenv()

    #1. Connect to MCP Client
    client = await connect()

    async with client:
        
        #2. Discover MCP tools
        tools = await discover_tools(client)

        print("----Loading MCP Tools---")
        for tool in tools:
            print(tool.name, tool.description)
        print("------------------------")

        #3. User Input
        user_request = input("Enter Question : ")

        #3. Run Agent loop
        state = await run_agent_loop(
            user_request,
            planner,
            execute_action,
            format_answer,
            tools,
            client
        )

        """ run_agent_loop

        1. Create initial State (state)
        2. Decompose (user_request) using LLM : [task1, task2,...]
        3. Set the Tasks in the State.task[]
        4. Loop until State.current_step < State.max_steps
           - If all task completed using (state.observations[{tasks:"task1",state:"pending"},{tasks:"task2",state:"pending"}])
                - formatAnswer(state), set  state.final_result, Based on the state properties, using LLM, generate natural sounding answer using user-request,observations, actions etc.
                - finish(state,answer) set state.finished = "completed"
.          - else
               - Ask planner for next action for one pending task based on
                  decision = planner(state,tools)
                  # state.tasks = [task1, task2,...]
                  # decsion = {TASK: "***", TOOL:"***"}
                  
                    Ask planner for next action for one pending task based on
                    - User Request:
                      {state["user_request"]}
                    - Tasks:
                      {state["tasks"]}
                    - Completed Actions:
                      {state["actions"]}
                    - Previous Observations:
                      {state["observations"]}
                    - Intermediary Results:
                      {state["results"]}
                    - Available Tools:
                      {tools_description}
                    - Return exactly:
                        TASK: <task>
                        TOOL: <tool> 
                         
                - state["current_task"] = task
                - state.observation += await execute_action(tool,state,client)
                - state.action += tool
                - state.results += results
                - state["current_step"] += 1

         5. return state.final_result         
        """

        print("Final Answer : ",state["final_answer"])

if __name__ == "__main__":
    asyncio.run(main())