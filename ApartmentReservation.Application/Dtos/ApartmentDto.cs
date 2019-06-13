using System;
using System.Collections.Generic;
using System.Linq;
using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Application.Dtos
{
    public class ApartmentDto
    {
        public ApartmentDto(IApartment apartment)
        {
            if (apartment is null)
                return;

            this.Id = apartment.Id;
            this.ActivityState = apartment.ActivityState;
            this.Amenities = apartment.Amenities.Select(a => new AmenityDto(a));
            this.ApartmentType = apartment.ApartmentType;
            this.CheckInTime = apartment.CheckInTime;
            this.CheckOutTime = apartment.CheckOutTime;
            this.Comments = apartment.Comments.Select(c => new CommentDto(c));
            this.ForRentalDates = apartment.ForRentalDates.Select(frd => new ForRentalDateDto(frd));
            this.Host = new HostDto(apartment.Host);
            this.Images = apartment.Images.Select(i => new ImageDto(i));
            this.Location = new LocationDto(apartment.Location);
            this.NumberOfGuests = apartment.NumberOfGuests;
            this.NumberOfRooms = apartment.NumberOfRooms;
            this.PricePerNight = apartment.PricePerNight;
            this.Reservations = apartment.Reservations.Select(r => new ReservationDto(r));
        }

        public long Id { get; set; }
        public string ActivityState { get; set; }
        public IEnumerable<AmenityDto> Amenities { get; set; }
        public string ApartmentType { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        public IEnumerable<ForRentalDateDto> ForRentalDates { get; set; }
        public HostDto Host { get; set; }
        public IEnumerable<ImageDto> Images { get; set; }
        public LocationDto Location { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfRooms { get; set; }
        public double PricePerNight { get; set; }
        public IEnumerable<ReservationDto> Reservations { get; set; }
    }
}