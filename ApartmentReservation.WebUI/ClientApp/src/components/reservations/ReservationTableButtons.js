import React, { Component } from "react";
import { ButtonGroup, Button } from "react-bootstrap";
import { roleNames, reservationStates } from "../../constants";

const guestStateToButtons = {
  [reservationStates.Created]: <Button variant="danger">Withdraw</Button>,
  [reservationStates.Accepted]: <Button variant="danger">Withdraw</Button>
};

const hostStateToButtons = {
  [reservationStates.Created]: (
    <ButtonGroup>
      <Button variant="success">Accept</Button>
      <Button variant="danger">Deny</Button>
    </ButtonGroup>
  ),
  [reservationStates.Accepted]: <Button variant="success">Complete</Button>
};

const mapGuestReservationStateToJsx = state => {
  if (guestStateToButtons.hasOwnProperty(state)) {
    return guestStateToButtons[state];
  } else {
    return <div />;
  }
};

const mapHostReservationStateToJsx = state => {
  if (hostStateToButtons.hasOwnProperty(state)) {
    return hostStateToButtons[state];
  } else {
    return <div />;
  }
};

export default class ReservationTableButtons extends Component {
  render() {
    const { user = {}, reservation = {} } = this.props;
    const roleName = user.roleName;
    const reservationState = reservation.reservationState;
    if (roleName == roleNames.Host) {
      return mapHostReservationStateToJsx(reservationState);
    } else if (roleName == roleNames.Guest) {
      return mapGuestReservationStateToJsx(reservationState);
    } else {
      return <div />;
    }
  }
}
