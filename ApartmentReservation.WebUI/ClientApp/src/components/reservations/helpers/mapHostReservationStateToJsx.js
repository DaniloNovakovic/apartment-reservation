import React from "react";
import { ButtonGroup, Button } from "react-bootstrap";
import { reservationStates } from "../../../constants";
import CompleteReservation from "../CompleteReservation";

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
  [reservationStates.Accepted]: props => <CompleteReservation {...props} />
};

export const mapHostReservationStateToJsx = state => {
  if (hostStateToButtons.hasOwnProperty(state)) {
    return hostStateToButtons[state];
  } else {
    return () => <div />;
  }
};
