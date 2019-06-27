import React, { useState, useEffect } from "react";
import EditApartmentLocationModal from "./edit/EditApartmentLocationModal";
import OpenLayersMap from "../../map/OpenLayersMap";
import { countriesService } from "../../../services";

export default function ViewApartmentLocation({
  location = { address: {} },
  allowEdit = false
}) {
  const [countryName, setCountryName] = useState("");

  useEffect(() => {
    let promise = countriesService.mapCountryCodeToCountryName(
      location.address.countryName
    );
    promise
      .then(name => setCountryName(name))
      .catch(_ => {
        console.log("canceled promise.");
      });

    return () => {
      promise.cancel();
    };
  }, [location.address.countryName]);

  let { longitude, latitude, address } = location;
  address = address || {};
  return (
    <article className="view-apartment-location">
      <h5>Location</h5>
      <div className="location-details">
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
            <b>State:</b> {countryName}
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
        <OpenLayersMap
          lon={longitude}
          lat={latitude}
          markerLon={longitude}
          markerLat={latitude}
          readonly
        />
      </div>
      {allowEdit && (
        <EditApartmentLocationModal
          formData={{ ...address, longitude, latitude }}
        />
      )}
    </article>
  );
}
