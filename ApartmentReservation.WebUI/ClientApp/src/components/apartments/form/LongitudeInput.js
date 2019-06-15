import React from "react";
import { NumberInput } from "./base";

export const LongitudeInput = ({
  label = "Longitude",
  name = "longitude",
  placeholder = "ex. 40.754026",
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
