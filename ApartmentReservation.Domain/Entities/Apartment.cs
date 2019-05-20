using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApartmentReservation.Domain.Entities
{
    public enum ActivityState
    {
        Active,
        Inactive
    }

    public enum ApartmentType
    {
        Full,
        SingleRoom
    }

    public class Apartment
    {
        public Apartment()
        {
            ForRentalDates = new List<DateTime>();
            Comments = new HashSet<Comment>();
            ImageUris = new HashSet<string>();
            Amenities = new HashSet<Amenity>();
        }

        public ActivityState ActivityState { get; set; }

        public ICollection<Amenity> Amenities { get; set; }

        public ApartmentType ApartmentType { get; set; }

        public DateTime CheckInTime { get; set; }

        public DateTime CheckOutTime { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public ICollection<DateTime> ForRentalDates { get; set; }

        public Host Host { get; set; }

        public string HostId { get; set; }

        public string Id { get; set; }

        [NotMapped]
        public ICollection<string> ImageUris { get; set; }

        public Location Location { get; set; }

        public string LocationId { get; set; }

        public int NumberOfGuests { get; set; }

        public int NumberOfRooms { get; set; }

        public double PricePerNight { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}