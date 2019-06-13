using System;
using System.Collections.Generic;
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
            this.Amenities = new HashSet<Amenity>();
        }

        public long Id { get; set; }

        public string ActivityState { get; set; } = ActivityStates.Active;

        public ICollection<Amenity> Amenities { get; set; }

        public string ApartmentType { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

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
    }
}