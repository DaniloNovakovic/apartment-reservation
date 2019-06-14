import React, { Component } from "react";
import ApartmentCard from "./ApartmentCard";
import { Container } from "react-bootstrap";

export class DisplayApartments extends Component {
  render() {
    return (
      <div>
        <ApartmentCard />
      </div>
    );
  }
}

export default DisplayApartments;
