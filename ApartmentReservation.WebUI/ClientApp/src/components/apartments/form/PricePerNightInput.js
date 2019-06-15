import React from "react";
import { Form, Col } from "react-bootstrap";
export const PricePerNightInput = props => (
  <Form.Group as={Col}>
    <Form.Label>Price($) per night</Form.Label>
    <Form.Control
      type="number"
      name="pricePerNight"
      min="0"
      placeholder="ex. 20"
      onChange={props.handleChange}
    />
  </Form.Group>
);
