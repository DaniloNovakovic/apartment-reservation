import React, { Component } from "react";
import ApartmentCarousel from "./ApartmentCarousel";

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
    images: [
      {
        uri = "https://www.onni.com/wp-content/uploads/2016/11/Rental-Apartment-Page-new-min.jpg"
      },
      {
        uri = "https://arystudios.files.wordpress.com/2015/08/3dcontemperoryapartmentrenderingarchitecturalduskviewrealisticarystudios.jpg"
      }
    ],
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

export class ApartmentCard extends Component {
  constructor(props = defaultProps) {
    super(props);

    this.state = { apartment: props.apartment };
  }
  render() {
    return <div className="apartment-card">
      <ApartmentCarousel images={this.state.apartment.images}/>
    </div>
  }
}

export default ApartmentCard;
