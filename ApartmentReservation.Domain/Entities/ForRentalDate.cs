using System;

namespace ApartmentReservation.Domain.Entities
{
    public class ForRentalDate
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Apartment Apartment { get; set; }

        public string ApartmentId { get; set; }
    }
}