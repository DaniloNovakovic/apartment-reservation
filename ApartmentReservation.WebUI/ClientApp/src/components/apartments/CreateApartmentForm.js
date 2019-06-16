import React, { Component } from "react";
import { Form, Button, Col } from "react-bootstrap";
import { StreetNameInput, ApartmentTypeInput } from "./form";
import { ApartmentTitleInput } from "./form/ApartmentTitleInput";
import { NumberOfRoomsInput } from "./form/NumberOfRoomsInput";
import { PricePerNightInput } from "./form/PricePerNightInput";
import { CheckInTimeInput } from "./form/CheckInTimeInput";
import { CheckOutTimeInput } from "./form/CheckOutTimeInput";
import { StreetNumberInput } from "./form/StreetNumberInput";
import { CityNameInput } from "./form/CityNameInput";
import { PostalCodeInput } from "./form/PostalCodeInput";
import { LongitudeInput } from "./form/LongitudeInput";
import { LatitudeInput } from "./form/LatitudeInput";
import { CountryInput } from "./form/CountryInput";
//api geocode.xyz ?

/*
IEnumerable<AmenityDto> Amenities { get; set; }
IEnumerable<ForRentalDateDto> ForRentalDates { get; set; }
long HostId { get; set; }
*/

export default class CreateApartmentForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      checkInTime: "14:00:00",
      checkOutTime: "10:00:00",
      apartmentType: "Full"
    };
  }
  handleSubmit = event => {
    event.preventDefault();
    this.props.handleSubmit(this.state);
  };
  handleChange = event => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };
  render() {
    return (
      <Form onSubmit={this.handleSubmit}>
        <Form.Row>
          <ApartmentTitleInput
            as={Col}
            sm="8"
            handleChange={this.handleChange}
          />
          <ApartmentTypeInput
            as={Col}
            value={this.state.apartmentType}
            handleChange={this.handleChange}
          />
        </Form.Row>

        <Form.Row>
          <NumberOfRoomsInput as={Col} handleChange={this.handleChange} />
          <PricePerNightInput as={Col} handleChange={this.handleChange} />
          <CheckInTimeInput
            as={Col}
            value={this.state.checkInTime}
            handleChange={this.handleChange}
          />
          <CheckOutTimeInput
            as={Col}
            value={this.state.checkOutTime}
            handleChange={this.handleChange}
          />
        </Form.Row>

        <Form.Row>
          <StreetNameInput as={Col} sm="7" handleChange={this.handleChange} />
          <StreetNumberInput as={Col} sm="5" handleChange={this.handleChange} />
        </Form.Row>

        <Form.Row>
          <CityNameInput as={Col} handleChange={this.handleChange} />
          <CountryInput as={Col} handleChange={this.handleChange} />
          <PostalCodeInput as={Col} handleChange={this.handleChange} />
        </Form.Row>

        <Form.Row>
          <LongitudeInput as={Col} handleChange={this.handleChange} />
          <LatitudeInput as={Col} handleChange={this.handleChange} />
        </Form.Row>

        <Button variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    );
  }
}