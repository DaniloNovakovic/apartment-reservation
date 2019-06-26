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
            this.Uri = image.ImageUri;
            this.Id = image.Id;
        }

        public long Id { get; set; }
        public string Uri { get; set; }
    }
}