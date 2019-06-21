import React from "react";
import { roleNames } from "../../constants";
import { Table, ButtonToolbar } from "react-bootstrap";
import { formatDateToYearMonthDayString } from "../../helpers";

export const ReservationsTable = ({ reservations, user }) => (
  <Table striped hover bordered className="reservations-table">
    <thead>
      <tr>
        {user.roleName !== roleNames.Guest && <th>Guest</th>}
        {user.roleName !== roleNames.Host && <th>Host</th>}
        <th>Apartment</th>
        <th>Date</th>
        <th>Number of Nights</th>
        <th>Total Cost (in $)</th>
        <th>Reservation state</th>
        <th />
      </tr>
    </thead>
    <tbody>
      {reservations.map(item => (
        <tr key={item.id}>
          {user.roleName !== roleNames.Guest && <td>{item.guest.username}</td>}
          {user.roleName !== roleNames.Host && (
            <td>{item.apartment.host.username}</td>
          )}
          <td>{item.apartment.title}</td>
          <td>{formatDateToYearMonthDayString(item.reservationStartDate)}</td>
          <td>{item.numberOfNightsRented}</td>
          <td>{item.totalCost}</td>
          <td>{item.reservationState}</td>
          <td>
            {user.roleName === roleNames.Guest && <ButtonToolbar />}
            {user.roleName === roleNames.Host && <ButtonToolbar />}
          </td>
        </tr>
      ))}
    </tbody>
  </Table>
);

export default ReservationsTable;
