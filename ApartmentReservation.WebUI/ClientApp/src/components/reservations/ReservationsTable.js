import React from "react";
import { roleNames } from "../../constants";
import { Table } from "react-bootstrap";
import { formatDateToYearMonthDayString } from "../../helpers";
import ReservationTableButtons from "./ReservationTableButtons";

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
          {user.roleName !== roleNames.Guest && <td>{item.guestUsername}</td>}
          {user.roleName !== roleNames.Host && (
            <td>{item.hostUsername}</td>
          )}
          <td>{item.apartmentTitle}</td>
          <td>{formatDateToYearMonthDayString(item.reservationStartDate)}</td>
          <td>{item.numberOfNightsRented}</td>
          <td>{item.totalCost}</td>
          <td>{item.reservationState}</td>
          <td>
            <ReservationTableButtons user={user} reservation={item} />
          </td>
        </tr>
      ))}
    </tbody>
  </Table>
);

export default ReservationsTable;
