import EditModalBase from "./EditApartmentModal";
import { connect } from "react-redux";
import { updateCurrentApartmentRentalDates } from "../../../../store/actions";

export class EditApartmentAvailabilityModal extends EditModalBase {
  get modalTitle() {
    return "Edit Apartment Availability";
  }
  handleSubmit = () => {
    const { forRentalDates } = this.state.formData;
    const apartmentId = this.props.apartment.id;
    this.props
      .updateCurrentApartmentRentalDates({ id: apartmentId, forRentalDates })
      .then(_ => this.handleClose());
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
  { updateCurrentApartmentRentalDates }
)(EditApartmentAvailabilityModal);
