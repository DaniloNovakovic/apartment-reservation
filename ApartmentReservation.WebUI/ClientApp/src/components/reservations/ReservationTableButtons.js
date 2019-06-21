import React, { Component } from "react";
import { reservationService } from "../../services";
import { mapRoleNameToJsx } from "./helpers";

export default class ReservationTableButtons extends Component {
  handleAccept = () => {
    const id = this.props.reservation.id;
    reservationService.accept(id).then(_ => window.location.reload());
  };
  handleWithdraw = () => {
    const id = this.props.reservation.id;
    reservationService.withdraw(id).then(_ => window.location.reload());
  };
  handleDeny = () => {
    const id = this.props.reservation.id;
    reservationService.deny(id).then(_ => window.location.reload());
  };
  handleComplete = () => {
    const id = this.props.reservation.id;
    reservationService.complete(id).then(_ => window.location.reload());
  };
  render() {
    const { user = {}, reservation = {} } = this.props;
    const roleName = user.roleName;
    const reservationState = reservation.reservationState;
    const onClickHandlers = {
      handleAccept: this.handleAccept,
      handleComplete: this.handleComplete,
      handleDeny: this.handleDeny,
      handleWithdraw: this.handleWithdraw
    };

    return mapRoleNameToJsx(roleName, reservationState, onClickHandlers);
  }
}
