import React, { Component } from "react";
import { Form, Button, Row, Col, ButtonGroup } from "react-bootstrap";
import {
  StreetNameInput,
  StreetNumberInput,
  CityNameInput,
  CountryInput,
  PostalCodeInput,
  LongitudeInput,
  LatitudeInput
} from "../form";
import OpenLayersMap from "../../map/OpenLayersMap";

const startLon = 19.833549;
const startLat = 45.267136;

export class CreateApartmentLocationForm extends Component {
  constructor(props) {
    super(props);
    this.state = { ...this.props.formData };
  }
  handleSubmit = event => {
    event.preventDefault();
    if (this.props.handleSubmit) {
      this.props.handleSubmit(this.state);
    }
  };
  handleChange = ({ target = {} }) => {
    this.setState({
      [target.name]: target.value
    });
  };
  handleBack = () => {
    if (this.props.handleBack) {
      this.props.handleBack();
    }
  };
  handleMapClick = json => {
    const { lon, lat, address = {} } = json || {};
    this.setState({
      cityName: address.city || "",
      countryName: address.country_code || "rs",
      postalCode: address.postcode || "",
      longitude: lon || startLon,
      latitude: lat || startLat,
      streetName: address.road || "",
      streetNumber: address.house_number || ""
    });
  };
  render() {
    const { hidden = false } = this.props;
    const {
      streetName,
      streetNumber,
      cityName,
      countryName,
      postalCode,
      longitude,
      latitude
    } = this.state;

    return (
      <Form
        onSubmit={this.handleSubmit}
        className={`location-form ${hidden ? "d-none" : "d-block"}`}
      >
        <Row>
          <Col>
            <OpenLayersMap
              lon={startLon}
              lat={startLat}
              onClick={this.handleMapClick}
            />
          </Col>
          <Col className="location-form-inputs">
            <Form.Row>
              <StreetNameInput
                as={Col}
                value={streetName || ""}
                sm="8"
                handleChange={this.handleChange}
              />
              <StreetNumberInput
                as={Col}
                value={streetNumber || ""}
                sm="4"
                handleChange={this.handleChange}
              />
            </Form.Row>
            <CityNameInput
              value={cityName || ""}
              handleChange={this.handleChange}
            />
            <Form.Row>
              <CountryInput
                as={Col}
                sm="7"
                value={countryName || ""}
                handleChange={this.handleChange}
                required
              />
              <PostalCodeInput
                as={Col}
                sm="5"
                value={postalCode || ""}
                handleChange={this.handleChange}
              />
            </Form.Row>

            <Form.Row>
              <LongitudeInput
                as={Col}
                value={longitude || ""}
                handleChange={this.handleChange}
              />
              <LatitudeInput
                as={Col}
                value={latitude || ""}
                handleChange={this.handleChange}
              />
            </Form.Row>
          </Col>
        </Row>
        <ButtonGroup>
          <Button variant="outline-primary" onClick={this.handleBack}>
            Back
          </Button>
          <Button variant="primary" type="submit">
            Next
          </Button>
        </ButtonGroup>
      </Form>
    );
  }
}

export default CreateApartmentLocationForm;
