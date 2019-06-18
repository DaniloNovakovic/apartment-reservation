import EditModalBase from "./EditModalBase";
import { connect } from "react-redux";
import { updateCurrentApartmentAmenities } from "../../../../store/actions";

export class EditApartmentAmenitiesModal extends EditModalBase {
  get modalTitle() {
    return "Edit Apartment Amenities";
  }
  handleSubmit = () => {
    const { amenities } = this.state.formData;
    const apartmentId = this.props.apartment.id;
    console.log(this.modalTitle, amenities, apartmentId);
    this.props
      .updateCurrentApartmentAmenities({ id: apartmentId, amenities })
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
  { updateCurrentApartmentAmenities }
)(EditApartmentAmenitiesModal);
