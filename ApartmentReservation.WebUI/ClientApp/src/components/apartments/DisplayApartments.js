import "./DisplayApartments.css";
import React, { Component } from "react";
import ApartmentCard from "./ApartmentCard";
import { Spinner, ButtonGroup, Button } from "react-bootstrap";
import { FaSortAmountDown, FaSortAmountUp } from "react-icons/fa";
import { apartmentService } from "../../services";
import ApartmentsFilter from "./ApartmentsFilter";

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
    this.state = { apartments: [], loading: true, sortAsc: true };
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
  render() {
    const { apartments, loading, sortAsc } = this.state;
    const content = loading ? (
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : (
      apartments.map(apartment => {
        return (
          <ApartmentCard key={`apc-${apartment.id}`} apartment={apartment} />
        );
      })
    );

    return (
      <section>
        <ButtonGroup>
          <ApartmentsFilter
            handleSubmit={this.refreshData}
            filters={this.props.filters}
          />
          <Button variant="info" onClick={this.toggleSort}>
            {sortAsc ? <FaSortAmountUp /> : <FaSortAmountDown />} Sort by price
          </Button>
        </ButtonGroup>
        <main className="apartment-display">{content}</main>
      </section>
    );
  }
}

export default DisplayApartments;
