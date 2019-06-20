import "./BookingCard.css";
import React, { Component } from "react";
import { Card, Button } from "react-bootstrap";
import ReactStars from "react-stars";

export default class BookingCard extends Component {
  constructor(props) {
    super(props);
    const { pricePerNight, rating, numberOfGuests } =
      this.props.apartment || {};

    this.state = {
      maxNumberOfGuests: numberOfGuests,
      pricePerNight,
      rating
    };
  }
  render() {
    return (
      <Card className="booking-card">
        <Card.Body>
          <Card.Title>
            <span className="price-digit">${this.state.pricePerNight}</span>{" "}
            <span className="price-letters">per night</span>
          </Card.Title>
          <Card.Subtitle
            as={ReactStars}
            count={5}
            edit={false}
            value={this.state.rating}
          />
          <hr />
        </Card.Body>
        <Card.Body>
          <Button variant="primary">Book</Button>
        </Card.Body>
      </Card>
    );
  }
}
