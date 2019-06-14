import React from "react";
import { Carousel } from "react-bootstrap";

export const ApartmentCarousel = ({ images }) => {
  return (
    <Carousel>
      {images.map((img, index) => {
        <Carousel.Item key={`apartment-carousel-img-${index}`}>
          <img
            className="apartment-carousel-img w-100"
            src={img}
            alt="apartment image"
          />
        </Carousel.Item>;
      })}
    </Carousel>
  );
};

export default ApartmentCarousel;
