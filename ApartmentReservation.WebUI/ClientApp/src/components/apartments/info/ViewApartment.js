import React, { Component } from "react";
import { Spinner } from "react-bootstrap";
import { connect } from "react-redux";
import { apartmentService } from "../../../services";
import { ViewApartmentSummary } from "./ViewApartmentSummary";
import { ViewApartmentAmenities } from "./ViewApartmentsAmenities";
import { ViewApartmentAvailability } from "./ViewApartmentsAvailability";
import { ViewApartmentComments } from "./ViewApartmentComments";
import ViewApartmentImages from "./ViewApartmentImages";

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
      images = [],
      amenities = [],
      comments = [],
      forRentalDates = []
    } = apartment;

    const { user = {} } = this.props;

    const allowEdit =
      apartment && apartment.host && user && user.id === apartment.host.id;

    return loading ? (
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : (
      <section className="view-apartment-page">
        <ViewApartmentSummary apartment={apartment} allowEdit={allowEdit} />
        <main>
          <hr />
          <ViewApartmentImages images={images} allowEdit={allowEdit} />
          <hr />
          <ViewApartmentAmenities amenities={amenities} allowEdit={allowEdit} />
          <hr />
          <ViewApartmentAvailability
            forRentalDates={forRentalDates}
            allowEdit={allowEdit}
          />
          <hr />
          <ViewApartmentComments comments={comments} allowEdit={allowEdit} />
        </main>
      </section>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user
  };
};

export default connect(mapStateToProps)(ViewApartment);
