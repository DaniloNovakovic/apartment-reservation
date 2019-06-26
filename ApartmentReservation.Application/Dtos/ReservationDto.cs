using System;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Dtos
{
    public class ReservationDto
    {
        public ReservationDto()
        {
        }

        public ReservationDto(Reservation reservation)
        {
            this.Id = reservation.Id;
            this.Apartment = new ApartmentDto(reservation.Apartment);
            this.ReservationStartDate = reservation.ReservationStartDate;
            this.NumberOfNightsRented = reservation.NumberOfNightsRented;
            this.TotalCost = reservation.TotalCost;
            this.Guest = new UserPublicDto(reservation.Guest);
            this.ReservationState = reservation.ReservationState;
        }

        public long Id { get; set; }
        public ApartmentDto Apartment { get; set; }

        public DateTime ReservationStartDate { get; set; }

        public int NumberOfNightsRented { get; set; }

        public double TotalCost { get; set; }

        public UserPublicDto Guest { get; set; }

        public string ReservationState { get; set; }
    }
}