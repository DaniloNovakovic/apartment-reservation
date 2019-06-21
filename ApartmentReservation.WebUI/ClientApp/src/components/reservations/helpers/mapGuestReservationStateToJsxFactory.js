import React from "react";
import { Button } from "react-bootstrap";
import { reservationStates } from "../../../constants";

const guestStateToButtonsFactory = {
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

export const mapGuestReservationStateToJsxFactory = state => {
  if (guestStateToButtonsFactory.hasOwnProperty(state)) {
    return guestStateToButtonsFactory[state];
  } else {
    return () => <div />;
  }
};
