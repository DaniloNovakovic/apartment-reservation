import EditModalBase from "./EditApartmentModal";
import { connect } from "react-redux";

export class EditApartmentSummaryModal extends EditModalBase {
  get modalTitle() {
    return "Edit Apartment Summary";
  }
  handleSubmit = () => {
    console.log(this.modalTitle, this.state);
  };
}

const mapStateToProps = state => {
  return {
    apartment: state.apartment.currentApartment
  };
};
export default connect(mapStateToProps)(EditApartmentSummaryModal);
