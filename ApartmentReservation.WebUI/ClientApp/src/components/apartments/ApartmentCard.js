import "./ApartmentCard.css";
import React, { Component } from "react";
import ApartmentCarousel from "./ApartmentCarousel";
import { Card, Row, Col } from "react-bootstrap";
import ReactStars from "react-stars";
import { Link } from "react-router-dom";

export class ApartmentCard extends Component {
  constructor(props) {
    super(props);

    let apartment = props.apartment || { images: [] };

    if (!apartment.images || apartment.images.length === 0) {
      apartment.images = [
        {
          uri: "/images/No_Image_Available.jpg"
        }
      ];
    }

    this.state = { apartment };
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
