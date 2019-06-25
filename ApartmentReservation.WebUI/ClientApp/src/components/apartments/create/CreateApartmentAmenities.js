import React, { Component } from "react";
import { Form, Button, ButtonGroup } from "react-bootstrap";
import { AmenityMultiSelect } from "../form";

export class CreateApartmentAmenities extends Component {
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
  handleAmenityChange = selectedAmenities => {
    this.setState({
      amenities: selectedAmenities
    });
  };
  handleBack = () => {
    if (this.props.handleBack) {
      this.props.handleBack();
    }
  };
  render() {
    const { amenities = [] } = this.state;
    const { hidden = false } = this.props;

    return (
      <Form onSubmit={this.handleSubmit} className={hidden ? "d-none" : ""}>
        <Form.Group>
          <Form.Label>Amenities</Form.Label>
          <AmenityMultiSelect
            defaultValues={amenities}
            handleChange={this.handleAmenityChange}
          />
        </Form.Group>
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

export default CreateApartmentAmenities;
