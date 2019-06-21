import "./BookingCard.css";
import React, { Component } from "react";
import { Card, Button, Form } from "react-bootstrap";
import ReactStars from "react-stars";
import { NumberOfGuestsInput, NumberOfNightsInput } from "../form";
import DayInput from "../form/DayInput";
import { calculateMaxNumberOfNights } from "../../../helpers";
import { reservationService } from "../../../services";

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
  handleChange = event => {
    let isValid = true;
    let validationMessage = "";

    if (event.target.checkValidity) {
      isValid = event.target.checkValidity();
      validationMessage = event.target.validationMessage;
    }

    this.setState({
      ...this.state,
      [event.target.name]: event.target.value,
      validation: {
        ...this.state.validation,
        [event.target.name]: {
          isValid,
          validationMessage
        }
      }
    });
  };

  handleSubmit = event => {
    event.preventDefault();
    const form = event.currentTarget;
    if (
      form.checkValidity() &&
      this.state.selectedDay &&
      this.props.apartment &&
      this.props.user
    ) {
      reservationService
        .post({
          apartmentId: this.props.apartment.id,
          guestId: this.props.user.id,
          startDate: this.state.selectedDay,
          numberOfNights: this.state.numberOfNights
        })
        .then(
          response => {
            window.location.reload();
          },
          err => {
            this.setState({ alertMessage: JSON.toString() });
          }
        );
    }

    this.setState({ validated: true });
  };
  render() {
    const {
      maxNumberOfGuests,
      pricePerNight,
      rating,
      availableDates = [],
      selectedDay,
      validation = {}
    } = this.state;

    const maxNumberOfNights =
      selectedDay && calculateMaxNumberOfNights(selectedDay, availableDates);

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
          <Form onSubmit={this.handleSubmit} validated={this.state.validated}>
            <DayInput
              availableDates={availableDates}
              handleChange={this.handleChange}
            />
            {maxNumberOfNights >= 1 ? (
              <NumberOfNightsInput
                min={1}
                max={maxNumberOfNights}
                label={`Number of nights (max=${maxNumberOfNights})`}
                value={this.state.numberOfNights || ""}
                handleChange={this.handleChange}
                feedback={
                  validation.numberOfNights &&
                  validation.numberOfNights.validationMessage
                }
              />
            ) : (
              <p>Please select date...</p>
            )}
            <NumberOfGuestsInput
              min={1}
              max={maxNumberOfGuests}
              label={`Number of guests (max=${maxNumberOfGuests})`}
              value={this.state.numberOfGuests || ""}
              handleChange={this.handleChange}
              feedback={
                validation.numberOfGuests &&
                validation.numberOfGuests.validationMessage
              }
            />
            <Button type="submit" variant="primary" size="lg" block>
              Book
            </Button>
          </Form>
        </Card.Body>
      </Card>
    );
  }
}
