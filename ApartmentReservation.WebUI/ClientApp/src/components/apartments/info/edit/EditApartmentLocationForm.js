import React, { Component } from "react";
import { Form, Alert, Col } from "react-bootstrap";
import {
  StreetNameInput,
  StreetNumberInput,
  CityNameInput,
  CountryInput,
  PostalCodeInput,
  LongitudeInput,
  LatitudeInput
} from "../../form";

export default class EditApartmentLocationForm extends Component {
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
        <StreetNameInput
          value={this.props.streetName || ""}
          handleChange={this.handleChange}
        />
        <StreetNumberInput
          value={this.props.streetNumber || ""}
          handleChange={this.handleChange}
        />
        <Form.Row>
          <CityNameInput
            as={Col}
            value={this.props.cityName || ""}
            handleChange={this.handleChange}
          />
          <CountryInput
            as={Col}
            value={this.props.countryName || "Serbia"}
            handleChange={this.handleChange}
          />
          <PostalCodeInput
            as={Col}
            value={this.props.postalCode || ""}
            handleChange={this.handleChange}
          />
        </Form.Row>
        <Form.Row>
          <LongitudeInput
            as={Col}
            value={this.props.longitude || 0}
            handleChange={this.handleChange}
          />
          <LatitudeInput
            as={Col}
            value={this.props.latitude || 0}
            handleChange={this.handleChange}
          />
        </Form.Row>
      </Form>
    );
  }
}
