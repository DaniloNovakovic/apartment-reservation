import React, { Component } from "react";
import { Link } from "react-router-dom";
import { userService } from "../../services";

export default class Users extends Component {
  constructor(props) {
    super(props);
    this.state = { users: [], loading: true };
  }
  componentDidMount() {
    this.refreshData();
  }
  refreshData = () => {
    userService.getAll().then(data => {
      this.setState({ users: data, loading: false });
    });
  };

  static renderUsersTable(users) {
    return (
      <table className="table table-striped table-hover">
        <thead>
          <tr>
            <th>username</th>
            <th>firstName</th>
            <th>lastName</th>
            <th>gender</th>
            <th>roleName</th>
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
            </tr>
          ))}
        </tbody>
      </table>
    );
  }
  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      Users.renderUsersTable(this.state.users)
    );

    return (
      <div>
        <h1>Users</h1>
        <Link to="/add-guest" className="btn btn-primary">
          Add Guest
        </Link>
        <Link to="/add-host" className="btn btn-primary">
          Add Host
        </Link>
        {contents}
      </div>
    );
  }
}
