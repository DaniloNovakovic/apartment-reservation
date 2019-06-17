using System;

namespace ApartmentReservation.Domain.Entities
{
    public class ForRentalDate : Logical
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public Apartment Apartment { get; set; }

        public long? ApartmentId { get; set; }

        public bool Equals(DateTime dateTime)
        {
            return dateTime.Year == Date.Year
                && dateTime.Month == Date.Month
                && dateTime.Day == Date.Day;
        }
    }
}