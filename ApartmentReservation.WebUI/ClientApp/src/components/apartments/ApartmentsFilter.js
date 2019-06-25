import React, { Component } from "react";
import { Button, Modal, Form } from "react-bootstrap";
import { FaFilter } from "react-icons/fa";
import {
  mapObjToSelectOptions,
  TextInput,
  SelectInput
} from "../baseFormHelpers";
import { activityStates, apartmentTypes } from "../../constants";

export default class ApartmentsFilter extends Component {
  constructor(props, context) {
    super(props, context);

    this.activityStateOptions = mapObjToSelectOptions(activityStates);
    this.apartmentTypeOptions = mapObjToSelectOptions(apartmentTypes);

    this.state = {
      show: false,
      filters: this.props.filters || {}
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
        filters: {
          hostId: this.state.filters.hostId
        }
      });
      if (this.props.handleSubmit) {
        this.props.handleSubmit({});
      }
      this.handleHide();
    };
  }
  render() {
    const {
      activityState,
      amenityName,
      apartmentType,
      cityName,
      countryName,
      fromDate,
      toDate,
      fromPrice,
      toPrice,
      fromNumberOfRooms,
      toNumberOfRooms,
      numberOfGuests
    } = this.state.filters;

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
                Apply one or more filters to all apartments on the list.
              </span>
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            {/*
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }
        public int? FromNumberOfRooms { get; set; }
        public int? ToNumberOfRooms { get; set; }
        public int? NumberOfGuests { get; set; } */}
            <Form onSubmit={this.handleSubmit}>
              <SelectInput
                label="Activity State"
                name="activityState"
                value={activityState || ""}
                options={this.activityStateOptions}
                handleChange={this.handleChange}
              />
              <TextInput
                label="Amenity Name"
                name="amenityName"
                value={amenityName || ""}
                handleChange={this.handleChange}
              />
              <SelectInput
                label="Apartment Type"
                name="apartmentType"
                value={apartmentType || ""}
                options={this.apartmentTypeOptions}
                handleChange={this.handleChange}
              />
              <pre>{JSON.stringify(this.state.filters, null, 2)}</pre>
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
