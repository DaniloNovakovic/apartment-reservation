import React from "react";
import { Form } from "react-bootstrap";

export const CheckInTimeInput = ({
  handleChange,
  value,
  label = "Check in time",
  name = "checkInTime",
  required = false,
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
