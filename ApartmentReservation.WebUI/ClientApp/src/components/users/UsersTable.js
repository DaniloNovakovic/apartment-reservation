import React from "react";
import { Link } from "react-router-dom";
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
              <Link to={`/edit-user/${user.id}`} className="btn btn-warning">
                Edit
              </Link>
            )}
          </td>
          <td>
            {user.roleName !== roleNames.Admin && (
              <button
                className="btn btn-danger"
                onClick={() => deleteUserHandler(user)}
              >
                Delete
              </button>
            )}
          </td>
        </tr>
      ))}
    </tbody>
  </table>
);

export default UsersTable;
