namespace ApartmentReservation.Domain.Entities
{
    public class Comment : Logical
    {
        public long Id { get; set; }

        public Apartment Apartment { get; set; }

        public long? ApartmentId { get; set; }

        public Guest Guest { get; set; }

        public long? GuestId { get; set; }

        public byte Rating { get; set; }

        public string Text { get; set; }

        public bool Approved { get; set; } = false;
    }
}