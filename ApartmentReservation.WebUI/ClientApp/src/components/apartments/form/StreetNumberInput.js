import React from "react";
import { TextInput } from "./base";

export const StreetNumberInput = ({
  label = "Street Number",
  placeholder = "ex. 92",
  name = "streetNumber",
  required = false,
  ...other
}) => (
  <TextInput
    label={label}
    name={name}
    placeholder={placeholder}
    required={required}
    {...other}
  />
);
