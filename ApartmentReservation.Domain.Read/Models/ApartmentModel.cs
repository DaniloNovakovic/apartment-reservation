using ApartmentReservation.Domain.Read.Common;
using System;
using System.Collections.Generic;

namespace ApartmentReservation.Domain.Read.Models
{
    public class ApartmentModel : Record
    {
        public long? HostId { get; set; }
        public bool IsHostBanned { get; set; }

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

        public IEnumerable<ImageModel> Images { get; set; } = new HashSet<ImageModel>();
        public IEnumerable<AmenityModel> Amenities { get; set; } = new HashSet<AmenityModel>();
        public IEnumerable<DateTime> ForRentalDates { get; set; } = new HashSet<DateTime>();
        public IEnumerable<DateTime> AvailableDates { get; set; } = new HashSet<DateTime>();
        public IEnumerable<CommentModel> Comments { get; set; } = new HashSet<CommentModel>();
    }
}
