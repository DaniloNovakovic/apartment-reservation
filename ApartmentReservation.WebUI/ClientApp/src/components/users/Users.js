import React, { Component } from "react";
import { Link } from "react-router-dom";
import { userService } from "../../services";
import { UsersTable } from "./UsersTable";
import { Spinner } from "react-bootstrap";

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
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : (
      <UsersTable
        users={this.state.users}
        deleteUserHandler={this.deleteUserHandler}
      />
    );

    return (
      <section>
        <header>
          <h1>Users</h1>
        </header>
        <main>
          <Link to="/add-guest" className="btn btn-primary">
            Add Guest
          </Link>
          <Link to="/add-host" className="btn btn-primary">
            Add Host
          </Link>
          <br />
          {contents}
        </main>
      </section>
    );
  }
}
