import React from "react";
import { NumberInput } from "../../baseFormHelpers";

export const LongitudeInput = ({
  label = "Longitude",
  name = "longitude",
  placeholder = "ex. 40.754026",
  step = 0.000001,
  required = true,
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
