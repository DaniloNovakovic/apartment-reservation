import React, { Component } from "react";
import { Spinner } from "react-bootstrap";
import { apartmentService } from "../../../services";
import { ViewApartmentSummary } from "./ViewApartmentSummary";
import { ViewApartmentAmenities } from "./ViewApartmentsAmenities";
import { ViewApartmentAvailability } from "./ViewApartmentsAvailability";
import { ViewApartmentComments } from "./ViewApartmentComments";

export class ViewApartment extends Component {
  constructor(props) {
    super(props);
    this.state = {
      apartmentId: props.match.params.id,
      apartment: {}
    };
  }
  componentDidMount() {
    this.setState({ loading: true });
    apartmentService.getById(this.state.apartmentId).then(apartment => {
      console.log(apartment);
      this.setState({ loading: false, apartment });
    });
  }
  render() {
    const { apartment, loading } = this.state;
    const {
      location = {},
      images = [],
      amenities = [],
      comments = [],
      forRentalDates = []
    } = apartment;
    const { address = {} } = location;

    return loading ? (
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : (
      <section className="view-apartment-page">
        <ViewApartmentSummary apartment={apartment} />
        <main>
          <hr />
          <ViewApartmentAmenities amenities={amenities} />
          <hr />
          <ViewApartmentAvailability forRentalDates={forRentalDates} />
          <hr />
          <ViewApartmentComments comments={comments} />
        </main>
      </section>
    );
  }
}

export default ViewApartment;
