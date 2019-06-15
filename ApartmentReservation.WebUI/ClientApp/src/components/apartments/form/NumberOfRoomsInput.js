import React from "react";
import { Form, Col } from "react-bootstrap";
export const NumberOfRoomsInput = props => (
  <Form.Group as={Col}>
    <Form.Label>Number of rooms</Form.Label>
    <Form.Control
      type="number"
      name="numberOfRooms"
      min="1"
      placeholder="ex. 2"
      onChange={props.handleChange}
    />
  </Form.Group>
);
