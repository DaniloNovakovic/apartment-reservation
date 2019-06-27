import React, { Component } from "react";
import Form from "react-bootstrap/Form";
import DayPicker, { DateUtils } from "react-day-picker/DayPicker";

export default class EditApartmentAvailabilityForm extends Component {
  handleChange = dates => {
    this.props.handleChange({
      target: { name: "forRentalDates", value: dates }
    });
  };
  handleDayClick = (day, { selected }) => {
    const { forRentalDates } = this.props;
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
    let { forRentalDates = [] } = this.props;
    forRentalDates = forRentalDates.map(item => new Date(item));
    return (
      <Form>
        <DayPicker
          numberOfMonths={2}
          pagedNavigation
          fromMonth={new Date()}
          disabledDays={{ before: new Date() }}
          selectedDays={forRentalDates}
          onDayClick={this.handleDayClick}
        />
      </Form>
    );
  }
}
