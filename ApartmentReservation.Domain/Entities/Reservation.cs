using System;
using ApartmentReservation.Domain.Enumerations;

namespace ApartmentReservation.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public Apartment Apartment { get; set; }

        public string ApartmentId { get; set; }

        public DateTime ReservationStartDate { get; set; }

        public int NumberOfNightsRented { get; set; } = 1;

        public double TotalCost { get; set; }

        public Guest Guest { get; set; }

        public string GuestId { get; set; }

        public ReservationState ReservationState { get; set; }
    }
}