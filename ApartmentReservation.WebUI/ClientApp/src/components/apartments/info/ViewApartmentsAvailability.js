import React from "react";
import DayPicker, { DateUtils } from "react-day-picker";
import EditApartmentAvailabilityModal from "./edit/EditApartmentAvailabilityModal";
import EditApartmentAvailabilityForm from "./edit/EditApartmentAvailabilityForm";

function isContainedIn(day, forRentalDates) {
  const index = forRentalDates.findIndex(rentalDate =>
    DateUtils.isSameDay(rentalDate, day)
  );
  return index >= 0;
}

export function ViewApartmentAvailability({
  forRentalDates = [],
  allowEdit = false
}) {
  forRentalDates = forRentalDates.map(item => new Date(item));
  return (
    <article className="view-availability">
      <div>
        <h5>Availability</h5>
        <DayPicker
          numberOfMonths={2}
          pagedNavigation
          disabledDays={day => !isContainedIn(day, forRentalDates)}
          selectedDays={forRentalDates}
        />
      </div>
      {allowEdit && (
        <EditApartmentAvailabilityModal
          formData={{ forRentalDates }}
          form={EditApartmentAvailabilityForm}
        />
      )}
    </article>
  );
}

export default ViewApartmentAvailability;
