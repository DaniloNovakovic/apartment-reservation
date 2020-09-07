using ApartmentReservation.Domain.Read.Common;
using System;
using System.Collections.Generic;

namespace ApartmentReservation.Domain.Read.Models
{
    public class ApartmentModel : Record
    {
        public long? HostId { get; set; }

        public string ActivityState { get; set; }
        public string ApartmentType { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public LocationModel Location { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfRooms { get; set; }
        public double PricePerNight { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public ICollection<ImageModel> Images { get; set; } = new HashSet<ImageModel>();
        public ICollection<AmenityModel> Amenities { get; set; } = new HashSet<AmenityModel>();
        public ICollection<DateTime> ForRentalDates { get; set; } = new HashSet<DateTime>();
        public ICollection<DateTime> AvailableDates { get; set; } = new HashSet<DateTime>();
        public ICollection<CommentModel> Comments { get; set; } = new HashSet<CommentModel>();
    }
}
