import EditModalBase from "./EditModalBase";
import { connect } from "react-redux";
import { updateCurrentApartment } from "../../../../store/actions";

export class EditApartmentLocationModal extends EditModalBase {
  get modalTitle() {
    return "Edit Apartment Location";
  }
  handleSubmit = () => {
    console.log(this.modalTitle, this.state);
    this.props
      .updateCurrentApartment({
        ...this.state.formData,
        id: this.props.apartment.id
      })
      .then(_ => {
        if (this.props.alert.type === "success") {
          this.handleClose();
        }
        window.location.reload();
      });
  };
}

const mapStateToProps = state => {
  return {
    apartment: state.apartment.currentApartment,
    alert: state.alert
  };
};
export default connect(
  mapStateToProps,
  { updateCurrentApartment }
)(EditApartmentLocationModal);
