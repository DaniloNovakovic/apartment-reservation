import React from "react";
import { Form } from "react-bootstrap";

export const ApartmentTitleInput = ({
  handleChange,
  value,
  maxLength = 50,
  label = "Title",
  name = "title",
  placeholder = "Enter title for apartment",
  required = true,
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
  </Form.Group>
);
