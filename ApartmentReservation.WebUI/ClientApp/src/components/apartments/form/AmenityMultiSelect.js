import React, { Component } from "react";
import AsyncSelect from "react-select/async";
import { amenitiesService } from "../../../services";

const loadOptions = (inputValue, callback) => {
  amenitiesService.getAll({ search: inputValue }).then(
    amenities => {
      const options = amenities.map(amenity => {
        return { value: amenity.id, label: amenity.name };
      });

      callback(options);
    },
    _ => {
      callback([]);
    }
  );
};

export default class AmenityMultiSelect extends Component {
  constructor(props) {
    super(props);
    this.state = { selected: [] };
  }
  handleChange = newValue => {
    this.setState({ selected: newValue });
  };
  render() {
    return (
      <div>
        <AsyncSelect
          isMulti
          cacheOptions
          defaultOptions
          loadOptions={loadOptions}
          onChange={this.handleChange}
        />
        <pre>{JSON.stringify(this.state.selected, null, 2)}</pre>
      </div>
    );
  }
}
