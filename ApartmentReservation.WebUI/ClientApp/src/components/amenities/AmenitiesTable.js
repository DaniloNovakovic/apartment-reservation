import "./AmenitiesTable.css";
import React from "react";
import { Link } from "react-router-dom";
import { Table, ButtonToolbar, Button } from "react-bootstrap";

export const AmenitiesTable = ({ amenities, deleteAmenityHandler }) => (
  <Table striped hover bordered className="amenities-table">
    <thead>
      <tr>
        <th>name</th>
        <th />
      </tr>
    </thead>
    <tbody>
      {amenities.map(amenity => (
        <tr key={amenity.id}>
          <td>{amenity.name}</td>
          <td>
            <ButtonToolbar>
              <Button
                as={Link}
                to={`/edit-amenity/${amenity.id}`}
                variant="warning"
              >
                Edit
              </Button>
              <Button
                variant="danger"
                onClick={() => deleteAmenityHandler(amenity)}
              >
                Delete
              </Button>
            </ButtonToolbar>
          </td>
        </tr>
      ))}
    </tbody>
  </Table>
);

export default AmenitiesTable;
