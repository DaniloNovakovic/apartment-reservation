import React, { Component } from "react";
import { Modal, Button } from "react-bootstrap";

// Generic class that holds `form` Component inside Modal body.
// You should inherit this class and override `handleSubmit` (and optionally modalTitle)
export class EditModalBase extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      show: false,
      formData: this.props.formData
    };
  }

  get modalTitle() {
    return "Edit";
  }

  handleClose = () => {
    this.setState({ show: false });
  };

  handleShow = () => {
    this.setState({ show: true });
  };

  handleSubmit = () => {
    // you should override this method
    console.log(this.modalTitle, this.state);
    this.handleClose();
  };

  handleChange = event => {
    this.setState({
      formData: {
        ...this.state.formData,
        [event.target.name]: event.target.value
      }
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
                {...this.state.formData}
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

export default EditModalBase;
