import React from "react";
import EditApartmentAmenitiesModal from "./edit/EditApartmentAmenitiesModal";
import EditApartmentAmenitiesForm from "./edit/EditApartmentAmenitiesForm";

export const ViewApartmentAmenities = ({
  amenities = [],
  allowEdit = false
}) => (
  <article className="view-amenities">
    <div>
      <h5>Amenities</h5>
      {amenities.length === 0 ? (
        <p>No amenities are currently available</p>
      ) : (
        <ul className="view-amenities-list">
          {amenities.map(item => {
            return <li key={item.name}>{item.name}</li>;
          })}
        </ul>
      )}
    </div>
    {allowEdit && (
      <EditApartmentAmenitiesModal
        formData={{ amenities }}
        form={EditApartmentAmenitiesForm}
      />
    )}
  </article>
);
