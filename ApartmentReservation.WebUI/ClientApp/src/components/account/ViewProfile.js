import React from "react";
import { Button } from "react-bootstrap";

const defaultProps = {
  user: {},
  handleEditClick: () => {}
};

export const ViewProfile = (props = defaultProps) => (
  <div>
    <p>
      <b>username:</b> {props.user.username}
    </p>
    <p>
      <b>password:</b> {props.user.password}
    </p>
    <p>
      <b>firstName:</b> {props.user.firstName}
    </p>
    <p>
      <b>lastName:</b> {props.user.lastName}
    </p>
    <p>
      <b>gender:</b> {props.user.gender}
    </p>
    <p>
      <b>roleName:</b> {props.user.roleName}
    </p>
    <Button variant="primary" onClick={props.handleEditClick}>
      Edit
    </Button>
  </div>
);

export default ViewProfile;
