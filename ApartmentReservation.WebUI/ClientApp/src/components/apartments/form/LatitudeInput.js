import React from "react";
import { NumberInput } from "../../baseFormHelpers";

export const LatitudeInput = ({
  label = "Latitude",
  name = "latitude",
  placeholder = "ex. -73.956096",
  step = 0.1,
  required = false,
  ...other
}) => (
  <NumberInput
    label={label}
    name={name}
    placeholder={placeholder}
    step={step}
    required={required}
    {...other}
  />
);
