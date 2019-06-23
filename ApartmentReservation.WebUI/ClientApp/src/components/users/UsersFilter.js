import React, { Component } from "react";
import { Button, Modal } from "react-bootstrap";
import { FaFilter } from "react-icons/fa";
import { Form } from "react-bootstrap";
import {
  SelectInput,
  TextInput,
  mapObjToSelectOptions
} from "../baseFormHelpers";
import { roleNames, genders } from "../../constants";

export default class UsersFilter extends Component {
  constructor(props, context) {
    super(props, context);

    this.roleNameOptions = mapObjToSelectOptions(roleNames);
    this.genderOptions = mapObjToSelectOptions(genders);

    this.state = {
      show: false,
      filters: {
        roleName: "",
        gender: "",
        username: ""
      }
    };

    this.handleShow = () => {
      this.setState({ show: true });
    };

    this.handleHide = () => {
      this.setState({ show: false });
    };

    this.handleChange = ({ target = {} }) => {
      this.setState({
        ...this.state,
        filters: {
          ...this.state.filters,
          [target.name]: target.value
        }
      });
    };

    this.handleSubmit = () => {
      const { filters = {} } = this.state;
      if (this.props.handleSubmit) {
        this.props.handleSubmit(filters);
      }
      this.handleHide();
    };

    this.handleClear = () => {
      this.setState({
        filters: {
          roleName: "",
          gender: "",
          username: ""
        }
      });
    };
  }
  render() {
    const { roleName, gender, username } = this.state.filters;

    return (
      <>
        <Button variant="primary" onClick={this.handleShow}>
          <FaFilter /> Filter
        </Button>

        <Modal
          size="lg"
          show={this.state.show}
          onHide={this.handleHide}
          className="modal-users-filter"
        >
          <Modal.Header closeButton>
            <Modal.Title>
              Filter{" "}
              <span className="modal-subtitle">
                Apply one or more filters to all users on the list.
              </span>
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form onSubmit={this.handleSubmit}>
              <SelectInput
                label="Role Name"
                name="roleName"
                value={roleName}
                options={this.roleNameOptions}
                handleChange={this.handleChange}
              />
              <SelectInput
                label="Gender"
                name="gender"
                value={gender}
                options={this.genderOptions}
                handleChange={this.handleChange}
              />
              <TextInput
                label="Username"
                name="username"
                value={username}
                handleChange={this.handleChange}
              />
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button onClick={this.handleSubmit}>Accept</Button>
            <Button onClick={this.handleClear}>Clear</Button>
          </Modal.Footer>
        </Modal>
      </>
    );
  }
}
