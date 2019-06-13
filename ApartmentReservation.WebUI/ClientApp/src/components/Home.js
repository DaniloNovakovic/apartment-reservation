import React, { Component } from "react";
import Apartment from "./apartments/Apartment";

export class Home extends Component {
  displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Apartment reservation</h1>
        <Apartment />
      </div>
    );
  }
}
