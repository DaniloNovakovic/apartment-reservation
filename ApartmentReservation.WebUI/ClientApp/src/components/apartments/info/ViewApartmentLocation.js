import React from "react";
import EditApartmentLocationModal from "./edit/EditApartmentLocationModal";
import EditApartmentLocationForm from "./edit/EditApartmentLocationForm";
import OpenLayersMap from "../../map/OpenLayersMap";

export default function ViewApartmentLocation({
  location = { address: {} },
  allowEdit = false
}) {
  let { longitude, latitude, address } = location;
  address = address || {};
  return (
    <article className="view-apartment-location">
      <h5>Location</h5>
      <div className="summary-content">
        <p>
          <b>Street Name:</b> {address.streetName}
        </p>
        <p>
          <b>Street Number:</b> {address.streetNumber}
        </p>
        <p>
          <b>City:</b> {address.cityName}
        </p>
        <p>
          <b>State:</b> {address.countryName}
        </p>
        <p>
          <b>Zip:</b> {address.postalCode}
        </p>
        <p>
          <b>Longitude:</b> {longitude}
        </p>
        <p>
          <b>Latitude:</b> {latitude}
        </p>
      </div>
      <OpenLayersMap lon={longitude} lat={latitude} readonly />
      {allowEdit && (
        <EditApartmentLocationModal
          formData={{ ...address, longitude, latitude }}
          form={EditApartmentLocationForm}
        />
      )}
    </article>
  );
}
