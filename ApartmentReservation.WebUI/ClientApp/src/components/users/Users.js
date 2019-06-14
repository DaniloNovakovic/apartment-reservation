import React, { Component } from "react";
import { Link } from "react-router-dom";
import { userService } from "../../services";
import { UsersTable } from "./UsersTable";
import { Spinner, Button, ButtonGroup } from "react-bootstrap";

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
          <ButtonGroup aria-label="Add user">
            <Button as={Link} to="/add-guest" variant="primary">
              Add Guest
            </Button>
            <Button as={Link} to="/add-host" variant="primary">
              Add Host
            </Button>
          </ButtonGroup>
          <br />
          {contents}
        </main>
      </section>
    );
  }
}
