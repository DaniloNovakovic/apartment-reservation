import React, { Component } from "react";
import { Form, Button } from "react-bootstrap";

export default class AmenityForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      ...this.props.amenity
    };
  }
  handleSubmit = event => {
    event.preventDefault();
    this.props.handleSubmit(this.state);
  };
  handleChange = event => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };
  render() {
    const { name } = this.state;
    return (
      <Form onSubmit={this.handleSubmit}>
        <Form.Group>
          <Form.Label>Name</Form.Label>
          <Form.Control
            name="name"
            value={name}
            onChange={this.handleChange}
            placeholder="Enter amenity name..."
            required
          />
        </Form.Group>
        <Button type="submit">Submit</Button>
      </Form>
    );
  }
}
