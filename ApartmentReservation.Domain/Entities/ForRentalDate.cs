using System;
using ApartmentReservation.Common;

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
            return DateTimeHelpers.AreSameDay(this.Date, dateTime);
        }
    }
}