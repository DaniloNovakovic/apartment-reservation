import React, { Component } from "react";
import { Form, Alert, Col } from "react-bootstrap";
import {
  ActivityStateInput,
  ApartmentTitleInput,
  ApartmentTypeInput,
  NumberOfRoomsInput,
  NumberOfGuestsInput,
  PricePerNightInput,
  CheckInTimeInput,
  CheckOutTimeInput
} from "../../form";

export default class EditApartmentSummaryForm extends Component {
  handleSubmit = event => {
    event.preventDefault();
    this.props.handleSubmit();
  };
  handleChange = event => {
    this.props.handleChange(event);
  };
  render() {
    const { alert, validation = {} } = this.props;
    return (
      <Form validated={true} onSubmit={this.handleSubmit}>
        {alert && <Alert variant={alert.type}>{alert.message}</Alert>}
        <ApartmentTitleInput
          value={this.props.title || ""}
          handleChange={this.handleChange}
          feedback={validation.title && validation.title.validationMessage}
        />
        <Form.Row>
          <ApartmentTypeInput
            as={Col}
            value={this.props.apartmentType}
            handleChange={this.handleChange}
            feedback={
              validation.apartmentType &&
              validation.apartmentType.validationMessage
            }
          />
          <ActivityStateInput
            as={Col}
            value={this.props.activityState}
            handleChange={this.handleChange}
            feedback={
              validation.activityState &&
              validation.activityState.validationMessage
            }
          />
        </Form.Row>
        <Form.Row>
          <NumberOfRoomsInput
            as={Col}
            value={this.props.numberOfRooms || 0}
            handleChange={this.handleChange}
            feedback={
              validation.numberOfRooms &&
              validation.numberOfRooms.validationMessage
            }
          />
          <NumberOfGuestsInput
            as={Col}
            value={this.props.numberOfGuests || 0}
            handleChange={this.handleChange}
            feedback={
              validation.numberOfGuests &&
              validation.numberOfGuests.validationMessage
            }
          />
          <PricePerNightInput
            as={Col}
            value={this.props.pricePerNight || 0}
            handleChange={this.handleChange}
            feedback={
              validation.pricePerNight &&
              validation.pricePerNight.validationMessage
            }
          />
        </Form.Row>
        <Form.Row>
          <CheckInTimeInput
            as={Col}
            value={this.props.checkInTime}
            handleChange={this.handleChange}
            feedback={
              validation.checkInTime && validation.checkInTime.validationMessage
            }
          />
          <CheckOutTimeInput
            as={Col}
            value={this.props.checkOutTime}
            handleChange={this.handleChange}
            feedback={
              validation.checkOutTime &&
              validation.checkOutTime.validationMessage
            }
          />
        </Form.Row>
      </Form>
    );
  }
}
