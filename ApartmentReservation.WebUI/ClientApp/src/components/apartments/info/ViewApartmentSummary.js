import "./ViewApartment.css";
import React from "react";
import { Link } from "react-router-dom";
import EditApartmentModal from "./edit/EditApartmentSummaryModal";
import EditApartmentSummaryForm from "./edit/EditApartmentSummaryForm";

export const ViewApartmentsHeader = props => (
  <header className="view-apartment-page-header">
    <h1>{props.title || "Apartment"}</h1>
    <p>
      {props.cityName}, {props.activityState}
    </p>
  </header>
);

export const ViewApartmentSummary = ({ apartment, allowEdit = false }) => {
  const { location, title, apartmentType, numberOfRooms, activityState } =
    apartment || {};
  const address = (location && location.address) || {};
  const host = apartment.host || {};

  return (
    <article className="view-apartment-page-summary">
      <ViewApartmentsHeader
        title={title}
        cityName={address.cityName}
        activityState={activityState}
      />
      <hr />
      <div className="summary-content">
        <p>
          <b>Type:</b> {apartmentType}
        </p>
        <p>
          <b>Number of rooms:</b> {numberOfRooms}
        </p>
        <p>
          <b>Price:</b> ${apartment.pricePerNight} per night
        </p>
        <p>
          <b>Check-In Time:</b> {apartment.checkInTime}
        </p>
        <p>
          <b>Check-Out Time:</b> {apartment.checkOutTime}
        </p>

        <Link to={`/?hostId=${host.id}`}>
          See more apartments from this host
        </Link>
      </div>
      {allowEdit && (
        <EditApartmentModal
          formData={{
            ...apartment,
            cityName: address.cityName
          }}
          form={EditApartmentSummaryForm}
        />
      )}
    </article>
  );
};
