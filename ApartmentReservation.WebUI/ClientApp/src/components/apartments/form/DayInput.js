import React from "react";
import { DateUtils } from "react-day-picker";
import DayPickerInput from "react-day-picker/DayPickerInput";
import { Form } from "react-bootstrap";

function isContainedIn(day, dates) {
  const index = dates.findIndex(rentalDate =>
    DateUtils.isSameDay(rentalDate, day)
  );
  return index >= 0;
}

export default class DayInput extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedDay: undefined
    };
  }

  handleDayChange = selectedDay => {
    this.setState({
      selectedDay
    });
  };

  render() {
    const { selectedDay } = this.state;
    let { availableDates = [] } = this.props;
    availableDates = availableDates.map(d => new Date(d));

    return (
      <Form.Group>
        <Form.Label>Date</Form.Label>
        <DayPickerInput
          value={selectedDay}
          onDayChange={this.handleDayChange}
          dayPickerProps={{
            selectedDays: selectedDay,
            disabledDays: day => !isContainedIn(day, availableDates)
          }}
        />
      </Form.Group>
    );
  }
}
