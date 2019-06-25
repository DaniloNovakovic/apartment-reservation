import "./Reservations.css";
import React, { Component } from "react";
import { connect } from "react-redux";
import { Spinner, Button, ButtonGroup } from "react-bootstrap";
import { FaSortAmountDown, FaSortAmountUp } from "react-icons/fa";
import { reservationService } from "../../services";
import ReservationsTable from "./ReservationsTable";
import ReservationsFilter from "./ReservationsFilter";
import { roleNames } from "../../constants";

const getSortedReservations = (reservations, sortAsc = false) => {
  let retVal = [...reservations];
  retVal.sort((x, y) => {
    return sortAsc ? x.totalCost - y.totalCost : y.totalCost - x.totalCost;
  });
  return retVal;
};

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
      const sorted = getSortedReservations(reservations, this.state.sortAsc);
      this.setState({
        loading: false,
        reservations: sorted
      });
    });
  };
  toggleSort = () => {
    let newSortAsc = !this.state.sortAsc;
    let reservations = getSortedReservations(
      this.state.reservations,
      newSortAsc
    );
    this.setState({ sortAsc: newSortAsc, reservations });
  };
  render() {
    const { loading, reservations = [], sortAsc } = this.state;
    const { user = {} } = this.props;

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
          <ButtonGroup>
            {(user.roleName === roleNames.Admin ||
              user.roleName === roleNames.Host) && (
              <ReservationsFilter handleSubmit={this.refreshData} />
            )}
            <Button variant="info" onClick={this.toggleSort}>
              {sortAsc ? <FaSortAmountUp /> : <FaSortAmountDown />} Sort by
              price
            </Button>
          </ButtonGroup>
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
