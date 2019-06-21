import React from "react";
import { roleNames } from "../../../constants";
import { mapGuestReservationStateToJsxFactory } from "./mapGuestReservationStateToJsxFactory";
import { mapHostReservationStateToJsxFactory } from "./mapHostReservationStateToJsxFactory";

export const mapRoleNameToJsxFactory = (roleName, reservationState) => {
  if (roleName === roleNames.Host) {
    return mapHostReservationStateToJsxFactory(reservationState);
  } else if (roleName === roleNames.Guest) {
    return mapGuestReservationStateToJsxFactory(reservationState);
  } else {
    return () => <div />;
  }
};
