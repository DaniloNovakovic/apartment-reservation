import "./EditApartmentLocationModal.css";
import { connect } from "react-redux";
import { updateCurrentApartment } from "../../../../store/actions";
import React, { Component } from "react";
import { Form, Button, Row, Col, ButtonGroup, Modal } from "react-bootstrap";
import {
  StreetNameInput,
  StreetNumberInput,
  CityNameInput,
  CountryInput,
  PostalCodeInput,
  LongitudeInput,
  LatitudeInput
} from "../../form";
import OpenLayersMap from "../../../map/OpenLayersMap";

const startLon = 19.833549;
const startLat = 45.267136;

class EditApartmentLocationModal extends Component {
  constructor(props, context) {
    super(props, context);

    this.state = {
      show: false,
      formData: this.props.formData || {}
    };

    this.olmRef = React.createRef();
  }
  componentDidUpdate(prevProps, prevState) {
    if (this.state.show !== prevState.show) {
      this.olmRef.current.forceUpdate();
    }
  }
  handleClose = () => {
    this.setState({ show: false });
  };
  handleCancel = () => {
    this.setState({ show: false, formData: this.props.formData || {} });
  };
  handleShow = () => {
    this.setState({ show: true });
  };

  handleSubmit = event => {
    event.preventDefault();
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

  handleChange = ({ target = {} }) => {
    this.setState({
      formData: {
        ...this.state.formData,
        [target.name]: target.value
      }
    });
  };
  handleMapClick = json => {
    const { lon, lat, address = {} } = json || {};
    this.setState({
      formData: {
        ...this.state.formData,
        cityName: address.city || "",
        countryName: address.country_code || "rs",
        postalCode: address.postcode || "",
        longitude: lon || startLon,
        latitude: lat || startLat,
        streetName: address.road || "",
        streetNumber: address.house_number || ""
      }
    });
  };
  render() {
    const { formData = {} } = this.state;
    const {
      streetName,
      streetNumber,
      cityName,
      countryName,
      postalCode,
      longitude = startLon,
      latitude = startLat
    } = formData;
    return (
      <>
        <Button variant="warning" onClick={this.handleShow}>
          Edit
        </Button>

        <Modal size="lg" show={this.state.show} onHide={this.handleCancel}>
          <Modal.Header closeButton>
            <Modal.Title>Edit Apartment Location</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form
              onSubmit={this.handleSubmit}
              className={`edit-location-form ${
                this.state.show ? "d-block" : "d-none"
              }`}
            >
              <Row>
                <Col>
                  <OpenLayersMap
                    lon={longitude}
                    lat={latitude}
                    markerLon={longitude}
                    markerLat={latitude}
                    onClick={this.handleMapClick}
                    ref={this.olmRef}
                  />
                </Col>
                <Col className="location-form-inputs">
                  <Form.Row>
                    <StreetNameInput
                      as={Col}
                      value={streetName || ""}
                      sm="8"
                      handleChange={this.handleChange}
                    />
                    <StreetNumberInput
                      as={Col}
                      value={streetNumber || ""}
                      sm="4"
                      handleChange={this.handleChange}
                    />
                  </Form.Row>
                  <CityNameInput
                    value={cityName || ""}
                    handleChange={this.handleChange}
                  />
                  <Form.Row>
                    <CountryInput
                      as={Col}
                      sm="7"
                      value={countryName || ""}
                      handleChange={this.handleChange}
                      required
                    />
                    <PostalCodeInput
                      as={Col}
                      sm="5"
                      value={postalCode || ""}
                      handleChange={this.handleChange}
                    />
                  </Form.Row>

                  <Form.Row>
                    <LongitudeInput
                      as={Col}
                      value={longitude || ""}
                      handleChange={this.handleChange}
                    />
                    <LatitudeInput
                      as={Col}
                      value={latitude || ""}
                      handleChange={this.handleChange}
                    />
                  </Form.Row>
                </Col>
              </Row>
              <ButtonGroup>
                <Button variant="secondary" onClick={this.handleCancel}>
                  Cancel
                </Button>
                <Button variant="primary" type="submit">
                  Save Changes
                </Button>
              </ButtonGroup>
            </Form>
          </Modal.Body>
        </Modal>
      </>
    );
  }
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
