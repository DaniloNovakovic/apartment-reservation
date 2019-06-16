using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentReservation.Domain.Entities
{
    public class ApartmentAmenity
    {
        public long ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public long AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}