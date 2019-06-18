import React from "react";
import { TextInput } from "../../baseFormHelpers";

export const ApartmentTitleInput = ({
  maxLength = 50,
  label = "Title",
  name = "title",
  placeholder = "Enter title for apartment",
  feedback = "Title is required!",
  required = true,
  ...other
}) => (
  <TextInput
    maxLength={maxLength}
    label={label}
    name={name}
    placeholder={placeholder}
    required={required}
    feedback={feedback}
    {...other}
  />
);
