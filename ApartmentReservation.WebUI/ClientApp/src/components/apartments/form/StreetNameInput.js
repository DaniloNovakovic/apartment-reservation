import React from "react";
import { TextInput } from "../../baseFormHelpers";

export const StreetNameInput = ({
  label = "Street Name",
  placeholder = "ex. Main street",
  name = "streetName",
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
