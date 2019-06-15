import React from "react";
import { Form } from "react-bootstrap";

export const NumberInput = ({
  handleChange,
  value,
  min,
  max,
  label,
  name,
  placeholder,
  required,
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
      min={min}
      max={max}
    />
  </Form.Group>
);
