// middleware/fetchUsers.js
import axios from "axios";

const fetchUsers = async (req, res, next) => {
  try {
    const response = await axios.get(
      "https://jsonplaceholder.typicode.com/users",
    );

    req.users = response.data; // attach to request
    next();
  } catch (error) {
    error.status = 500;
    error.message = "Failed to fetch users";
    next(error);
  }
};

export default fetchUsers;
