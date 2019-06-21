import React from "react";
import { roleNames } from "../../../constants";
import { mapGuestReservationStateToJsx } from "./mapGuestReservationStateToJsx";
import { mapHostReservationStateToJsx } from "./mapHostReservationStateToJsx";

export const mapRoleNameToJsx = (
  roleName,
  reservationState,
  onClickHandlers
) => {
  if (roleName == roleNames.Host) {
    return mapHostReservationStateToJsx(reservationState, onClickHandlers);
  } else if (roleName == roleNames.Guest) {
    return mapGuestReservationStateToJsx(reservationState, onClickHandlers);
  } else {
    return <div />;
  }
};
