import React, { Component } from "react";
import { Link } from "react-router-dom";
import { userService } from "../../services";
import { UsersTable } from "./UsersTable";

export default class Users extends Component {
  constructor(props) {
    super(props);
    this.state = { users: [], loading: true };
  }
  componentDidMount() {
    this.refreshData();
  }
  refreshData = () => {
    this.setState({ loading: true });
    userService.getAll().then(data => {
      this.setState({ users: data, loading: false });
    });
  };

  deleteUserHandler = user => {
    userService.delete(user.id).then(_ => this.refreshData());
  };

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      <UsersTable
        users={this.state.users}
        deleteUserHandler={this.deleteUserHandler}
      />
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
