using System;
using System.Collections.Generic;
using System.Linq;
using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Application.Dtos
{
    public class ApartmentDto
    {
        public ApartmentDto()
        {
        }

        public ApartmentDto(IApartment apartment)
        {
            if (apartment is null)
                return;

            this.Id = apartment.Id;
            this.ActivityState = apartment.ActivityState;
            this.Amenities = apartment.Amenities.Where(a => !a.IsDeleted).Select(a => new AmenityDto(a));
            this.ApartmentType = apartment.ApartmentType;
            this.CheckInTime = apartment.CheckInTime;
            this.CheckOutTime = apartment.CheckOutTime;
            this.ForRentalDates = apartment.ForRentalDates.Where(a => !a.IsDeleted).Select(frd => frd.Date);

            if (apartment.Host != null)
            {
                this.Host = new UserPublicDto(apartment.Host);
            }

            this.Images = apartment.Images.Where(i => !i.IsDeleted).Select(i => new ImageDto(i));

            if (apartment.Location != null)
            {
                this.Location = new LocationDto(apartment.Location);
            }

            this.NumberOfGuests = apartment.NumberOfGuests;
            this.NumberOfRooms = apartment.NumberOfRooms;
            this.PricePerNight = apartment.PricePerNight;
            this.Title = apartment.Title;
        }

        public long Id { get; set; }

        public string ActivityState { get; set; }
        public IEnumerable<AmenityDto> Amenities { get; set; }
        public string ApartmentType { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public IEnumerable<DateTime> ForRentalDates { get; set; }
        public IEnumerable<DateTime> AvailableDates { get; set; }
        public UserPublicDto Host { get; set; }
        public long? HostId { get; set; }

        public IEnumerable<ImageDto> Images { get; set; }
        public LocationDto Location { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfRooms { get; set; }
        public double PricePerNight { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
    }
}