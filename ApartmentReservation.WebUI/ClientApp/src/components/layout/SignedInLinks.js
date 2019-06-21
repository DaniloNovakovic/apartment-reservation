import React from "react";
import { Nav, NavDropdown } from "react-bootstrap";
import { roleNames } from "../../constants";
import { IoIosLogOut, IoIosList } from "react-icons/io";
import { Link } from "react-router-dom";

const roleLinks = {
  [roleNames.Admin]: [
    <Nav.Item key="admin-amenities-nav">
      <Nav.Link as={Link} to="/Amenities">
        <IoIosList />
        Amenities
      </Nav.Link>
    </Nav.Item>,
    <Nav.Item key="admin-users-nav">
      <Nav.Link as={Link} to="/Users">
        <IoIosList />
        Users
      </Nav.Link>
    </Nav.Item>
  ],
  [roleNames.Host]: [
    <Nav.Item key="host-add-apartment-nav">
      <Nav.Link as={Link} to="/add-apartment">
        Add Apartment
      </Nav.Link>
    </Nav.Item>
  ]
};

const SignedInLinks = ({ user }) => {
  return (
    <Nav>
      {roleLinks[user.roleName]}

      <Nav.Item key="reservations-nav">
        <Nav.Link as={Link} to="/reservations">
          <IoIosList />
          Reservations
        </Nav.Link>
      </Nav.Item>

      <NavDropdown title="Account" id="basic-nav-dropdown">
        <NavDropdown.Item as={Link} to="/Profile">
          Profile
        </NavDropdown.Item>
        <NavDropdown.Item as={Link} to="/Account/Logout">
          <IoIosLogOut />
          Logout
        </NavDropdown.Item>
      </NavDropdown>
    </Nav>
  );
};

export default SignedInLinks;
