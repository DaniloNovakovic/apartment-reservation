namespace ApartmentReservation.Domain.Entities
{
    public class ApartmentAmenity : Logical
    {
        public long ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public long AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}