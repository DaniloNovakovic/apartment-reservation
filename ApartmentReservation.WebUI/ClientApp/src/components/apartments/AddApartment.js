import React, { Component } from "react";
import { Alert } from "react-bootstrap";
import { connect } from "react-redux";
import CreateApartmentForm from "./CreateApartmentForm";
import { createApartment } from "../../store/actions";

export class AddApartment extends Component {
  handleSubmit = apartment => {
    let hostId = this.props.user.id;
    this.props.createApartment(hostId, apartment);
  };

  render() {
    const { alert } = this.props;
    return (
      <section>
        <h1>Add Apartment</h1>
        <main>
          {alert && alert.message ? (
            <Alert variant={alert.type}>{alert.message}</Alert>
          ) : null}
          <CreateApartmentForm handleSubmit={this.handleSubmit} />
        </main>
      </section>
    );
  }
}

const mapStateToProps = state => {
  return {
    alert: state.alert,
    user: state.auth.user
  };
};

export default connect(
  mapStateToProps,
  { createApartment }
)(AddApartment);
