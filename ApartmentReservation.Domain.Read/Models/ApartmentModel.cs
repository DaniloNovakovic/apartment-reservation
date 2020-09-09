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

        public ImageModel[] Images { get; set; } = Array.Empty<ImageModel>();
        public AmenityModel[] Amenities { get; set; } = Array.Empty<AmenityModel>();
        public DateTime[] ForRentalDates { get; set; } = Array.Empty<DateTime>();
        public DateTime[] AvailableDates { get; set; } = Array.Empty<DateTime>();
        public CommentModel[] Comments { get; set; } = Array.Empty<CommentModel>();
    }
}
