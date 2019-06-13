import React from "react";
import { Nav } from "react-bootstrap";
import { IoIosLogIn } from "react-icons/io";
import { Link } from "react-router-dom";

const SignedOutLinks = () => {
  return (
    <Nav className="justify-content-end">
      <Nav.Item>
        <Nav.Link as={Link} to="/Account/Register">
          <IoIosLogIn />
          Register
        </Nav.Link>
      </Nav.Item>
      <Nav.Item>
        <Nav.Link as={Link} to="/Account/Login">
          <IoIosLogIn />
          <span>Log-in</span>
        </Nav.Link>
      </Nav.Item>
    </Nav>
  );
};

export default SignedOutLinks;
