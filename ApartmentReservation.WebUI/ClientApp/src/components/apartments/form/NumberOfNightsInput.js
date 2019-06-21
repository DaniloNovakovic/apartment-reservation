import React from "react";
import { NumberInput } from "../../baseFormHelpers";

export const NumberOfNightsInput = ({
  label = "Number of nights",
  name = "numberOfNights",
  min = 1,
  placeholder = "ex. 1",
  required = true,
  ...other
}) => {
  return (
    <NumberInput
      label={label}
      name={name}
      min={min}
      placeholder={placeholder}
      required={required}
      {...other}
    />
  );
};
