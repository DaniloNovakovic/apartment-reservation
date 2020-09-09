using System;

namespace ApartmentReservation.Application.Dtos
{
    public class ReservationDto
    {
        public long Id { get; set; }
        public long GuestId { get; set; }
        public string GuestUsername { get; set; }

        public long HostId { get; set; }
        public string HostUsername { get; set; }

        public long ApartmentId { get; set; }
        public string ApartmentTitle { get; set; }

        public DateTime ReservationStartDate { get; set; }
        public int NumberOfNightsRented { get; set; }
        public double TotalCost { get; set; }

        public string ReservationState { get; set; }
    }
}