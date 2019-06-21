using System;

namespace ApartmentReservation.Application.Features.Reservations.Commands
{
    public class CanUpdateReservationArgs
    {
        public long GuestId { get; set; }
        public long HostId { get; set; }
        public int NumberOfNightsRented { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public string ReservationState { get; set; }
        public double TotalCost { get; set; }
    }
}