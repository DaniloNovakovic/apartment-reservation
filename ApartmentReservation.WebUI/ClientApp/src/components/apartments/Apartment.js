import React, { Component } from "react";

const defaultProps = {
  apartment: {
    id: 1,
    activityState: "Active",
    amenities: [],
    apartmentType: "Full",
    checkInTime: null,
    checkOutTime: null,
    comments: [],
    forRentalDates: [],
    host: null,
    images: [],
    location: {
      id: 0,
      address: {
        id: 1,
        cityName: "Novi Sad",
        postalCode: "21102",
        streetName: "Bulevar kralja Petra",
        streetNumber: "25"
      },
      latitude: 45.267136,
      longitude: 19.833549
    },
    numberOfGuests: 0,
    numberOfRooms: 1,
    pricePerNight: 0,
    reservations: []
  }
};

export default class Apartment extends Component {
  constructor(props = defaultProps) {
    super(props);

    this.state = { apartment: props.apartment };
  }
  render() {
    // TODO: Display apartment in a meaningful way
    return <div>This is my apartment</div>;
  }
}
