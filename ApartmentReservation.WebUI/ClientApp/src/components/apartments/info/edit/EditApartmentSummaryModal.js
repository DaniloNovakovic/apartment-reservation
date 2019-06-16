import EditApartmentModal from "./EditApartmentModal";

export default class EditApartmentSummaryModal extends EditApartmentModal {
  get modalTitle() {
    return "Edit Apartment Summary";
  }
  handleSubmit = () => {
    console.log(this.modalTitle, this.state);
  };
}
