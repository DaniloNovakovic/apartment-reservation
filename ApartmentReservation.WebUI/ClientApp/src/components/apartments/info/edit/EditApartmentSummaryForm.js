import React, { Component } from "react";
import { Form, Alert } from "react-bootstrap";
import {
  ApartmentTitleInput,
  ApartmentTypeInput,
  CityNameInput,
  NumberOfRoomsInput
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
        <CityNameInput
          value={this.props.cityName || ""}
          handleChange={this.handleChange}
        />
        <ApartmentTypeInput
          value={this.props.apartmentType}
          handleChange={this.handleChange}
        />
        <NumberOfRoomsInput
          value={this.props.numberOfRooms || 0}
          handleChange={this.handleChange}
        />
      </Form>
    );
  }
}
