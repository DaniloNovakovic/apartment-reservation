import React from "react";
import { Form } from "react-bootstrap";
export const CityNameInput = ({
  handleChange,
  value,
  label = "City",
  name = "cityName",
  placeholder = "ex. Novi Sad",
  required = false,
  ...other
}) => (
  <Form.Group {...other}>
    <Form.Label>{label}</Form.Label>
    <Form.Control
      name={name}
      onChange={handleChange}
      value={value}
      placeholder={placeholder}
      required={required}
    />
  </Form.Group>
);
