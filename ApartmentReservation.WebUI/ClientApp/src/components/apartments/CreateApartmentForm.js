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
//api geocode.xyz ?

/*
IEnumerable<AmenityDto> Amenities { get; set; }
IEnumerable<ForRentalDateDto> ForRentalDates { get; set; }
long HostId { get; set; }
*/

const countriesApi = "./countries.json";

export default class CreateApartmentForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      countries: [{ text: "Serbia", value: "RS" }]
    };
  }
  componentDidMount() {
    fetch(countriesApi)
      .then(req => req.json())
      .then(data => this.setState({ countries: data }));
  }
  handleSubmit = event => {
    event.preventDefault();
    console.log(this.state);
    this.props.handleSubmit(this.state);
  };
  handleChange = event => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };
  render() {
    const { countries } = this.state;
    return (
      <Form onSubmit={this.handleSubmit}>
        <Form.Row>
          <ApartmentTitleInput
            as={Col}
            sm="8"
            handleChange={this.handleChange}
          />
          <ApartmentTypeInput as={Col} handleChange={this.handleChange} />
        </Form.Row>

        <Form.Row>
          <NumberOfRoomsInput handleChange={this.handleChange} />
          <PricePerNightInput handleChange={this.handleChange} />
          <CheckInTimeInput as={Col} handleChange={this.handleChange} />
          <CheckOutTimeInput as={Col} handleChange={this.handleChange} />
        </Form.Row>

        <Form.Row>
          <StreetNameInput as={Col} sm="7" handleChange={this.handleChange} />
          <StreetNumberInput handleChange={this.handleChange} />
        </Form.Row>

        <Form.Row>
          <CityNameInput as={Col} handleChange={this.handleChange} />

          <Form.Group as={Col}>
            <Form.Label>State</Form.Label>
            <Form.Control
              as="select"
              name="countryName"
              onChange={this.handleChange}
            >
              {countries.map(country => {
                return (
                  <option key={`${country.value}`} value={country.text}>
                    {country.text}
                  </option>
                );
              })}
            </Form.Control>
          </Form.Group>

          <PostalCodeInput handleChange={this.handleChange} />
        </Form.Row>

        <Form.Row>
          <LongitudeInput handleChange={this.handleChange} />
          <LatitudeInput as={Col} handleChange={this.handleChange} />
        </Form.Row>

        <Button variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    );
  }
}
