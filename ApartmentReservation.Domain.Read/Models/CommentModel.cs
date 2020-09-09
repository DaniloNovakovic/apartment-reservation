using ApartmentReservation.Domain.Read.Common;

namespace ApartmentReservation.Domain.Read.Models
{
    public class CommentModel : Record
    {
        public long GuestId { get; set; }
        public string GuestUsername { get; set; }

        public byte Rating { get; set; }

        public string Text { get; set; }

        public bool Approved { get; set; }
    }
}
