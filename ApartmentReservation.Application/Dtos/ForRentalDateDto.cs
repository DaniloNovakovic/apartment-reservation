using System;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Dtos
{
    public class ForRentalDateDto
    {
        public ForRentalDateDto(ForRentalDate frd)
        {
            this.Date = frd.Date;
        }

        public DateTime Date { get; set; }

        public bool Equals(DateTime dateTime)
        {
            return dateTime.Year == Date.Year
                && dateTime.Month == Date.Month
                && dateTime.Day == Date.Day;
        }
    }
}