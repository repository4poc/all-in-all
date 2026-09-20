import { useState, useEffect } from "react";
import { UsersData } from "./UsersData";

import useToggle from "./useToogle";

function UserChallenge() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [users, setUsers] = useState(UsersData);

  const { flag, handleFlag } = useToggle(false);

  // Fix : React State Sync Issue
  useEffect(() => {
    console.log(email, password);
  }, [email, password]);

  // Handle Submit
  const handleSubmit = (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

    setEmail(e.currentTarget.email.value);
    setPassword(e.currentTarget.password.value);

    const newuser = {
      id: Date.now(),
      name: e.currentTarget.email.value,
      age: 10,
    };

    setUsers([...users, newuser]);
  };

  // Handle Remove
  const handleRemove = (id: Number) => {
    const newArray = users.filter((user) => user.id !== id);
    setUsers(newArray);
  };

  return (
    <>
      <div className="container">
        <form className="form" onSubmit={handleSubmit}>
          <div className="form-row">
            <label className="form-label" htmlFor="email">
              Email
            </label>
            <input className="form-input" type="text" id="email"></input>
          </div>

          <div className="form-row">
            <label className="form-label" htmlFor="password">
              Password
            </label>
            <input className="form-input" type="password" id="password"></input>
          </div>
          <button type="submit">Submit</button>
        </form>

        <h3>List users</h3>
        {users.map((user) => {
          const { id, name, age } = user;
          return (
            <div key={user.id}>
              <p>{user.name}</p>
              <button onClick={() => handleRemove(id)}>Remote</button>
            </div>
          );
        })}
      </div>

      <>
        {flag && <p>Hello</p>}
        <button onClick={handleFlag}>Toggle</button>
      </>
    </>
  );
}

export default UserChallenge;
