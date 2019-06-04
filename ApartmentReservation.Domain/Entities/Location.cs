namespace ApartmentReservation.Domain.Entities
{
    public class Location : Logical
    {
        public long Id { get; set; }
        public Address Address { get; set; }

        public long? AddressId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}