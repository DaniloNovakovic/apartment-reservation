import "./CreateApartmentForm.css";
import React, { Component } from "react";
import {
  CreateApartmentInfoForm,
  CreateApartmentLocationForm,
  CreateApartmentAmenities,
  CreateApartmentAvailability
} from "./create";

const isDebug = false;

export default class CreateApartmentForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      checkInTime: "14:00:00",
      checkOutTime: "10:00:00",
      apartmentType: "Full",
      currFormIndex: 0
    };
  }
  handleComplete = data => {
    const finalData = {
      ...this.state,
      ...data
    };
    if (this.props.handleSubmit) {
      this.props.handleSubmit(finalData);
    }
    this.setState({
      ...finalData,
      currFormIndex: this.state.currFormIndex + 1
    });
  };
  handleNext = data => {
    const { currFormIndex } = this.state;
    this.setState({ ...data, currFormIndex: currFormIndex + 1 });
  };
  handleBack = () => {
    const { currFormIndex } = this.state;
    this.setState({ currFormIndex: currFormIndex - 1 });
  };
  handleChange = event => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };
  render() {
    const { currFormIndex } = this.state;
    return (
      <div className="create-apartment-forms">
        {isDebug && (
          <>
            <button onClick={() => this.handleBack()}>{"<"}</button>
            <button onClick={() => this.handleNext()}>{">"}</button>
          </>
        )}
        <CreateApartmentInfoForm
          hidden={currFormIndex !== 0}
          handleSubmit={this.handleNext}
          formData={{ ...this.state }}
        />
        <CreateApartmentLocationForm
          hidden={currFormIndex !== 1}
          handleSubmit={this.handleNext}
          handleBack={this.handleBack}
          formData={{ ...this.state }}
        />
        <CreateApartmentAmenities
          hidden={currFormIndex !== 2}
          handleSubmit={this.handleNext}
          handleBack={this.handleBack}
          formData={{ ...this.state }}
        />
        <CreateApartmentAvailability
          hidden={currFormIndex !== 3}
          handleSubmit={this.handleComplete}
          handleBack={this.handleBack}
          formData={{ ...this.state }}
        />
      </div>
    );
  }
}
