import React from "react";
import { Form } from "react-bootstrap";

export const TextInput = ({
  handleChange,
  value,
  maxLength,
  label,
  name,
  placeholder,
  required,
  feedback,
  ...other
}) => (
  <Form.Group {...other}>
    <Form.Label>{label}</Form.Label>
    <Form.Control
      name={name}
      onChange={handleChange}
      value={value}
      maxLength={maxLength}
      placeholder={placeholder}
      required={required}
    />
    <Form.Control.Feedback type="invalid">{feedback}</Form.Control.Feedback>
  </Form.Group>
);
