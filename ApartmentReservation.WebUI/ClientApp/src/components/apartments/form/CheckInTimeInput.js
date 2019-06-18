import React from "react";
import { TimeInput } from "../../baseFormHelpers/TimeInput";

export const CheckInTimeInput = ({
  label = "Check in time",
  name = "checkInTime",
  required = true,
  ...other
}) => <TimeInput label={label} name={name} required={required} {...other} />;
