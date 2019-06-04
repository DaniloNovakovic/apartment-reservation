namespace ApartmentReservation.Domain.Entities
{
    public class Image : Logical
    {
        public long Id { get; set; }

        public string ImageUri { get; set; }

        public Apartment Apartment { get; set; }

        public long? ApartmentId { get; set; }
    }
}