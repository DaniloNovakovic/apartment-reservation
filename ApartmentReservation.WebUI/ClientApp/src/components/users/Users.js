import "./Users.css";
import React, { Component } from "react";
import { Link } from "react-router-dom";
import { FaPlus } from "react-icons/fa";
import { userService } from "../../services";
import { UsersTable } from "./UsersTable";
import { Spinner, Button, ButtonGroup } from "react-bootstrap";
import UsersFilter from "./UsersFilter";

export default class Users extends Component {
  constructor(props) {
    super(props);
    this.state = { users: [], loading: true };
  }
  componentDidMount() {
    this.refreshData();
  }
  refreshData = (filters = {}) => {
    this.setState({ loading: true });
    userService.getAll(filters).then(data => {
      this.setState({ users: data, loading: false });
    });
  };
  banUserHandler = user => {
    userService.ban(user.id).then(_ => this.refreshData());
  };
  unbanUserHandler = user => {
    userService.unban(user.id).then(_ => this.refreshData());
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
        banUserHandler={this.banUserHandler}
        unbanUserHandler={this.unbanUserHandler}
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
              <FaPlus /> Add Guest
            </Button>
            <Button as={Link} to="/add-host" variant="primary">
              <FaPlus /> Add Host
            </Button>
            <UsersFilter handleSubmit={this.refreshData} />
          </ButtonGroup>
          <br />
          {contents}
        </main>
      </section>
    );
  }
}
