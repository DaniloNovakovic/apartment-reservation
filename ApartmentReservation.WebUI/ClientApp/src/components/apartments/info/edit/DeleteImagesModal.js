import React from "react";
import { connect } from "react-redux";
import { Button } from "react-bootstrap";
import EditModalBase from "./EditModalBase";
import { apartmentService } from "../../../../services";

export class DeleteImagesModal extends EditModalBase {
  get modalTitle() {
    return "Delete Images";
  }
  get showModalBtn() {
    return (
      <Button variant="danger" onClick={this.handleShow}>
        Delete
      </Button>
    );
  }
  get submitBtnText() {
    return "Delete Selected";
  }
  handleSubmit = () => {
    const { selectedImages } = this.state.formData;
    const { id } = this.props.apartment;
    apartmentService
      .deleteImages(id, selectedImages)
      .then(() => window.location.reload());
  };
}

const mapStateToProps = state => {
  return {
    apartment: state.apartment.currentApartment
  };
};
export default connect(mapStateToProps)(DeleteImagesModal);
