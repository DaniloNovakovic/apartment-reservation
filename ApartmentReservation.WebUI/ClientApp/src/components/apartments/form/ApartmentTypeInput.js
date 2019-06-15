import React from "react";
import { SelectInput } from "./base";

const options = [
  { value: "Full", text: "Full" },
  { value: "SingleRoom", text: "Single Room" }
];

export function ApartmentTypeInput({
  label = "Apartment Type",
  name = "apartmentType",
  required = false,
  types = options,
  ...other
}) {
  return (
    <SelectInput
      label={label}
      options={types}
      name={name}
      required={required}
      {...other}
    />
  );
}
