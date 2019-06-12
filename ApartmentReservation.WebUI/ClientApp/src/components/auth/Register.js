import React, { Component } from "react";
import { connect } from "react-redux";
import { signup } from "../../store/actions";
import { UserForm } from "./UserForm";

export class Register extends Component {
  handleSubmit = user => {
    this.props.signup(user);
  };
  render() {
    const { authError } = this.props;
    return (
      <div>
        <h1>Register</h1>
        <div>{authError ? <p>{authError}</p> : null}</div>
        <UserForm handleSubmit={this.handleSubmit} />
      </div>
    );
  }
}
const mapStateToProps = state => {
  return {
    authError: state.auth.authError,
    user: state.auth.user
  };
};

export default connect(
  mapStateToProps,
  { signup }
)(Register);
