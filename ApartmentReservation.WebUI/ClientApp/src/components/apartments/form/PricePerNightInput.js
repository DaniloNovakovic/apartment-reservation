import React from "react";
import { NumberInput } from "./base";

export const PricePerNightInput = ({
  label = "Price($) per night",
  name = "pricePerNight",
  min = 0,
  placeholder = "ex. 20",
  required = false,
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
