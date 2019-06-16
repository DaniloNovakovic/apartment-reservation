import React from "react";

export const ViewApartmentAmenities = ({ amenities = [] }) => (
  <article className="view-amenities">
    <h5>Amenities</h5>
    {amenities.length === 0 ? (
      <p>No amenities are currently available</p>
    ) : (
      <ul className="view-amenities-list">
        {amenities.map((item, index) => {
          return <li key={item.name}>{item.name}</li>;
        })}
      </ul>
    )}
  </article>
);
