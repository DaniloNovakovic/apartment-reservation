namespace ApartmentReservation.Application.Dtos
{
    public class LocationDto
    {
        public long Id { get; set; }
        public AddressDto Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}