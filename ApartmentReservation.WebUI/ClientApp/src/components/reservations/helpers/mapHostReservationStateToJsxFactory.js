import React from "react";
import { ButtonGroup, Button } from "react-bootstrap";
import { reservationStates } from "../../../constants";
import CompleteReservation from "../CompleteReservation";

const hostStateToButtonsFactory = {
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

export const mapHostReservationStateToJsxFactory = state => {
  if (hostStateToButtonsFactory.hasOwnProperty(state)) {
    return hostStateToButtonsFactory[state];
  } else {
    return () => <div />;
  }
};
