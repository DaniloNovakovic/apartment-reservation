import React, { Component } from "react";
import { connect } from "react-redux";

export class Profile extends Component {
  render() {
    const user = this.props.user || {};
    return (
      <div>
        <p>username: {user.username}</p>
        <p>password: {user.password}</p>
        <p>firstName: {user.firstName}</p>
        <p>lastName: {user.lastName}</p>
        <p>gender: {user.gender}</p>
        <p>roleName: {user.roleName}</p>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user
  };
};

export default connect(mapStateToProps)(Profile);
