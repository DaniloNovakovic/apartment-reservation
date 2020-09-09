import "./ViewApartment.css";
import React, { Component } from "react";
import { Spinner, Row, Col, Button } from "react-bootstrap";
import { connect } from "react-redux";
import { apartmentService, commentService } from "../../../services";
import { ViewApartmentSummary } from "./ViewApartmentSummary";
import { ViewApartmentAmenities } from "./ViewApartmentsAmenities";
import { ViewApartmentAvailability } from "./ViewApartmentsAvailability";
import { ViewApartmentComments } from "./ViewApartmentComments";
import ViewApartmentImages from "./ViewApartmentImages";
import ViewApartmentLocation from "./ViewApartmentLocation";
import { setCurrentApartment, deleteApartment } from "../../../store/actions";
import { roleNames } from "../../../constants";
import BookingCard from "./BookingCard";

export class ViewApartment extends Component {
  constructor(props) {
    super(props);
    this.state = {
      apartmentId: props.match.params.id,
      canPostComments: false,
    };
  }
  setCurrApartment = (apartment) => {
    this.props.setCurrentApartment(apartment);
    this.setState({ loading: false });
  };

  componentDidMount() {
    this.setState({ loading: true });
    apartmentService.getById(this.state.apartmentId).then((apartment) => {
      this.setCurrApartment(apartment);
    });

    if (this.props.user) {
      commentService
        .canPostComment(this.state.apartmentId, this.props.user.id)
        .then((res) => {
          this.setState({ canPostComments: res.allowed });
        });
    }
  }
  handleDelete = () => {
    if (this.props.deleteApartment) {
      this.props.deleteApartment(this.state.apartmentId);
    }
  };
  render() {
    const { loading, canPostComments } = this.state;
    const { apartment = {}, user = {} } = this.props;
    const {
      location = { address: {} },
      images = [],
      amenities = [],
      forRentalDates = [],
      availableDates = [],
      comments = [],
    } = apartment;

    const isGuest = user && user.roleName === roleNames.Guest;
    const isHostOfCurrentApartment = apartment && user && user.id === apartment.hostId;

    const allowEdit =
      user && (user.roleName === roleNames.Admin || isHostOfCurrentApartment);

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
              comments={comments}
              canPostComments={canPostComments}
              canApprove={isHostOfCurrentApartment}
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

          <footer className="view-apartment-page-footer">
            {allowEdit && (
              <Button variant="danger" onClick={this.handleDelete}>
                Delete Apartment
              </Button>
            )}
          </footer>
        </Row>
      </>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    user: state.auth.user,
    apartment: state.apartment.currentApartment,
  };
};

export default connect(mapStateToProps, {
  setCurrentApartment,
  deleteApartment,
})(ViewApartment);
