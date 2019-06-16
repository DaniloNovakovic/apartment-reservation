import "./ApartmentCard.css";
import React, { Component } from "react";
import ApartmentCarousel from "./ApartmentCarousel";
import { Card, Row, Col } from "react-bootstrap";
import ReactStars from "react-stars";
import { Link } from "react-router-dom";

const defaultProps = {
  apartment: {
    id: 1,
    activityState: "Active",
    amenities: [
      { name: "TV" },
      { name: "Kitchen" },
      { name: "Bed" },
      { name: "Refrigirator" },
      { name: "Microwave" }
    ],
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
    pricePerNight: 35,
    rating: 3,
    reservations: []
  }
};

export class ApartmentCard extends Component {
  constructor(props) {
    super(props);

    this.state = { apartment: props.apartment || defaultProps.apartment };
    if (!this.state.apartment.images) {
      this.state.apartment.images = [
        {
          uri:
            "http://maestroselectronics.com/wp-content/uploads/2017/12/No_Image_Available.jpg"
        }
      ];
    }
  }
  render() {
    const { apartment } = this.state;
    const borderColor =
      apartment.activityState === "Active" ? "secondary" : "danger";

    return !apartment ? (
      <div />
    ) : (
      <div className="apartment-card">
        <Card
          border={borderColor}
          className="apartment-card-main d-inline-block"
        >
          <Card.Header as="div">
            <ApartmentCarousel images={apartment.images} />
          </Card.Header>
          <Card.Body>
            <Card.Title>{apartment.title}</Card.Title>
            <Card.Subtitle className="mb-2 text-muted text-truncate">
              {apartment.apartmentType}
            </Card.Subtitle>
            <Card.Text className="text-truncate">
              Number of rooms: {apartment.numberOfRooms}
            </Card.Text>
            <Card.Text className="text-truncate">
              {apartment.amenities
                .slice(0, 4)
                .map(amenity => amenity.name)
                .join(" · ")}
            </Card.Text>
          </Card.Body>
          <Card.Body>
            <Row>
              <Col>
                <ReactStars count={5} edit={false} value={apartment.rating} />
              </Col>
              <Col>
                <Card.Text className="text-right">
                  ${apartment.pricePerNight}/night
                </Card.Text>
              </Col>
            </Row>
          </Card.Body>
          <Card.Footer>
            <Card.Link as={Link} to={`/view-apartment/${apartment.id}`}>
              View more
            </Card.Link>
          </Card.Footer>
        </Card>
      </div>
    );
  }
}

export default ApartmentCard;
