import EditModalBase from "./EditApartmentModal";
import { connect } from "react-redux";
import { updateCurrentApartment } from "../../../../store/actions";

export class EditApartmentAmenitiesModal extends EditModalBase {
  get modalTitle() {
    return "Edit Apartment Amenities";
  }
  handleSubmit = () => {
    const { amenities } = this.state.formData;
    const apartmentId = this.props.apartment.id;
    console.log(amenities, apartmentId);
    // TODO: API request to update amenities for given apartment
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
)(EditApartmentAmenitiesModal);
