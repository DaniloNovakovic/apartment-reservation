import React, { Component } from "react";
import { history } from "../../helpers";
import { Spinner } from "react-bootstrap";
import { amenitiesService } from "../../services";
import AmenityForm from "./AmenityForm";

export default class EditAmenity extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: true,
      amenityId: props.match.params.id,
      amenity: {}
    };
    amenitiesService.getById(this.state.amenityId).then(amenity => {
      this.setState({ loading: false, amenity });
    });
  }

  handleSubmit = amenity => {
    amenitiesService.update(amenity).then(_ => history.push("/Amenities"));
  };

  render() {
    return (
      <section>
        {this.state.loading ? (
          <Spinner animation="grow" variant="secondary" role="status">
            <span className="sr-only">Loading...</span>
          </Spinner>
        ) : (
          <AmenityForm
            amenity={this.state.amenity}
            handleSubmit={this.handleSubmit}
          />
        )}
      </section>
    );
  }
}
