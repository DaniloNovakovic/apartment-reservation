import React from "react";
import { NumberInput } from "../../baseFormHelpers";

export const NumberOfRoomsInput = ({
  label = "Number of rooms",
  name = "numberOfRooms",
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
