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
    if (this.props.handleChange) {
      this.props.handleChange({
        target: {
          name: this.props.name || "selectedDay",
          value: selectedDay,
          checkValidity: () => !!selectedDay,
          validationMessage: "Please select date"
        }
      });
    }
  };

  render() {
    const { selectedDay } = this.state;
    let { availableDates = [], feedback } = this.props;
    availableDates = availableDates.map(d => new Date(d));

    return (
      <Form.Group>
        <Form.Label>Date</Form.Label>
        <DayPickerInput
          value={selectedDay}
          onDayChange={this.handleDayChange}
          dayPickerProps={{
            selectedDays: selectedDay,
            disabledDays: [
              day => !isContainedIn(day, availableDates),
              { before: new Date() }
            ]
          }}
        />
        <Form.Control.Feedback type="invalid">{feedback}</Form.Control.Feedback>
      </Form.Group>
    );
  }
}
