import axios from "axios";

let cachedUsers = [];
let cachedTodos = [];

let lastUpdated = null;

export const getCachedUsers = () => cachedUsers;
export const getCachedTodos = () => cachedTodos;

export const loadTodos = async () => {
  try {
    let response = await axios.get(
      "https://jsonplaceholder.typicode.com/todos",
    );

    cachedTodos = response.data;
    lastUpdated = new Date();

    console.log("Todos cache refreshed at:", lastUpdated.toISOString());
  } catch (error) {
    console.error("Failed to refresh users cache:", error.message);
  }
};

export const loadUsers = async () => {
  try {
    let response = await axios.get(
      "https://jsonplaceholder.typicode.com/Users",
    );

    cachedUsers = response.data;
    lastUpdated = new Date();

    console.log("Users cache refreshed at:", lastUpdated.toISOString());
  } catch (error) {
    console.error("Failed to refresh users cache:", error.message);
  }
};

export const startUsersCacheRefresh = async () => {
  // load immediately on app startup
  await loadUsers();
  await loadTodos();

  // refresh every 6 minutes
  setInterval(loadUsers, 60 * 1000);
  setInterval(loadTodos, 60 * 1000);
};
