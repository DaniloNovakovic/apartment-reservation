import React from "react";
import DayPicker, { DateUtils } from "react-day-picker";
import EditApartmentAvailabilityModal from "./edit/EditApartmentAvailabilityModal";
import EditApartmentAvailabilityForm from "./edit/EditApartmentAvailabilityForm";

function isContainedIn(day, dates) {
  const index = dates.findIndex(rentalDate =>
    DateUtils.isSameDay(rentalDate, day)
  );
  return index >= 0;
}

export function ViewApartmentAvailability({
  forRentalDates = [],
  availableDates = [],
  allowEdit = false
}) {
  forRentalDates = forRentalDates.map(item => new Date(item));
  availableDates = availableDates.map(item => new Date(item));
  return (
    <article className="view-availability">
      <div>
        <h5>Availability</h5>
        <DayPicker
          numberOfMonths={2}
          pagedNavigation
          fromMonth={new Date()}
          disabledDays={[
            day => !isContainedIn(day, availableDates),
            { before: new Date() }
          ]}
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
