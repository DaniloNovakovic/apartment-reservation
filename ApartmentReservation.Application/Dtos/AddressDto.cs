using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Dtos
{
    public class AddressDto
    {
        public AddressDto()
        {
        }

        public AddressDto(Address address)
        {
            if (address is null)
                return;

            CustomMapper.Map(address, this);
        }

        public long? Id { get; set; }

        public string CityName { get; set; }

        public string PostalCode { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string CountryName { get; set; }
    }
}