import React, { Component } from "react";
import { connect } from "react-redux";
import { logout } from "../../store/actions";
import { Spinner, Alert } from "react-bootstrap";

export class Logout extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }
  componentDidMount() {
    this.setState({ loggingOut: true });
    this.props.logout().then(_ => this.setState({ loggingOut: false }));
  }
  render() {
    const { alert } = this.props;
    return (
      <div>
        <h1>
          {!this.state.loggingOut ? (
            <Alert variant={alert.type}>{alert.message}</Alert>
          ) : (
            <Spinner animation="border" role="status">
              <span className="sr-only">Logging out...</span>
            </Spinner>
          )}
        </h1>
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
  { logout }
)(Logout);
