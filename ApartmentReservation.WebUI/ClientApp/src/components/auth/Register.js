import React, { Component } from "react";
import { connect } from "react-redux";
import { signup } from "../../store/actions";
import { UserForm } from "./UserForm";
import { Alert } from "react-bootstrap";

export class Register extends Component {
  handleSubmit = user => {
    this.props.signup(user);
  };
  render() {
    const { alert } = this.props;
    return (
      <div>
        <h1>Register</h1>
        <div>
          {alert && alert.message ? (
            <Alert variant={alert.type}>{alert.message}</Alert>
          ) : null}
        </div>
        <UserForm handleSubmit={this.handleSubmit} />
      </div>
    );
  }
}
const mapStateToProps = state => {
  return {
    alert: state.alert,
    user: state.auth.user
  };
};

export default connect(
  mapStateToProps,
  { signup }
)(Register);
