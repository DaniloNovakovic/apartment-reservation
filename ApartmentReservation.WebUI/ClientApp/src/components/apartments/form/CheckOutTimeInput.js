import React from "react";
import { TimeInput } from "../../baseFormHelpers/TimeInput";

export const CheckOutTimeInput = ({
  label = "Check out time",
  name = "checkOutTime",
  required = false,
  ...other
}) => <TimeInput label={label} name={name} required={required} {...other} />;
