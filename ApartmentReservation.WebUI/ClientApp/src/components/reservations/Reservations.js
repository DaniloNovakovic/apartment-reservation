import "./Reservations.css";
import React, { Component } from "react";
import { connect } from "react-redux";
import { Spinner } from "react-bootstrap";
import { reservationService } from "../../services";
import ReservationsTable from "./ReservationsTable";
import ReservationsFilter from "./ReservationsFilter";

export class Reservations extends Component {
  state = {
    loading: true,
    reservations: []
  };
  componentDidMount() {
    this.refreshData();
  }
  refreshData = (filters = {}) => {
    this.setState({ loading: true });
    reservationService.getAll(filters).then(reservations => {
      this.setState({ loading: false, reservations });
    });
  };
  render() {
    const { loading, reservations = [] } = this.state;
    const { user } = this.props;

    const content = loading ? (
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : (
      <ReservationsTable reservations={reservations} user={user} />
    );

    return (
      <section>
        <header>
          <h1>Reservations</h1>
        </header>
        <main>
          <ReservationsFilter handleSubmit={this.refreshData} user={user} />
          {content}
        </main>
      </section>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user
  };
};

export default connect(mapStateToProps)(Reservations);
