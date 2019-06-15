import React from "react";
import { Form, Col } from "react-bootstrap";
export const StreetNumberInput = props => (
  <Form.Group as={Col} sm="5">
    <Form.Label>Street Number</Form.Label>
    <Form.Control
      name="streetNumber"
      placeholder="ex. 92"
      onChange={props.handleChange}
    />
  </Form.Group>
);
