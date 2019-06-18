import EditModalBase from "./EditModalBase";
import { connect } from "react-redux";
import { apartmentService } from "../../../../services";

export class AddImagesModal extends EditModalBase {
  get modalTitle() {
    return "Add Images";
  }
  get btnText() {
    return "Add";
  }
  handleSubmit = () => {
    const { selectedFiles } = this.state.formData;
    const formData = new FormData();
    for (let file of selectedFiles) {
      formData.append("images", file, file.name);
    }

    const { id } = this.props.apartment;
    apartmentService
      .addImages(id, formData)
      .then(() => window.location.reload());
  };
}

const mapStateToProps = state => {
  return {
    apartment: state.apartment.currentApartment
  };
};
export default connect(mapStateToProps)(AddImagesModal);
