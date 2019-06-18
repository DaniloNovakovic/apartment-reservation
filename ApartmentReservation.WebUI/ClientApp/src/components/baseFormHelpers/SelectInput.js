import React from "react";
import { Form } from "react-bootstrap";

export function SelectInput({
  handleChange,
  value,
  label,
  name,
  required,
  feedback,
  options = [],
  ...other
}) {
  return (
    <Form.Group {...other}>
      <Form.Label>{label}</Form.Label>
      <Form.Control
        as="select"
        name={name}
        onChange={handleChange}
        value={value}
        required={required}
      >
        {options.map((item, index) => {
          return (
            <option
              key={item.key || item.value || item.text || index}
              value={item.value}
            >
              {item.text}
            </option>
          );
        })}
      </Form.Control>
      <Form.Control.Feedback>{feedback}</Form.Control.Feedback>
    </Form.Group>
  );
}
