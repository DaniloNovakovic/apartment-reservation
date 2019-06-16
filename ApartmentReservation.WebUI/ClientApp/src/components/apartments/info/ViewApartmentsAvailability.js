import React from "react";

export function ViewApartmentAvailability({ forRentalDates = [] }) {
  return (
    <article className="view-availability">
      <h5>Availability</h5>
      <p>Currently unavailable</p>
    </article>
  );
}

export default ViewApartmentAvailability;
