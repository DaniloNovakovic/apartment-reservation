import React, { Component } from "react";
import { Form, Button, ButtonGroup } from "react-bootstrap";
import DayPicker, { DateUtils } from "react-day-picker";

export class CreateApartmentAvailability extends Component {
  constructor(props) {
    super(props);
    this.state = { ...this.props.formData };
  }
  handleSubmit = event => {
    event.preventDefault();
    if (this.props.handleSubmit) {
      this.props.handleSubmit(this.state);
    }
  };
  handleChange = selectedDays => {
    this.setState({
      forRentalDates: selectedDays
    });
  };
  handleBack = () => {
    if (this.props.handleBack) {
      this.props.handleBack();
    }
  };
  handleDayClick = (day, { selected }) => {
    const { forRentalDates = [] } = this.state;
    const selectedDays = [...forRentalDates];
    if (selected) {
      const selectedIndex = selectedDays.findIndex(selectedDay =>
        DateUtils.isSameDay(selectedDay, day)
      );
      selectedDays.splice(selectedIndex, 1);
    } else {
      selectedDays.push(day);
    }
    this.handleChange(selectedDays);
  };
  render() {
    const { forRentalDates = [] } = this.state;
    const { hidden = false } = this.props;

    return (
      <Form onSubmit={this.handleSubmit} className={hidden ? "d-none" : ""}>
        <h5>For rental dates</h5>
        <Form.Group>
          <DayPicker
            numberOfMonths={2}
            pagedNavigation
            fromMonth={new Date()}
            disabledDays={{ before: new Date() }}
            selectedDays={forRentalDates}
            onDayClick={this.handleDayClick}
          />
        </Form.Group>
        <ButtonGroup>
          <Button variant="outline-primary" onClick={this.handleBack}>
            Back
          </Button>
          <Button variant="primary" type="submit">
            Next
          </Button>
        </ButtonGroup>
      </Form>
    );
  }
}

export default CreateApartmentAvailability;
