import React from "react";
import DayPicker from "react-day-picker";
import "react-day-picker/lib/style.css";

function formatDateToString(date) {
  return `${date.getDay()}/${date.getMonth()}/${date.getYear()}`;
}
function isContainedIn(day, forRentalDates) {
  let dayStr = formatDateToString(day);
  for (let rentalDate of forRentalDates) {
    let rentalDateStr = formatDateToString(rentalDate);
    if (dayStr === rentalDateStr) {
      return true;
    }
  }
  return false;
}

export function ViewApartmentAvailability({
  forRentalDates = [],
  allowEdit = false
}) {
  forRentalDates = forRentalDates.map(item => new Date(item));
  return (
    <article className="view-availability">
      <h5>Availability</h5>
      <DayPicker
        disabledDays={day => !isContainedIn(day, forRentalDates)}
        selectedDays={forRentalDates}
      />
    </article>
  );
}

export default ViewApartmentAvailability;
