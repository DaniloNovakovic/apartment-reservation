﻿using System;

namespace ApartmentReservation.Domain.Entities
{
    public class ForRentalDate
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public Apartment Apartment { get; set; }

        public long ApartmentId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}