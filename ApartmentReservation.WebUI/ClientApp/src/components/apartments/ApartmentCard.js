import "./ApartmentCard.css";
import React, { Component } from "react";
import ApartmentCarousel from "./ApartmentCarousel";
import { Container, Row, Col, Card } from "react-bootstrap";

const defaultProps = {
  apartment: {
    id: 1,
    activityState: "Active",
    amenities: [],
    apartmentType: "Full",
    title: "Magnificent apartment",
    checkInTime: null,
    checkOutTime: null,
    comments: [],
    forRentalDates: [],
    host: null,
    images: [
      {
        uri:
          "https://www.onni.com/wp-content/uploads/2016/11/Rental-Apartment-Page-new-min.jpg"
      },
      {
        uri:
          "https://arystudios.files.wordpress.com/2015/08/3dcontemperoryapartmentrenderingarchitecturalduskviewrealisticarystudios.jpg"
      },
      {
        uri:
          "https://www.travelonline.com/melbourne/city-cbd/accommodation/adina-apartment-hotel-melbourne-flinders-street/penthouse-76880.jpg"
      }
    ],
    location: {
      id: 0,
      address: {
        id: 1,
        cityName: "Novi Sad",
        postalCode: "21102",
        streetName: "Bulevar kralja Petra",
        streetNumber: "25"
      },
      latitude: 45.267136,
      longitude: 19.833549
    },
    numberOfGuests: 0,
    numberOfRooms: 1,
    pricePerNight: 0,
    rating: null,
    reservations: []
  }
};

export class ApartmentCard extends Component {
  constructor(props) {
    super(props);

    this.state = { apartment: props.apartment || defaultProps.apartment };
  }
  render() {
    const { apartment } = this.state;

    return !apartment ? (
      <div />
    ) : (
      <div className="apartment-card">
        <Card className="apartment-card-main d-inline-block">
          <Card.Header as="div">
            <ApartmentCarousel images={apartment.images} />
          </Card.Header>
          <Card.Body>
            <Card.Title className="text-truncate">{apartment.title}</Card.Title>
            <Card.Subtitle className="mb-2 text-muted text-truncate">
              {apartment.apartmentType}
            </Card.Subtitle>
            <Card.Text className="text-truncate">
              Number of rooms: {apartment.numberOfRooms}
            </Card.Text>
          </Card.Body>
        </Card>
      </div>
    );
  }
}

export default ApartmentCard;
