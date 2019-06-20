import EditModalBase from "./EditModalBase";
import { connect } from "react-redux";
import { updateCurrentApartmentRentalDates } from "../../../../store/actions";

export class EditApartmentAvailabilityModal extends EditModalBase {
  get modalTitle() {
    return "Edit Apartment Availability (For Rental Dates)";
  }
  get btnText() {
    return "Edit For Rental Dates";
  }
  handleSubmit = () => {
    const { forRentalDates } = this.state.formData;
    const apartmentId = this.props.apartment.id;
    this.props
      .updateCurrentApartmentRentalDates({ id: apartmentId, forRentalDates })
      .then(_ => {
        this.handleClose();
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
  { updateCurrentApartmentRentalDates }
)(EditApartmentAvailabilityModal);
