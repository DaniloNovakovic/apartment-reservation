import React from "react";
import { Form, Col } from "react-bootstrap";

export const LatitudeInput = ({
  handleChange,
  value,
  label = "Latitude",
  name = "latitude",
  placeholder = "ex. -73.956096",
  required = false,
  ...other
}) => (
  <Form.Group {...other}>
    <Form.Label>{label}</Form.Label>
    <Form.Control
      type="number"
      name={name}
      onChange={handleChange}
      value={value}
      placeholder={placeholder}
      required={required}
    />
  </Form.Group>
);
