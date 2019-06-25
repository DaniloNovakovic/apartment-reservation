import React, { Component } from "react";
import { Form, Button, Col, ButtonGroup } from "react-bootstrap";
import {
  StreetNameInput,
  StreetNumberInput,
  CityNameInput,
  CountryInput,
  PostalCodeInput,
  LongitudeInput,
  LatitudeInput
} from "../form";

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
  render() {
    const { hidden = false } = this.props;

    return (
      <Form onSubmit={this.handleSubmit} className={hidden ? "d-none" : ""}>
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
