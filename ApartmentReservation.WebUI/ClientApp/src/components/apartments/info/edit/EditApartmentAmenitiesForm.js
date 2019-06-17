import React, { Component } from "react";
import AmenityMultiSelect from "../../form/AmenityMultiSelect";
import { Form } from "react-bootstrap";

export default class EditApartmentAmenitiesForm extends Component {
  handleChange = selectedAmenities => {
    this.props.handleChange({
      target: { name: "amenities", value: selectedAmenities }
    });
  };
  render() {
    const { amenities = [] } = this.props;

    return (
      <Form>
        <AmenityMultiSelect
          defaultValues={amenities}
          handleChange={this.handleChange}
        />
      </Form>
    );
  }
}
