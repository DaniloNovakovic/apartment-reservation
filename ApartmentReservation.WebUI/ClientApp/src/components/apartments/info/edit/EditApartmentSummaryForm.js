import React, { Component } from "react";
import { Form, Alert, Col } from "react-bootstrap";
import {
  ActivityStateInput,
  ApartmentTitleInput,
  ApartmentTypeInput,
  NumberOfRoomsInput,
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
    const { alert } = this.props;
    return (
      <Form onSubmit={this.handleSubmit}>
        {alert && <Alert variant={alert.type}>{alert.message}</Alert>}
        <ApartmentTitleInput
          value={this.props.title || ""}
          handleChange={this.handleChange}
        />
        <Form.Row>
          <ApartmentTypeInput
            as={Col}
            value={this.props.apartmentType}
            handleChange={this.handleChange}
          />
          <ActivityStateInput
            as={Col}
            value={this.props.activityState}
            handleChange={this.handleChange}
          />
        </Form.Row>
        <Form.Row>
          <NumberOfRoomsInput
            as={Col}
            value={this.props.numberOfRooms || 0}
            handleChange={this.handleChange}
          />
          <PricePerNightInput
            as={Col}
            value={this.props.pricePerNight || 0}
            handleChange={this.handleChange}
          />
        </Form.Row>
        <Form.Row>
          <CheckInTimeInput
            as={Col}
            value={this.props.checkInTime}
            handleChange={this.handleChange}
          />
          <CheckOutTimeInput
            as={Col}
            value={this.props.checkOutTime}
            handleChange={this.handleChange}
          />
        </Form.Row>
      </Form>
    );
  }
}
