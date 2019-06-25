import React, { Component } from "react";
import { Form, Button, Col } from "react-bootstrap";
import {
  ApartmentTitleInput,
  ApartmentTypeInput,
  NumberOfRoomsInput,
  PricePerNightInput,
  CheckInTimeInput,
  CheckOutTimeInput,
  NumberOfGuestsInput
} from "../form";

export class CreateApartmentInfoForm extends Component {
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
  render() {
    const {
      title = "",
      apartmentType = "",
      numberOfRooms = "",
      numberOfGuests = "",
      pricePerNight = "",
      checkInTime = "",
      checkOutTime = ""
    } = this.state;

    const { hidden = false } = this.props;

    return (
      <Form onSubmit={this.handleSubmit} className={hidden ? "d-none" : ""}>
        <ApartmentTitleInput value={title} handleChange={this.handleChange} />
        <Form.Row>
          <ApartmentTypeInput
            as={Col}
            value={apartmentType}
            handleChange={this.handleChange}
          />

          <PricePerNightInput
            as={Col}
            value={pricePerNight}
            handleChange={this.handleChange}
          />
        </Form.Row>
        <Form.Row>
          <NumberOfRoomsInput
            as={Col}
            value={numberOfRooms}
            handleChange={this.handleChange}
          />
          <NumberOfGuestsInput
            as={Col}
            value={numberOfGuests}
            handleChange={this.handleChange}
          />
        </Form.Row>
        <Form.Row>
          <CheckInTimeInput
            as={Col}
            value={checkInTime}
            handleChange={this.handleChange}
          />
          <CheckOutTimeInput
            as={Col}
            value={checkOutTime}
            handleChange={this.handleChange}
          />
        </Form.Row>
        <Button type="submit">Next</Button>
      </Form>
    );
  }
}

export default CreateApartmentInfoForm;
