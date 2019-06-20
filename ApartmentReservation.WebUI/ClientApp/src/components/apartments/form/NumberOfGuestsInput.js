import React from "react";
import { NumberInput } from "../../baseFormHelpers";

export const NumberOfGuestsInput = ({
  label = "Number of guests",
  name = "numberOfGuests",
  min = 1,
  placeholder = "ex. 2",
  required = true,
  ...other
}) => (
  <NumberInput
    label={label}
    name={name}
    min={min}
    placeholder={placeholder}
    required={required}
    {...other}
  />
);
