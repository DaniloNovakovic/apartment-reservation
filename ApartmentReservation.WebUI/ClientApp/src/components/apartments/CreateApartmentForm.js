import React, { Component } from "react";
import { Form, Button, Col } from "react-bootstrap";
//api geocode.xyz ?

/*
IEnumerable<AmenityDto> Amenities { get; set; }
IEnumerable<ForRentalDateDto> ForRentalDates { get; set; }
long HostId { get; set; }
*/

export default class CreateApartmentForm extends Component {
  constructor(props) {
    super(props);
  }
  handleSubmit = event => {
    event.preventDefault();
    this.props.handleSubmit(this.state);
  };
  render() {
    return (
      <Form>
        <Form.Row>
          <Form.Group as={Col} sm="8">
            <Form.Label>Title</Form.Label>
            <Form.Control
              name="title"
              maxLength={50}
              placeholder="Enter title for apartment"
              required
            />
          </Form.Group>
          <Form.Group as={Col}>
            <Form.Label>Apartment Type</Form.Label>
            <Form.Control as="select" name="apartmentType" required>
              <option value="Full">Full</option>
              <option value="SingleRoom">Single Room</option>
            </Form.Control>
          </Form.Group>
        </Form.Row>

        <Form.Row>
          <Form.Group as={Col}>
            <Form.Label>Number of rooms</Form.Label>
            <Form.Control
              type="number"
              name="numberOfRooms"
              min="1"
              placeholder="ex. 2"
              required
            />
          </Form.Group>

          <Form.Group as={Col}>
            <Form.Label>Price($) per night</Form.Label>
            <Form.Control
              type="number"
              name="pricePerNight"
              min="0"
              placeholder="ex. 20"
              required
            />
          </Form.Group>

          <Form.Group as={Col}>
            <Form.Label>Check in time</Form.Label>
            <Form.Control type="time" name="pricePerNight" required />
          </Form.Group>

          <Form.Group as={Col}>
            <Form.Label>Check out time</Form.Label>
            <Form.Control type="time" name="pricePerNight" required />
          </Form.Group>
        </Form.Row>

        <Form.Row>
          <Form.Group as={Col} xm="7">
            <Form.Label>Street Name</Form.Label>
            <Form.Control name="streetName" placeholder="ex. Main street" />
          </Form.Group>
          <Form.Group as={Col} sm="5">
            <Form.Label>Street Number</Form.Label>
            <Form.Control name="streetNumber" placeholder="ex. 92" />
          </Form.Group>
        </Form.Row>

        <Form.Row>
          <Form.Group as={Col}>
            <Form.Label>City</Form.Label>
            <Form.Control name="cityName" placeholder="ex. Novi Sad" />
          </Form.Group>

          <Form.Group as={Col}>
            <Form.Label>State</Form.Label>
            <Form.Control as="select" name="countryName">
              <option>Choose...</option>
            </Form.Control>
          </Form.Group>

          <Form.Group as={Col}>
            <Form.Label>Zip</Form.Label>
            <Form.Control name="postalCode" placeholder="ex. 21102" required />
          </Form.Group>
        </Form.Row>

        <Form.Row>
          <Form.Group as={Col}>
            <Form.Label>Longitude</Form.Label>
            <Form.Control
              type="number"
              name="longitude"
              placeholder="ex. 40.754026"
            />
          </Form.Group>
          <Form.Group as={Col}>
            <Form.Label>Latitude</Form.Label>
            <Form.Control
              type="number"
              name="latitude"
              placeholder="ex. -73.956096"
            />
          </Form.Group>
        </Form.Row>

        <Button variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    );
  }
}
