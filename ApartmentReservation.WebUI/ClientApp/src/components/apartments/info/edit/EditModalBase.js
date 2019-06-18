import React, { Component } from "react";
import { Modal, Button } from "react-bootstrap";

// Generic class that holds `form` Component inside Modal body.
// You should inherit this class and override `handleSubmit` (and optionally modalTitle)
export class EditModalBase extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      show: false,
      formData: { ...this.props.formData, validation: {} }
    };
  }

  get btnText() {
    return "Edit";
  }
  get showModalBtn() {
    return (
      <Button variant="warning" onClick={this.handleShow}>
        {this.btnText}
      </Button>
    );
  }
  get submitBtnText() {
    return "Save Changes";
  }
  get cancelBtnText() {
    return "Close";
  }

  get modalTitle() {
    return "Edit";
  }

  handleClose = () => {
    this.setState({ show: false, formData: this.props.formData });
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
    let isValid = true;
    let validationMessage = "";

    if (event.target.checkValidity) {
      isValid = event.target.checkValidity();
      validationMessage = event.target.validationMessage;
    }

    this.setState({
      formData: {
        ...this.state.formData,
        [event.target.name]: event.target.value,
        validation: {
          ...this.state.formData.validation,
          [event.target.name]: {
            isValid,
            validationMessage
          }
        }
      }
    });
  };

  validate() {
    const { validation } = this.state.formData;
    for (let propName in validation) {
      if (!validation[propName].isValid) {
        console.log(`'${propName}' is invalid!`);
        return false;
      }
    }
    return true;
  }

  render() {
    const { form: Component } = this.props;

    return (
      <>
        {this.showModalBtn}

        <Modal size="lg" show={this.state.show} onHide={this.handleClose}>
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
            <Button
              variant="primary"
              onClick={() => {
                this.validate() && this.handleSubmit();
              }}
            >
              {this.submitBtnText}
            </Button>
            <Button variant="secondary" onClick={this.handleClose}>
              {this.cancelBtnText}
            </Button>
          </Modal.Footer>
        </Modal>
      </>
    );
  }
}

export default EditModalBase;
