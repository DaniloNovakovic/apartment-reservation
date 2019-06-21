import React from "react";
import { ButtonGroup, Button } from "react-bootstrap";
import { reservationStates } from "../../../constants";

const hostStateToButtons = {
  [reservationStates.Created]: ({ handleAccept, handleDeny }) => (
    <ButtonGroup>
      <Button variant="success" onClick={handleAccept}>
        Accept
      </Button>
      <Button variant="danger" onClick={handleDeny}>
        Deny
      </Button>
    </ButtonGroup>
  ),
  [reservationStates.Accepted]: ({ handleComplete }) => (
    <Button variant="success" onClick={handleComplete}>
      Complete
    </Button>
  )
};

export const mapHostReservationStateToJsx = (state, onClickHandlers) => {
  if (hostStateToButtons.hasOwnProperty(state)) {
    return hostStateToButtons[state](onClickHandlers);
  } else {
    return <div />;
  }
};
