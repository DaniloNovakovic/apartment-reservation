import "./BookingCard.css";
import React, { Component } from "react";
import { Card, Button, Form } from "react-bootstrap";
import ReactStars from "react-stars";
import { NumberOfGuestsInput, NumberOfNightsInput } from "../form";
import DayInput from "../form/DayInput";

export default class BookingCard extends Component {
  constructor(props) {
    super(props);
    const { pricePerNight, rating, numberOfGuests, availableDates = [] } =
      this.props.apartment || {};

    this.state = {
      maxNumberOfGuests: numberOfGuests,
      pricePerNight,
      rating,
      availableDates
    };
  }
  render() {
    const {
      maxNumberOfGuests,
      pricePerNight,
      rating,
      availableDates = []
    } = this.state;

    return (
      <Card className="booking-card">
        <Card.Body className="price-wrapper">
          <Card.Title>
            <span className="price-digit">${pricePerNight}</span>{" "}
            <span className="price-letters">per night</span>
          </Card.Title>
          <Card.Subtitle
            as={ReactStars}
            count={5}
            edit={false}
            value={rating}
          />
          <hr />
        </Card.Body>
        <Card.Body className="form-wrapper">
          <Form>
            <DayInput availableDates={availableDates} />
            <NumberOfNightsInput />
            <NumberOfGuestsInput max={maxNumberOfGuests} />
            <Button type="input" variant="primary" size="lg" block>
              Book
            </Button>
          </Form>
        </Card.Body>
      </Card>
    );
  }
}
