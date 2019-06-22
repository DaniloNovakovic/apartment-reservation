using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Dtos
{
    public class CommentDto
    {
        public CommentDto()
        {
        }

        public CommentDto(Comment c)
        {
            Id = c.Id;

            if (c.Apartment != null)
            {
                Apartment = new ApartmentDto(c.Apartment);
            }

            if (c.Guest != null)
            {
                Guest = new GuestDto(c.Guest);
            }

            Rating = c.Rating;
            Text = c.Text;
            Approved = c.Approved;
        }

        public long? Id { get; set; }

        public ApartmentDto Apartment { get; set; }

        public GuestDto Guest { get; set; }

        public byte Rating { get; set; }

        public string Text { get; set; }

        public bool Approved { get; set; }
    }
}