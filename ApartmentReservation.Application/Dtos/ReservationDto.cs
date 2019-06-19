using System;
using ApartmentReservation.Domain.Constants;
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
            Id = reservation.Id;
            Apartment = new ApartmentDto(reservation.Apartment);
            ReservationStartDate = reservation.ReservationStartDate;
            NumberOfNightsRented = reservation.NumberOfNightsRented;
            TotalCost = reservation.TotalCost;
            Guest = new UserPublicDto(reservation.Guest);
            ReservationState = reservation.ReservationState;
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