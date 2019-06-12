import React, { Component } from "react";
import { UserForm } from "../auth/UserForm";

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
    console.log("Adding " + user);
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
