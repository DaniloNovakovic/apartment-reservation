import React from "react";
import { Form } from "react-bootstrap";

export const TimeInput = ({
  handleChange,
  value,
  label,
  name,
  required,
  ...other
}) => (
  <Form.Group {...other}>
    <Form.Label>{label}</Form.Label>
    <Form.Control
      type="time"
      name={name}
      onChange={handleChange}
      value={value}
      required={required}
    />
  </Form.Group>
);
