import React from "react";
import { TextInput } from "../../baseFormHelpers";

export const PostalCodeInput = ({
  label = "Zip",
  placeholder = "ex. 21102",
  name = "postalCode",
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
