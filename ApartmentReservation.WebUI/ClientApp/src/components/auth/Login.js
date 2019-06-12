import React, { Component } from "react";
import { connect } from "react-redux";
import { login } from "../../store/actions";

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
    const { authError } = this.props;
    return (
      <div>
        <h1>Login</h1>
        <div>{authError ? <p>{authError}</p> : null}</div>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label htmlFor="username">Username:</label>
            <input
              type="text"
              name="username"
              value={this.state.username}
              onChange={this.handleChange}
              required
            />
          </div>
          <div className="form-group">
            <label htmlFor="password">Password:</label>
            <input
              type="password"
              minLength="4"
              name="password"
              value={this.state.password}
              onChange={this.handleChange}
              required
            />
          </div>
          <div>
            <input type="submit" className="btn btn-primary" value="Submit" />
          </div>
        </form>
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
  { login }
)(Login);