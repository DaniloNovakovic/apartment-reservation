import React, { Component } from "react";
import { connect } from "react-redux";
import { logout } from "../../store/actions";

export class Logout extends Component {
  componentWillMount() {
    this.props.logout();
  }
  render() {
    const { authError } = this.props;
    return (
      <div>
        <h1>{authError || "Logging out..."}</h1>
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
  { logout }
)(Logout);
