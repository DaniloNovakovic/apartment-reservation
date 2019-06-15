import React from "react";
import { TimeInput } from "./base/TimeInput";

export const CheckOutTimeInput = ({
  label = "Check out time",
  name = "checkOutTime",
  required = false,
  ...other
}) => <TimeInput label={label} name={name} required={required} {...other} />;
