namespace ApartmentReservation.Domain.Entities
{
    public class Administrator : Logical
    {
        public long UserId { get; set; }

        public User User { get; set; }
    }
}