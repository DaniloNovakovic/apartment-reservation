import React, { Component } from "react";
import DisplayApartments from "./apartments/DisplayApartments";

export class Home extends Component {
  displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Apartment reservation</h1>
        <DisplayApartments />
      </div>
    );
  }
}
