import React from "react";
import { history } from "../../helpers";

const defaultProps = {
  user: {
    username: "",
    password: "",
    firstName: "",
    lastName: "",
    gender: "Male"
  },
  handleSubmit: user => {
    console.log(user);
  },
  handleCancel: () => {}
};

export class EditProfile extends React.Component {
  constructor(props = defaultProps) {
    super(props);

    this.state = {
      ...props.user
    };
  }

  handleChange = event => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };
  handleSubmit = event => {
    event.preventDefault();
    this.props.handleSubmit(this.state);
  };
  handleCancel = () => {
    if (this.props.handleCancel) {
      this.props.handleCancel();
    } else {
      history.goBack();
    }
  };
  render() {
    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label htmlFor="password">Password:</label>
            <input
              type="password"
              minLength="4"
              name="password"
              value={this.state.password || ""}
              onChange={this.handleChange}
              required
            />
          </div>
          <div className="form-group">
            <label htmlFor="firstName">First Name:</label>
            <input
              type="text"
              name="firstName"
              value={this.state.firstName || ""}
              onChange={this.handleChange}
            />
          </div>
          <div className="form-group">
            <label htmlFor="lastName">Last Name:</label>
            <input
              type="text"
              name="lastName"
              value={this.state.lastName || ""}
              onChange={this.handleChange}
            />
          </div>
          <div className="form-group">
            <label htmlFor="gender">Gender:</label>
            <select
              name="gender"
              value={this.state.gender || ""}
              onChange={this.handleChange}
            >
              <option value="Male">Male</option>
              <option value="Female">Female</option>
              <option value="Other">Other</option>
            </select>
          </div>
          <div>
            <input type="submit" className="btn btn-primary" value="submit" />
            <button
              type="reset"
              className="btn btn-info"
              onClick={this.handleCancel}
            >
              Cancel
            </button>
          </div>
        </form>
      </div>
    );
  }
}
