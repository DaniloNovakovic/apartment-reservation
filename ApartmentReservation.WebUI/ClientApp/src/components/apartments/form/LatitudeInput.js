import React from "react";
import { NumberInput } from "./base";

export const LatitudeInput = ({
  label = "Latitude",
  name = "latitude",
  placeholder = "ex. -73.956096",
  required = false,
  ...other
}) => (
  <NumberInput
    label={label}
    name={name}
    placeholder={placeholder}
    required={required}
    {...other}
  />
);
