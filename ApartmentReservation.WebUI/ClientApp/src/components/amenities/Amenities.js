import React, { Component } from "react";
import { Link } from "react-router-dom";
import { amenitiesService } from "../../services";
import AmenitiesTable from "./AmenitiesTable";
import { Spinner, Button, ButtonGroup } from "react-bootstrap";

export default class Amenities extends Component {
  constructor(props) {
    super(props);
    this.state = { amenities: [], loading: true };
  }
  componentDidMount() {
    this.refreshData();
  }
  refreshData = () => {
    this.setState({ loading: true });
    amenitiesService.getAll().then(data => {
      this.setState({ amenities: data, loading: false });
    });
  };
  deleteAmenityHandler = amenity => {
    amenitiesService.delete(amenity.id).then(_ => this.refreshData());
  };
  render() {
    let contents = this.state.loading ? (
      <Spinner animation="grow" variant="secondary" role="status">
        <span className="sr-only">Loading...</span>
      </Spinner>
    ) : (
      <AmenitiesTable
        amenities={this.state.amenities}
        deleteAmenityHandler={this.deleteAmenityHandler}
      />
    );

    return (
      <section>
        <header>
          <h1>Amenities</h1>
        </header>
        <main>
          <ButtonGroup aria-label="Add amenity">
            <Button as={Link} to="/add-amenity" variant="primary">
              Add Amenity
            </Button>
          </ButtonGroup>
          <br />
          {contents}
        </main>
      </section>
    );
  }
}
