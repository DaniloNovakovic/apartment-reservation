import React from "react";
import ApartmentCarousel from "../ApartmentCarousel";
import AddImagesInput from "../form/AddImagesInput";
import { Button } from "react-bootstrap";

export function ViewApartmentImages({ images = [], allowEdit = false }) {
  return (
    <article>
      <h5>Images</h5>
      {images.length === 0 ? (
        <p>No images are currently available</p>
      ) : (
        <div>
          <ApartmentCarousel images={images} />
          <Button variant="info">View all</Button>
        </div>
      )}
      {allowEdit && <AddImagesInput />}
    </article>
  );
}

export default ViewApartmentImages;
