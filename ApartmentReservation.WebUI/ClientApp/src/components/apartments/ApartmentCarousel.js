import React from "react";
import { Carousel } from "react-bootstrap";

export const ApartmentCarousel = ({ images, className }) => {
  return (
    <Carousel className={`apartment-carousel ${className || ""}`}>
      {images &&
        images.map((img, index) => {
          return (
            <Carousel.Item key={`apartment-carousel-item-${index}`}>
              <img className="carousel-image" src={img.uri} alt="apartment" />
            </Carousel.Item>
          );
        })}
    </Carousel>
  );
};

export default ApartmentCarousel;
