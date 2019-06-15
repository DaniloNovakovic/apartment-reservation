import React from "react";
import { TimeInput } from "./base/TimeInput";

export const CheckInTimeInput = ({
  label = "Check in time",
  name = "checkInTime",
  required = false,
  ...other
}) => <TimeInput label={label} name={name} required={required} {...other} />;
