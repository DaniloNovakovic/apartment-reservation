import React from "react";
import { dateUtils } from "react-day-picker/utils";
import DayPickerInput from "react-day-picker/DayPickerInput";
import { Form } from "react-bootstrap";

function isContainedIn(day, dates) {
  const index = dates.findIndex(rentalDate =>
    dateUtils.isSameDay(rentalDate, day)
  );
  return index >= 0;
}

export class DayInput extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedDay: this.props.value
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
    let {
      availableDates = [],
      disabledDays,
      feedback,
      label = "Date",
      checkAvailableDates = true,
      handleChange,
      value,
      ...groupProps
    } = this.props;

    availableDates = availableDates.map(d => new Date(d));

    if (!disabledDays) {
      disabledDays = [
        day => checkAvailableDates && !isContainedIn(day, availableDates),
        { before: new Date() }
      ];
    }
    return (
      <Form.Group {...groupProps}>
        <Form.Label>{label}</Form.Label>
        <DayPickerInput
          value={selectedDay}
          onDayChange={this.handleDayChange}
          dayPickerProps={{
            selectedDays: selectedDay,
            disabledDays: disabledDays
          }}
        />
        <Form.Control.Feedback type="invalid">{feedback}</Form.Control.Feedback>
      </Form.Group>
    );
  }
}

export default DayInput;
