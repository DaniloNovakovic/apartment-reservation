import React, { Component } from "react";
import { Button, Modal, Form, Col } from "react-bootstrap";
import { FaFilter } from "react-icons/fa";
import {
  mapObjToSelectOptions,
  TextInput,
  SelectInput,
  NumberInput
} from "../baseFormHelpers";
import { activityStates, apartmentTypes, roleNames } from "../../constants";
import { DayInput, CountryInput } from "./form";

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

    const { roleName } = this.props.user || {};

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
            <Form onSubmit={this.handleSubmit}>
              {(roleName === roleNames.Admin ||
                roleName === roleNames.Host) && (
                <SelectInput
                  label="Activity State"
                  name="activityState"
                  value={activityState || ""}
                  options={this.activityStateOptions}
                  handleChange={this.handleChange}
                />
              )}
              <Form.Row>
                <TextInput
                  as={Col}
                  sm={7}
                  label="Amenity Name"
                  name="amenityName"
                  value={amenityName || ""}
                  handleChange={this.handleChange}
                />
                <SelectInput
                  as={Col}
                  sm={5}
                  label="Apartment Type"
                  name="apartmentType"
                  value={apartmentType || ""}
                  options={this.apartmentTypeOptions}
                  handleChange={this.handleChange}
                />
              </Form.Row>
              <Form.Row>
                <TextInput
                  as={Col}
                  label="City Name"
                  name="cityName"
                  value={cityName || ""}
                  handleChange={this.handleChange}
                />
                <CountryInput
                  as={Col}
                  label="Country Name"
                  name="countryName"
                  value={countryName || ""}
                  handleChange={this.handleChange}
                />
              </Form.Row>
              <Form.Row>
                <DayInput
                  as={Col}
                  label="From Date"
                  name="fromDate"
                  value={fromDate}
                  disabledDays={{ before: new Date(), after: toDate }}
                  handleChange={this.handleChange}
                />
                <DayInput
                  as={Col}
                  label="To Date"
                  name="toDate"
                  value={toDate}
                  disabledDays={{ before: fromDate || new Date() }}
                  handleChange={this.handleChange}
                />
              </Form.Row>
              <Form.Row>
                <NumberInput
                  as={Col}
                  label="From Price ($)"
                  name="fromPrice"
                  value={fromPrice}
                  handleChange={this.handleChange}
                  min={0}
                  max={toPrice}
                />
                <NumberInput
                  as={Col}
                  label="To Price ($)"
                  name="toPrice"
                  value={toPrice}
                  handleChange={this.handleChange}
                  min={fromPrice}
                />
              </Form.Row>
              <Form.Row>
                <NumberInput
                  as={Col}
                  label="From Number of Rooms"
                  name="fromNumberOfRooms"
                  value={fromNumberOfRooms}
                  handleChange={this.handleChange}
                  min={0}
                  max={toNumberOfRooms}
                />
                <NumberInput
                  as={Col}
                  label="To Number of Rooms"
                  name="toNumberOfRooms"
                  value={toNumberOfRooms}
                  handleChange={this.handleChange}
                  min={fromNumberOfRooms}
                />
              </Form.Row>
              <NumberInput
                label="Number of Guests"
                name="numberOfGuests"
                value={numberOfGuests}
                handleChange={this.handleChange}
                min={1}
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
