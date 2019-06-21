import React, { Component } from "react";
import { connect } from "react-redux";
import { Spinner } from "react-bootstrap";
import { reservationService } from "../../services";
import ReservationsTable from "./ReservationsTable";

export class Reservations extends Component {
  state = {
    loading: true,
    reservations: []
  };
  componentDidMount() {
    this.setState({ loading: true });
    reservationService.getAll().then(reservations => {
      this.setState({ loading: false, reservations });
    });
  }
  render() {
    const { loading, reservations = [] } = this.state;
    const { user } = this.props;
    return (
      <div>
        <h1>Reservations</h1>
        {loading ? (
          <Spinner animation="grow" variant="secondary" role="status">
            <span className="sr-only">Loading...</span>
          </Spinner>
        ) : (
          <ReservationsTable reservations={reservations} user={user} />
        )}
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user
  };
};

export default connect(mapStateToProps)(Reservations);
