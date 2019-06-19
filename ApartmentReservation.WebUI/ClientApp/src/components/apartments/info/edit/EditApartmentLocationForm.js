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
    const { alert, validation = {} } = this.props;
    return (
      <Form validated={true} onSubmit={this.handleSubmit}>
        {alert && <Alert variant={alert.type}>{alert.message}</Alert>}
        <StreetNameInput
          value={this.props.streetName || ""}
          handleChange={this.handleChange}
          feedback={
            validation.streetName && validation.streetName.validationMessage
          }
        />
        <StreetNumberInput
          value={this.props.streetNumber || ""}
          handleChange={this.handleChange}
          feedback={
            validation.streetNumber && validation.streetNumber.validationMessage
          }
        />
        <Form.Row>
          <CityNameInput
            as={Col}
            value={this.props.cityName || ""}
            handleChange={this.handleChange}
            feedback={
              validation.cityName && validation.cityName.validationMessage
            }
          />
          <CountryInput
            as={Col}
            value={this.props.countryName || "Serbia"}
            handleChange={this.handleChange}
            feedback={
              validation.countryName && validation.countryName.validationMessage
            }
          />
          <PostalCodeInput
            as={Col}
            value={this.props.postalCode || ""}
            handleChange={this.handleChange}
            feedback={
              validation.postalCode && validation.postalCode.validationMessage
            }
          />
        </Form.Row>
        <Form.Row>
          <LongitudeInput
            as={Col}
            value={this.props.longitude || 0}
            handleChange={this.handleChange}
            feedback={
              validation.longitude && validation.longitude.validationMessage
            }
          />
          <LatitudeInput
            as={Col}
            value={this.props.latitude || 0}
            handleChange={this.handleChange}
            feedback={
              validation.latitude && validation.latitude.validationMessage
            }
          />
        </Form.Row>
      </Form>
    );
  }
}
