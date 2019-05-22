using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ApartmentReservation.Domain.Enumerations;

namespace ApartmentReservation.Domain.Entities
{
    public class Apartment
    {
        public Apartment()
        {
            ForRentalDates = new HashSet<ForRentalDate>();
            Comments = new HashSet<Comment>();
            Images = new HashSet<Image>();
            Amenities = new HashSet<Amenity>();
        }

        public ActivityState ActivityState { get; set; }

        public ICollection<Amenity> Amenities { get; set; }

        public ApartmentType ApartmentType { get; set; }

        public DateTime CheckInTime { get; set; }

        public DateTime CheckOutTime { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<ForRentalDate> ForRentalDates { get; set; }

        public Host Host { get; set; }

        public string HostId { get; set; }

        public string Id { get; set; }

        public ICollection<Image> Images { get; set; }

        public Location Location { get; set; }

        public string LocationId { get; set; }

        public int NumberOfGuests { get; set; }

        public int NumberOfRooms { get; set; }

        public double PricePerNight { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}