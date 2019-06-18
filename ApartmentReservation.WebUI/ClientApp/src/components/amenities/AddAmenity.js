import React, { Component } from "react";
import { history } from "../../helpers";
import { amenitiesService } from "../../services";
import AmenityForm from "./AmenityForm";

export default class AddAmenity extends Component {
  handleSubmit = amenity => {
    amenitiesService.create(amenity).then(_ => history.push("/Amenities"));
  };
  render() {
    return <AmenityForm handleSubmit={this.handleSubmit} />;
  }
}
