import React, { Component } from "react";
import AsyncSelect from "react-select/async";
import { amenitiesService } from "../../../services";

const mapToAmenities = options =>
  options.map(item => {
    return { id: item.value, name: item.label };
  });

const mapToOptions = amenities =>
  amenities.map(amenity => {
    return { value: amenity.id, label: amenity.name };
  });

const loadOptions = (inputValue, callback) => {
  amenitiesService.getAll({ search: inputValue }).then(
    amenities => {
      callback(mapToOptions(amenities || []));
    },
    _ => {
      callback([]);
    }
  );
};

export default class AmenityMultiSelect extends Component {
  handleChange = selectedItems => {
    if (this.props.handleChange) {
      this.props.handleChange(mapToAmenities(selectedItems));
    }
  };
  render() {
    let { defaultValues = [] } = this.props;
    defaultValues = mapToOptions(defaultValues);

    return (
      <AsyncSelect
        isMulti
        cacheOptions
        defaultValue={defaultValues}
        defaultOptions
        loadOptions={loadOptions}
        onChange={this.handleChange}
      />
    );
  }
}
