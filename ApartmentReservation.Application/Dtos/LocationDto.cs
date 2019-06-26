using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Dtos
{
    public class LocationDto
    {
        public LocationDto()
        {
        }

        public LocationDto(Location location)
        {
            if (location is null)
                return;

            location.Id = location.Id;
            this.Latitude = location.Latitude;
            this.Longitude = location.Longitude;
            if (location.Address != null)
            {
                this.Address = new AddressDto(location.Address);
            }
        }

        public long Id { get; set; }
        public AddressDto Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}