import React from "react";
import { TextInput } from "../../baseFormHelpers";

export const StreetNameInput = ({
  label = "Street Name",
  placeholder = "ex. Main street",
  name = "streetName",
  required = true,
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
