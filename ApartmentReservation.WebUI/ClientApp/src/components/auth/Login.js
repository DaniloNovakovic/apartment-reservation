import React, { Component } from "react";
import { connect } from "react-redux";
import { login } from "../../store/actions";
import { Form, Button, Alert } from "react-bootstrap";

export class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: "",
      password: ""
    };
  }
  handleChange = event => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };

  handleSubmit = event => {
    event.preventDefault();
    const { username, password } = this.state;
    this.props.login(username, password);
  };

  render() {
    const { alert } = this.props;
    return (
      <div>
        <h1>Login</h1>
        <div>
          {alert && alert.message ? (
            <Alert variant={alert.type}>{alert.message}</Alert>
          ) : null}
        </div>
        <Form onSubmit={this.handleSubmit}>
          <Form.Group>
            <Form.Label htmlFor="username">Username:</Form.Label>
            <Form.Control
              type="text"
              name="username"
              value={this.state.username || ""}
              onChange={this.handleChange}
              placeholder="Enter username..."
              required
            />
          </Form.Group>
          <Form.Group>
            <Form.Label htmlFor="password">Password:</Form.Label>
            <Form.Control
              type="password"
              minLength="4"
              name="password"
              value={this.state.password || ""}
              onChange={this.handleChange}
              placeholder="Enter password..."
              required
            />
          </Form.Group>
          <Button variant="primary" type="submit">
            Submit
          </Button>
        </Form>
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
  { login }
)(Login);
