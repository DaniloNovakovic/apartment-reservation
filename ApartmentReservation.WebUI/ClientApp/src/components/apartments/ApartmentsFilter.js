import React, { Component } from "react";
import { Button, Modal, Form } from "react-bootstrap";
import { FaFilter } from "react-icons/fa";
import {
  mapObjToSelectOptions,
  TextInput,
  SelectInput
} from "../baseFormHelpers";
import { reservationStates } from "../../constants";

export default class ReservationsFilter extends Component {
  constructor(props, context) {
    super(props, context);

    this.reservationStateOptions = mapObjToSelectOptions(reservationStates);

    this.state = {
      show: false,
      filters: {}
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

    this.handleSubmit = event => {
      if (event) {
        event.preventDefault();
      }
      const { filters = {} } = this.state;
      if (this.props.handleSubmit) {
        this.props.handleSubmit(filters);
      }
      this.handleHide();
    };

    this.handleClear = () => {
      this.setState({
        filters: {}
      });
      if (this.props.handleSubmit) {
        this.props.handleSubmit({});
      }
      this.handleHide();
    };
  }
  render() {
    const { reservationState, guestUsername } = this.state.filters;
    return (
      <>
        <Button variant="primary" onClick={this.handleShow}>
          <FaFilter /> Filter
        </Button>

        <Modal
          size="lg"
          show={this.state.show}
          onHide={this.handleHide}
          className="modal-reservations-filter"
        >
          <Modal.Header closeButton>
            <Modal.Title>
              Filter{" "}
              <span className="modal-subtitle">
                Apply one or more filters to all reservations on the list.
              </span>
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form onSubmit={this.handleSubmit} />
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
