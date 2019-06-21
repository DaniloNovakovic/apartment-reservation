import React from "react";
import { roleNames } from "../../../constants";
import { mapGuestReservationStateToJsx } from "./mapGuestReservationStateToJsx";
import { mapHostReservationStateToJsx } from "./mapHostReservationStateToJsx";

export const mapRoleNameToJsx = (roleName, reservationState) => {
  if (roleName === roleNames.Host) {
    return mapHostReservationStateToJsx(reservationState);
  } else if (roleName === roleNames.Guest) {
    return mapGuestReservationStateToJsx(reservationState);
  } else {
    return () => <div />;
  }
};
