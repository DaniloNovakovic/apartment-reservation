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
        }

        public string Uri { get; set; }
    }
}