import axios from "axios";

let cachedUsers = [];
let lastUpdated = null;

export const getCachedUsers = () => cachedUsers;

export const loadUsers = async () => {
  try {
    const response = await axios.get(
      "https://jsonplaceholder.typicode.com/users",
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

  // refresh every 6 minutes
  setInterval(loadUsers, 6 * 60 * 1000);
};
