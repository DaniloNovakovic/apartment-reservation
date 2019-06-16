import React from "react";
import { Link } from "react-router-dom";

/*
activityState: "Active"
amenities: []
apartmentType: "Full"
checkInTime: null
checkOutTime: null
comments: []
forRentalDates: []
host: {id: 2, username: "host", firstName: "Janica", lastName: "Janic", gender: "Female"}
id: 1
images: []
location: {id: 0, address: {…}, latitude: 45.267136, longitude: 19.833549}
numberOfGuests: 0
numberOfRooms: 1
pricePerNight: 0
rating: 0
reservations: []
title: null

*/

const ViewApartmentsHeader = props => (
  <header className="view-apartment-page-header">
    <h1>{props.title || "Apartment"}</h1>
    <p>{props.cityName}</p>
  </header>
);

export const ViewApartmentSummary = ({ apartment }) => {
  const { location, title, apartmentType, numberOfRooms } = apartment || {};
  const address = (location && location.address) || {};
  const host = apartment.host || {};

  return (
    <article className="view-apartment-page-summary">
      <ViewApartmentsHeader title={title} cityName={address.cityName} />
      <hr />
      <div>
        <p>
          <b>Type:</b> {apartmentType}
        </p>
        <p>
          <b>Number of rooms:</b> {numberOfRooms}
        </p>
        <Link to={`/?hostId=${host.id}`}>
          See more apartments from this host
        </Link>
      </div>
    </article>
  );
};
