import "./UsersTable.css";
import React from "react";
import { Link } from "react-router-dom";
import { roleNames } from "../../constants";
import { Table, ButtonToolbar, Button } from "react-bootstrap";

export const UsersTable = ({
  users,
  deleteUserHandler = () => {},
  banUserHandler = () => {},
  unbanUserHandler = () => {}
}) => (
  <Table striped hover bordered className="users-table">
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
              <ButtonToolbar>
                <Button
                  as={Link}
                  to={`/edit-user/${user.id}`}
                  variant="warning"
                >
                  Edit
                </Button>
                {user.banned ? (
                  <Button
                    variant="outline-success"
                    onClick={() => unbanUserHandler(user)}
                  >
                    Unban
                  </Button>
                ) : (
                  <Button
                    variant="outline-danger"
                    onClick={() => banUserHandler(user)}
                  >
                    Ban
                  </Button>
                )}
                <Button
                  variant="danger"
                  onClick={() => deleteUserHandler(user)}
                >
                  Delete
                </Button>
              </ButtonToolbar>
            )}
          </td>
        </tr>
      ))}
    </tbody>
  </Table>
);

export default UsersTable;
