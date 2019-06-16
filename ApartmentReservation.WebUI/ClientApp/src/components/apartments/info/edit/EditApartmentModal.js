import React, { Component } from "react";
import { Modal, Button } from "react-bootstrap";

export class EditApartmentModal extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      show: false,
      ...this.props
    };
  }

  get modalTitle() {
    return "Edit Apartment";
  }

  handleClose = () => {
    this.setState({ show: false });
  };

  handleShow = () => {
    this.setState({ show: true });
  };

  handleSubmit = () => {
    console.log(this.state);
    // todo: dispatch update apartment action...
    this.handleClose();
  };

  handleChange = event => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };

  render() {
    const { form: Component } = this.props;

    return (
      <div className="modal-edit-btn-container">
        <Button variant="warning" onClick={this.handleShow}>
          Edit
        </Button>

        <Modal show={this.state.show} onHide={this.handleClose}>
          <Modal.Header closeButton>
            <Modal.Title>{this.modalTitle}</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            {
              <Component
                handleChange={this.handleChange}
                handleSubmit={this.handleSubmit}
                {...this.state}
              />
            }
          </Modal.Body>
          <Modal.Footer>
            <Button variant="primary" onClick={this.handleSubmit}>
              Save Changes
            </Button>
            <Button variant="secondary" onClick={this.handleClose}>
              Close
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
}

export default EditApartmentModal;
