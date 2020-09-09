using System;

namespace ApartmentReservation.Application.Dtos
{
    public class ForRentalDateDto
    {
        public DateTime Date { get; set; }

        public bool Equals(DateTime dateTime)
        {
            return dateTime.Year == this.Date.Year
                && dateTime.Month == this.Date.Month
                && dateTime.Day == this.Date.Day;
        }
    }
}