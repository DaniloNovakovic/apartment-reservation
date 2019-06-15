import React from "react";
import { TextInput } from "./base";

export const CityNameInput = ({
  label = "City",
  name = "cityName",
  placeholder = "ex. Novi Sad",
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
