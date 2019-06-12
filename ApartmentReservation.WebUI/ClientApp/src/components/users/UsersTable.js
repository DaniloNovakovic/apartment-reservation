import React from "react";
import { roleNames } from "../../constants";

export const UsersTable = ({ users, deleteUserHandler }) => (
  <table className="table table-striped table-hover">
    <thead>
      <tr>
        <th>username</th>
        <th>firstName</th>
        <th>lastName</th>
        <th>gender</th>
        <th>roleName</th>
        <th />
      </tr>
    </thead>
    <tbody>
      {users.map(user => (
        <tr key={user.id}>
          <td>{user.username}</td>
          <td>{user.firstName}</td>
          <td>{user.lastName}</td>
          <td>{user.gender}</td>
          <td>{user.roleName}</td>
          <td>
            {user.roleName !== roleNames.Admin && (
              <button onClick={() => deleteUserHandler(user)}>Delete</button>
            )}
          </td>
        </tr>
      ))}
    </tbody>
  </table>
);

export default UsersTable;
