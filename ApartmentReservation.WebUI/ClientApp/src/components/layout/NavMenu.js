import "./NavMenu.css";
import React, { Component } from "react";
import { Navbar } from "react-bootstrap";
import SignedOutLinks from "./SignedOutLinks";
import SignedInLinks from "./SignedInLinks";
import { connect } from "react-redux";
import { IoIosBed } from "react-icons/io";
import { Link } from "react-router-dom";

export class NavMenu extends Component {
  displayName = NavMenu.name;

  render() {
    const { user } = this.props;
    const links =
      user && user.id ? <SignedInLinks user={user} /> : <SignedOutLinks />;

    return (
      <Navbar bg="light" variant="light" expand="lg" fixed="top">
        <Navbar.Brand as={Link} to="/">
          <IoIosBed />
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse className="justify-content-end" id="basic-navbar-nav">
          {links}
        </Navbar.Collapse>
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
