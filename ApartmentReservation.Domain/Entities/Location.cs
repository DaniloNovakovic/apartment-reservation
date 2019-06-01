﻿namespace ApartmentReservation.Domain.Entities
{
    public class Location : Logical
    {
        public long Id { get; set; }
        public Address Address { get; set; }

        public string AddressId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}