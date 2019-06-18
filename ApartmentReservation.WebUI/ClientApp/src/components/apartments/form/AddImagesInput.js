import React, { Component } from "react";
import { Button } from "react-bootstrap";
import { connect } from "react-redux";
import { apartmentService } from "../../../services";

export class AddImagesInput extends Component {
  state = {
    selectedFiles: null
  };
  fileSelectedHandler = event => {
    this.setState({
      selectedFiles: event.target.files
    });
  };
  fileUploadHandler = () => {
    const formData = new FormData();
    for (let file of this.state.selectedFiles) {
      formData.append("images", file, file.name);
    }

    const { id } = this.props.apartment;
    apartmentService.addImages(id, formData);
  };

  render() {
    return (
      <div>
        <input
          type="file"
          name="images"
          onChange={this.fileSelectedHandler}
          multiple
        />
        <Button variant="primary" onClick={this.fileUploadHandler}>
          Upload
        </Button>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    apartment: state.apartment.currentApartment,
    alert: state.alert
  };
};

export default connect(mapStateToProps)(AddImagesInput);
