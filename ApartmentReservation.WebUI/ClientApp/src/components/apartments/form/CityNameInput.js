import React from "react";
import { TextInput } from "../../baseFormHelpers";

export const CityNameInput = ({
  label = "City",
  name = "cityName",
  placeholder = "ex. Novi Sad",
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
