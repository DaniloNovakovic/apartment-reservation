import React from "react";
import { Form, Col } from "react-bootstrap";
export const LongitudeInput = props => (
  <Form.Group as={Col}>
    <Form.Label>Longitude</Form.Label>
    <Form.Control
      type="number"
      name="longitude"
      placeholder="ex. 40.754026"
      onChange={props.handleChange}
    />
  </Form.Group>
);
