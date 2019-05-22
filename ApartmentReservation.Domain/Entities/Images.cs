using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentReservation.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public string ImageUri { get; set; }

        public Apartment Apartment { get; set; }

        public string ApartmentId { get; set; }
    }
}