import React from 'react';
import { fetchUsers } from '@/utils/actions';

const UsersList = async () => {
  const Users = await fetchUsers();
  console.log('---------------');

  console.log(Users.length);

  return (
    <div>
      {Users.map((user) => {
        return (
          <div id={user.id} key={user.id}>
            {`UserID : ${user.id}, First Name: ${user.firstName}, Last Name: ${user.lastName} `}
          </div>
        );
      })}
    </div>
  );
};

export default UsersList;
