import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Glyphicon, Nav, Navbar, NavItem } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import "./NavMenu.css";

export class NavMenu extends Component {
  displayName = NavMenu.name;

  render() {
    return (
      <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to={"/"}>Apartment Reservation</Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>
          <Nav>
            <LinkContainer to={"/"} exact>
              <NavItem>
                <Glyphicon glyph="home" /> Home
              </NavItem>
            </LinkContainer>
            <LinkContainer to={"/Account/Register"} exact>
              <NavItem>
                <Glyphicon glyph="log-in" /> Register
              </NavItem>
            </LinkContainer>
            <LinkContainer to={"/Account/Login"} exact>
              <NavItem>
                <Glyphicon glyph="log-in" /> Log-in
              </NavItem>
            </LinkContainer>
            <LinkContainer to={"/Account/Logout"} exact>
              <NavItem>
                <Glyphicon glyph="log-out" /> Log-out
              </NavItem>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}
