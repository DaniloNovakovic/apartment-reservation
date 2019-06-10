import React from "react";
import { Glyphicon, NavItem, Nav } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";

export default class SignedInLinks extends React.Component {
  render() {
    return (
      <Nav>
        <LinkContainer to={"/Account/Logout"} exact>
          <NavItem>
            <Glyphicon glyph="log-out" /> Log-out
          </NavItem>
        </LinkContainer>
      </Nav>
    );
  }
}
