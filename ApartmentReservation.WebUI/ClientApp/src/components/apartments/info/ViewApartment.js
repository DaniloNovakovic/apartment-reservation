import "./ViewApartment.css";
import React, { Component } from "react";
import { Spinner, Row, Col } from "react-bootstrap";
import { connect } from "react-redux";
import { apartmentService, commentService } from "../../../services";
import { ViewApartmentSummary } from "./ViewApartmentSummary";
import { ViewApartmentAmenities } from "./ViewApartmentsAmenities";
import { ViewApartmentAvailability } from "./ViewApartmentsAvailability";
import { ViewApartmentComments } from "./ViewApartmentComments";
import ViewApartmentImages from "./ViewApartmentImages";
import ViewApartmentLocation from "./ViewApartmentLocation";
import { setCurrentApartment } from "../../../store/actions";
import { roleNames } from "../../../constants";
import BookingCard from "./BookingCard";

export class ViewApartment extends Component {
  constructor(props) {
    super(props);
    this.state = {
      apartmentId: props.match.params.id,
      canPostComments: false
    };
  }
  setCurrApartment = apartment => {
    this.props.setCurrentApartment(apartment);
    this.setState({ loading: false });
  };

  componentDidMount() {
    this.setState({ loading: true });
    apartmentService.getById(this.state.apartmentId).then(apartment => {
      this.setCurrApartment(apartment);
    });
    commentService
      .canPostComment(this.state.apartmentId, this.props.user.id)
      .then(res => {
        this.setState({ canPostComments: res.allowed });
      });
  }
  render() {
    const { loading, canPostComments } = this.state;
    const { apartment = {}, user = {} } = this.props;
    const {
      location = { address: {} },
      images = [],
      amenities = [],
      forRentalDates = [],
      availableDates = []
    } = apartment;

    const isGuest = user && user.roleName === roleNames.Guest;

    const allowEdit =
      user &&
      (user.roleName === roleNames.Admin ||
        (apartment && apartment.host && user.id === apartment.host.id));

    return loading ? (
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : (
      <>
        <Row as="section" className="view-apartment-page">
          <Col as="main" sm={isGuest ? "7" : "12"}>
            <ViewApartmentSummary apartment={apartment} allowEdit={allowEdit} />
            <hr />
            <ViewApartmentImages images={images} allowEdit={allowEdit} />
            <hr />
            <ViewApartmentAmenities
              amenities={amenities}
              allowEdit={allowEdit}
            />
            <hr />
            <ViewApartmentAvailability
              forRentalDates={forRentalDates}
              availableDates={availableDates}
              allowEdit={allowEdit}
            />
            <hr />
            <ViewApartmentComments
              apartment={apartment}
              canPostComments={canPostComments}
            />
            <hr />
            <ViewApartmentLocation location={location} allowEdit={allowEdit} />
            <hr />
          </Col>
          {isGuest && (
            <Col as="aside" sm="5" className="booking-part">
              <BookingCard apartment={apartment} user={user} />
            </Col>
          )}
        </Row>
        <footer />
      </>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user,
    apartment: state.apartment.currentApartment
  };
};

export default connect(
  mapStateToProps,
  { setCurrentApartment }
)(ViewApartment);
