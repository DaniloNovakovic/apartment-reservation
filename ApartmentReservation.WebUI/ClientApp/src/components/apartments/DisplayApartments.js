import "./DisplayApartments.css";
import React, { Component } from "react";
import ApartmentCard from "./ApartmentCard";
import { Container, Spinner } from "react-bootstrap";
import { apartmentService } from "../../services";

export class DisplayApartments extends Component {
  constructor(props) {
    super(props);
    this.state = { apartments: [], loading: true };
  }
  componentDidMount() {
    this.refreshData();
  }
  refreshData = (filters = {}) => {
    this.setState({ loading: true });
    apartmentService.getAll(filters).then(data => {
      this.setState({ apartments: data, loading: false });
      console.log(data);
    });
  };
  render() {
    const { apartments, loading } = this.state;
    const content = loading ? (
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : (
      apartments.map((apartment, index) => {
        return <ApartmentCard key={`apc-${index}`} apartment={apartment} />;
      })
    );

    return <section className="apartment-display">{content}</section>;
  }
}

export default DisplayApartments;
