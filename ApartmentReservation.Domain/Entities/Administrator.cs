namespace ApartmentReservation.Domain.Entities
{
    public class Administrator
    {
        public long UserId { get; set; }

        public User User { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}