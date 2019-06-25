import React from "react";
import { SelectInput } from "../../baseFormHelpers";
import { activityStates } from "../../../constants";

const options = [
  { value: activityStates.Active, text: "Active" },
  { value: activityStates.Inactive, text: "Inactive" }
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
