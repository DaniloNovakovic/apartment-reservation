import React from "react";
import { Form, Col } from "react-bootstrap";
export const PostalCodeInput = props => (
  <Form.Group as={Col}>
    <Form.Label>Zip</Form.Label>
    <Form.Control
      name="postalCode"
      placeholder="ex. 21102"
      onChange={props.handleChange}
    />
  </Form.Group>
);
