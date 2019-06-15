import React from "react";
import { Form } from "react-bootstrap";

export function ApartmentTypeInput({
  handleChange,
  value,
  label = "Apartment Type",
  name = "apartmentType",
  required = false,
  ...other
}) {
  return (
    <Form.Group {...other}>
      <Form.Label>{label}</Form.Label>
      <Form.Control
        as="select"
        name={name}
        value={value}
        onChange={handleChange}
        required={required}
      >
        <option value="Full">Full</option>
        <option value="SingleRoom">Single Room</option>
      </Form.Control>
    </Form.Group>
  );
}
