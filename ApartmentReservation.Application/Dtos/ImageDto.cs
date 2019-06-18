using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Dtos
{
    public class ImageDto
    {
        public ImageDto()
        {
        }

        public ImageDto(Image image)
        {
            Uri = image.ImageUri;
            Id = image.Id;
        }

        public long Id { get; set; }
        public string Uri { get; set; }
    }
}