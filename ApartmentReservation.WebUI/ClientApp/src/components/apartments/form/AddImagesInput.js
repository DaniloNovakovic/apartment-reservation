import React, { Component } from "react";

export class AddImagesInput extends Component {
  fileSelectedHandler = event => {
    if (this.props.handleChange) {
      this.props.handleChange({
        target: {
          name: "selectedFiles",
          value: event.target.files
        }
      });
    }
  };

  render() {
    return (
      <input
        type="file"
        name="images"
        onChange={this.fileSelectedHandler}
        multiple
      />
    );
  }
}

export default AddImagesInput;
