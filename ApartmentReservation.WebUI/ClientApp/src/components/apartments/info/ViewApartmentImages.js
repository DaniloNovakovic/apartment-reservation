import React from "react";
import { ButtonGroup } from "react-bootstrap";
import ApartmentCarousel from "../ApartmentCarousel";
import AddImagesInput from "../form/AddImagesInput";
import AddImagesModal from "../info/edit/AddImagesModal";
import SelectImages from "../form/SelectImages";
import DeleteImagesModal from "./edit/DeleteImagesModal";

export function ViewApartmentImages({ images = [], allowEdit = false }) {
  return (
    <article className="view-apartment-images">
      <h5>Images</h5>
      {images.length === 0 ? (
        <p>No images are currently available</p>
      ) : (
        <div>
          <ApartmentCarousel images={images} />
          <ButtonGroup aria-label="Images Buttons">
            {allowEdit && <AddImagesModal form={AddImagesInput} />}
            {allowEdit && (
              <DeleteImagesModal formData={{ images }} form={SelectImages} />
            )}
          </ButtonGroup>
        </div>
      )}
    </article>
  );
}

export default ViewApartmentImages;
