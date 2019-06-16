import React from "react";
import { NumberInput } from "../../baseFormHelpers";

export const PricePerNightInput = ({
  label = "Price($) per night",
  name = "pricePerNight",
  min = 0,
  placeholder = "ex. 20",
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
