import React, { Component } from "react";
import { UserForm } from "../auth/UserForm";
import { hostService } from "../../services";
import { history } from "../../helpers";

export default class AddHost extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: "",
      password: "",
      firstName: "",
      lastName: "",
      gender: "Male"
    };
  }
  handleSubmit = user => {
    hostService.create(user).then(_ => {
      history.push("/Users");
    });
  };
  render() {
    return (
      <div>
        <h1>Add Host</h1>
        <UserForm user={this.state} handleSubmit={this.handleSubmit} />
      </div>
    );
  }
}
