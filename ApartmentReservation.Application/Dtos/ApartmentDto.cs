using System;
using System.Collections.Generic;

namespace ApartmentReservation.Application.Dtos
{
    public class ApartmentDto
    {
        public long Id { get; set; }

        public long? HostId { get; set; }
        public UserPublicDto Host { get; set; }

        public string ActivityState { get; set; }
        public string ApartmentType { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }

        public int NumberOfGuests { get; set; }
        public int NumberOfRooms { get; set; }
        public double PricePerNight { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public LocationDto Location { get; set; }


        public IEnumerable<AmenityDto> Amenities { get; set; } = new List<AmenityDto>();
        public IEnumerable<DateTime> ForRentalDates { get; set; } = new List<DateTime>();
        public IEnumerable<DateTime> AvailableDates { get; set; } = new List<DateTime>();
        public IEnumerable<ImageDto> Images { get; set; } = new List<ImageDto>();
        public IEnumerable<CommentDto> Comments{ get; set; } = new List<CommentDto>();
    }
}