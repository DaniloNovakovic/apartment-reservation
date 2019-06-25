import React from "react";
import { SelectInput } from "../../baseFormHelpers";
import { apartmentTypes } from "../../../constants";

const options = [
  { value: apartmentTypes.Full, text: "Full" },
  { value: apartmentTypes.SingleRoom, text: "Single Room" }
];

export function ApartmentTypeInput({
  label = "Apartment Type",
  name = "apartmentType",
  required = true,
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
