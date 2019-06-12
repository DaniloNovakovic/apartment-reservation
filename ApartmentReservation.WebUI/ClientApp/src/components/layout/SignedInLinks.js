import React from "react";
import { Glyphicon, NavItem, Nav } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { roleNames } from "../../constants";

const roleLinks = {
  [roleNames.Admin]: [
    <LinkContainer key="admin-link-users" to={"/Users"}>
      <NavItem>
        <Glyphicon glyph="list" /> Users
      </NavItem>
    </LinkContainer>
  ]
};

const SignedInLinks = ({ user }) => {
  return (
    <Nav>
      <LinkContainer to={"/"} exact>
        <NavItem>
          <Glyphicon glyph="home" /> Home
        </NavItem>
      </LinkContainer>
      {roleLinks[user.roleName]}
      <LinkContainer to={"/Profile"} exact>
        <NavItem>
          <Glyphicon glyph="user" /> Profile
        </NavItem>
      </LinkContainer>
      <LinkContainer to={"/Account/Logout"} exact>
        <NavItem>
          <Glyphicon glyph="log-out" /> Log-out
        </NavItem>
      </LinkContainer>
    </Nav>
  );
};

export default SignedInLinks;
