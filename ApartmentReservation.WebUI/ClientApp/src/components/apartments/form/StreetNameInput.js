import React from "react";
import { Form } from "react-bootstrap";

export const StreetNameInput = ({
  handleChange,
  value,
  label = "Street Name",
  placeholder = "ex. Main street",
  name = "streetName",
  ...other
}) => {
  return (
    <Form.Group {...other}>
      <Form.Label>{label}</Form.Label>
      <Form.Control
        name={name}
        value={value}
        placeholder={placeholder}
        onChange={handleChange}
      />
    </Form.Group>
  );
};
