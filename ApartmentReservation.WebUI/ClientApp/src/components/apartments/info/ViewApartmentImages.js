import React from "react";
import ApartmentCarousel from "../ApartmentCarousel";

export function ViewApartmentImages({ images = [] }) {
  return (
    <article>
      <h5>Images</h5>
      {images.length === 0 ? (
        <p>No images are currently available</p>
      ) : (
        <div>
          <ApartmentCarousel images={images} />
        </div>
      )}
    </article>
  );
}

export default ViewApartmentImages;
