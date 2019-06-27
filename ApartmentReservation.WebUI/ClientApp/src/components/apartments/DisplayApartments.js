import "./DisplayApartments.css";
import React, { Component } from "react";
import { connect } from "react-redux";
import { Spinner, ButtonGroup, Button, Alert, Row, Col } from "react-bootstrap";
import { FaSortAmountDown, FaSortAmountUp } from "react-icons/fa";
import ApartmentCard from "./ApartmentCard";
import { apartmentService } from "../../services";
import ApartmentsFilter from "./ApartmentsFilter";
import OpenLayersMap from "../map/OpenLayersMap";
import { history } from "../../helpers";
import { activityStates } from "../../constants";

const getSortedApartments = (apartments, sortAsc = false) => {
  let retVal = [...apartments];
  retVal.sort((x, y) => {
    return sortAsc
      ? x.pricePerNight - y.pricePerNight
      : y.pricePerNight - x.pricePerNight;
  });
  return retVal;
};

export class DisplayApartments extends Component {
  constructor(props) {
    super(props);
    this.state = {
      apartments: [],
      loading: true,
      sortAsc: true,
      showMap: true
    };
  }
  componentDidMount() {
    this.refreshData(this.props.filters);
  }
  refreshData = (filters = {}) => {
    this.setState({ loading: true });
    apartmentService
      .getAll(filters)
      .then(data => {
        let sorted = getSortedApartments(data, this.state.sortAsc);
        this.setState({ apartments: sorted, loading: false });
      })
      .catch(err => {
        console.error(err);
        this.setState({ loading: false });
      });
  };
  toggleSort = () => {
    let newSortAsc = !this.state.sortAsc;
    let apartments = getSortedApartments(this.state.apartments, newSortAsc);
    this.setState({ sortAsc: newSortAsc, apartments });
  };
  toggleMap = () => {
    this.setState({ showMap: !this.state.showMap });
  };
  render() {
    const { apartments = [], loading, sortAsc, showMap } = this.state;
    const { user } = this.props;

    const markers = apartments.map(apartment => {
      const { longitude, latitude } = apartment.location || {};
      return {
        lon: longitude,
        lat: latitude,
        src:
          apartment.activityState === activityStates.Active
            ? "/images/bighouse.png"
            : "/images/bighouse_inactive.png",
        props: {
          id: apartment.id,
          onClick: () => {
            history.push(`/view-apartment/${apartment.id}`);
          }
        }
      };
    });

    const content = loading ? (
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : apartments.length > 0 ? (
      apartments.map(apartment => {
        return (
          <ApartmentCard key={`apc-${apartment.id}`} apartment={apartment} />
        );
      })
    ) : (
      <Alert variant="info">No apartments available</Alert>
    );

    return (
      <section>
        <ButtonGroup>
          <ApartmentsFilter
            handleSubmit={this.refreshData}
            filters={this.props.filters}
            user={user}
          />
          <Button variant="info" onClick={this.toggleSort}>
            {sortAsc ? <FaSortAmountUp /> : <FaSortAmountDown />} Sort by price
          </Button>

          <Button variant="outline-info" onClick={this.toggleMap}>
            {showMap ? "Hide" : "Show"} map
          </Button>
        </ButtonGroup>

        <Row as="section" className="apartment-display-wrapper">
          <Col
            as="main"
            sm={showMap ? "5" : "12"}
            className="apartment-display"
          >
            {content}
          </Col>
          {showMap && markers.length > 0 && (
            <Col as="aside" sm="7" className="apartment-map-display">
              <OpenLayersMap
                lat={51.044848}
                lon={12.074263}
                zoom={2}
                markers={markers}
                readonly
              />
            </Col>
          )}
        </Row>
      </section>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user
  };
};

export default connect(mapStateToProps)(DisplayApartments);
