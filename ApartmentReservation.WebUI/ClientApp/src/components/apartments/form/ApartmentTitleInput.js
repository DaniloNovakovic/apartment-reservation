import React from "react";
import { TextInput } from "./base";

export const ApartmentTitleInput = ({
  maxLength = 50,
  label = "Title",
  name = "title",
  placeholder = "Enter title for apartment",
  required = true,
  ...other
}) => (
  <TextInput
    maxLength={maxLength}
    label={label}
    name={name}
    placeholder={placeholder}
    required={required}
    {...other}
  />
);
