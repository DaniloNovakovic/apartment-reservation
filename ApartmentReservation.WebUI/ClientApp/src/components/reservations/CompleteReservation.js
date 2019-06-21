import React from "react";
import { Button } from "react-bootstrap";
import { areSameDay } from "../../helpers";

function calculateEndDate(reservationStartDate, numberOfNightsRented) {
  let date = new Date(reservationStartDate);
  date.setDate(date.getDate() + numberOfNightsRented);
  return date;
}

export default function CompleteReservation({ reservation, handleComplete }) {
  const { reservationStartDate, numberOfNightsRented } = reservation;
  const endDate = calculateEndDate(reservationStartDate, numberOfNightsRented);
  const today = new Date();

  return areSameDay(today, endDate) || today >= endDate ? (
    <Button variant="success" onClick={handleComplete}>
      Complete
    </Button>
  ) : (
    <div />
  );
}
