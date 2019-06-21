import React from "react";
import { Button } from "react-bootstrap";
import { reservationStates } from "../../../constants";

const guestStateToButtons = {
  [reservationStates.Created]: ({ handleWithdraw }) => (
    <Button variant="danger" onClick={handleWithdraw}>
      Withdraw
    </Button>
  ),
  [reservationStates.Accepted]: ({ handleWithdraw }) => (
    <Button variant="danger" onClick={handleWithdraw}>
      Withdraw
    </Button>
  )
};

export const mapGuestReservationStateToJsx = (state, onClickHandlers) => {
  if (guestStateToButtons.hasOwnProperty(state)) {
    return guestStateToButtons[state](onClickHandlers);
  } else {
    return <div />;
  }
};
