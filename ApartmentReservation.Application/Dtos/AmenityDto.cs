using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Dtos
{
    public class AmenityDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public AmenityDto()
        {
        }

        public AmenityDto(Amenity amenity)
        {
            CustomMapper.Map(amenity, this);
        }
    }
}