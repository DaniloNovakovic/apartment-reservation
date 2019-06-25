import React, { Component } from "react";
import querystring from "querystring";
import DisplayApartments from "./apartments/DisplayApartments";

export class Home extends Component {
  displayName = Home.name;
  constructor(props) {
    super(props);
    this.filters = {};
    if (this.props.location && this.props.location.search) {
      this.filters = querystring.parse(this.props.location.search.slice(1));
    }
  }
  render() {
    return <DisplayApartments filters={this.filters} />;
  }
}
