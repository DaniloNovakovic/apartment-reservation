import React, { Component } from "react";
import Form from "react-bootstrap/Form";
import DayPicker, { DateUtils } from "react-day-picker";

export default class EditApartmentAvailabilityForm extends Component {
  handleChange = dates => {
    this.props.handleChange({
      target: { name: "forRentalDates", value: dates }
    });
  };
  handleDayClick = (day, { selected }) => {
    const { forRentalDates: selectedDays } = this.props;
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
    let { forRentalDates = [] } = this.props;
    forRentalDates = forRentalDates.map(item => new Date(item));
    return (
      <Form>
        <DayPicker
          selectedDays={forRentalDates}
          onDayClick={this.handleDayClick}
        />
      </Form>
    );
  }
}
