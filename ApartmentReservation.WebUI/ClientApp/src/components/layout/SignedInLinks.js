import React from "react";
import { Nav } from "react-bootstrap";
import { roleNames } from "../../constants";
import { IoIosHome, IoIosLogOut, IoIosList } from "react-icons/io";
import { Link } from "react-router-dom";

const roleLinks = {
  [roleNames.Admin]: [
    <Nav.Item key="admin-users-nav">
      <Nav.Link as={Link} to="/Users">
        <IoIosList />
        Users
      </Nav.Link>
    </Nav.Item>
  ]
};

const SignedInLinks = ({ user }) => {
  return (
    <Nav className="justify-content-end">
      {roleLinks[user.roleName]}
      <Nav.Item>
        <Nav.Link as={Link} to="/Profile">
          <IoIosHome />
          Profile
        </Nav.Link>
      </Nav.Item>

      <Nav.Item>
        <Nav.Link as={Link} to="/Account/Logout">
          <IoIosLogOut />
          Logout
        </Nav.Link>
      </Nav.Item>
    </Nav>
  );
};

export default SignedInLinks;
