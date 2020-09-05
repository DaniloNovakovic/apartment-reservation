using System;
using ApartmentReservation.Common.Constants;

namespace ApartmentReservation.Domain.Entities
{
    public class Reservation : Logical
    {
        public long Id { get; set; }
        public Apartment Apartment { get; set; }

        public long? ApartmentId { get; set; }

        public DateTime ReservationStartDate { get; set; }

        public int NumberOfNightsRented { get; set; } = 1;

        public double TotalCost { get; set; }

        public Guest Guest { get; set; }

        public long? GuestId { get; set; }

        public string ReservationState { get; set; } = ReservationStates.Created;
    }
}