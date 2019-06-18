import React from "react";
import { SelectInput } from "../../baseFormHelpers";

const options = [
  { value: "Active", text: "Active" },
  { value: "Inactive", text: "Inactive" }
];

export function ActivityStateInput({
  label = "Activity State",
  name = "activityState",
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
