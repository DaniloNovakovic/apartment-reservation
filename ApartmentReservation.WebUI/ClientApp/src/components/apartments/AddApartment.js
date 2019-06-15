import React, { Component } from "react";
import { Alert } from "react-bootstrap";
import { connect } from "react-redux";
import CreateApartmentForm from "./CreateApartmentForm";

export class AddApartment extends Component {
  handleSubmit = apartment => {
    let hostId = this.props.user.id;
    // todo: make api call to add host
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

export default connect(mapStateToProps)(AddApartment);
