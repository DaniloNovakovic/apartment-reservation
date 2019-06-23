import React from "react";
import { history } from "../../helpers";
import { Form, Row, Col } from "react-bootstrap";
import { genders } from "../../constants";

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
      ...props.user,
      gender: props.user.gender || "Male"
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
        <Form onSubmit={this.handleSubmit}>
          <Form.Group as={Row}>
            <Form.Label column sm={2} htmlFor="password">
              Password:
            </Form.Label>
            <Col sm={10}>
              <Form.Control
                type="password"
                minLength="4"
                name="password"
                value={this.state.password || ""}
                onChange={this.handleChange}
                placeholder="Enter password..."
                required
              />
            </Col>
          </Form.Group>
          <Form.Group as={Row}>
            <Form.Label column sm={2} htmlFor="firstName">
              First Name:
            </Form.Label>
            <Col sm={10}>
              <Form.Control
                type="text"
                name="firstName"
                value={this.state.firstName || ""}
                onChange={this.handleChange}
                placeholder="Enter first name..."
              />
            </Col>
          </Form.Group>
          <Form.Group as={Row}>
            <Form.Label column sm={2} htmlFor="lastName">
              Last Name:
            </Form.Label>
            <Col sm={10}>
              <Form.Control
                type="text"
                name="lastName"
                value={this.state.lastName || ""}
                onChange={this.handleChange}
                placeholder="Enter last name..."
              />
            </Col>
          </Form.Group>
          <Form.Group as={Row}>
            <Form.Label column sm={2} htmlFor="gender">
              Gender:
            </Form.Label>
            <Col sm={10}>
              <Form.Control
                as="select"
                name="gender"
                value={this.state.gender}
                onChange={this.handleChange}
              >
                <option value={genders.Male}>Male</option>
                <option value={genders.Female}>Female</option>
                <option value={genders.Other}>Other</option>
              </Form.Control>
            </Col>
          </Form.Group>
          <Form.Group as={Row}>
            <Col sm={1}>
              <input type="submit" className="btn btn-primary" />
            </Col>
            <Col sm={1}>
              <button
                type="reset"
                className="btn btn-info"
                onClick={this.handleCancel}
              >
                Cancel
              </button>
            </Col>
          </Form.Group>
        </Form>
      </div>
    );
  }
}
