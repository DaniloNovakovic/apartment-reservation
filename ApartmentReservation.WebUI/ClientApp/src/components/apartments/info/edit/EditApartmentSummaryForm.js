import React, { Component } from "react";
import { Form } from "react-bootstrap";
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
    return (
      <Form onSubmit={this.handleSubmit}>
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
          value={this.props.numberOfRooms}
          handleChange={this.handleChange}
        />
      </Form>
    );
  }
}
