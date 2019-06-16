using System;
using System.Collections.Generic;
using System.Linq;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Domain.Entities
{
    public class Apartment : Logical, IApartment
    {
        public Apartment()
        {
            this.ForRentalDates = new HashSet<ForRentalDate>();
            this.Comments = new HashSet<Comment>();
            this.Images = new HashSet<Image>();
            this.ApartmentAmenities = new HashSet<ApartmentAmenity>();
            this.Reservations = new HashSet<Reservation>();
        }

        public long Id { get; set; }
        public string ActivityState { get; set; } = ActivityStates.Inactive;

        public ICollection<ApartmentAmenity> ApartmentAmenities { get; set; }
        public IEnumerable<Amenity> Amenities => ApartmentAmenities.Select(a => a.Amenity);

        public string ApartmentType { get; set; } = ApartmentTypes.Full;
        public string CheckInTime { get; set; } = "14:00:00";
        public string CheckOutTime { get; set; } = "10:00:00";
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ForRentalDate> ForRentalDates { get; set; }
        public Host Host { get; set; }
        public long? HostId { get; set; }
        public ICollection<Image> Images { get; set; }

        public Location Location { get; set; }

        public long? LocationId { get; set; }

        public int NumberOfGuests { get; set; } = 0;

        public int NumberOfRooms { get; set; } = 1;

        public double PricePerNight { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public string Title { get; set; } = "";
    }
}