import React from "react";
import { Glyphicon, NavItem, Nav } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";

const SignedOutLinks = () => {
  return (
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
    </Nav>
  );
};

export default SignedOutLinks;
