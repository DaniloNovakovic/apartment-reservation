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
  handleChange = selectedItems => {
    if (this.props.handleChange) {
      this.props.handleChange(
        selectedItems.map(item => {
          return { id: item.value, name: item.label };
        })
      );
    }
  };
  render() {
    return (
      <AsyncSelect
        isMulti
        cacheOptions
        defaultOptions
        loadOptions={loadOptions}
        onChange={this.handleChange}
      />
    );
  }
}
