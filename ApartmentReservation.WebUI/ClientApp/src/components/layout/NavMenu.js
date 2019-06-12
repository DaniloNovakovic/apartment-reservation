import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Navbar } from "react-bootstrap";
import "./NavMenu.css";
import SignedOutLinks from "./SignedOutLinks";
import SignedInLinks from "./SignedInLinks";
import { connect } from "react-redux";

export class NavMenu extends Component {
  displayName = NavMenu.name;

  render() {
    const { user } = this.props;
    const links =
      user && user.id ? <SignedInLinks user={user} /> : <SignedOutLinks />;

    return (
      <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to={"/"}>Apartment Reservation</Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>{links}</Navbar.Collapse>
      </Navbar>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user
  };
};

export default connect(mapStateToProps)(NavMenu);
