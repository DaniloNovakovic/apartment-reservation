using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Domain.Entities
{
    public class Administrator : Logical, IUserRoleLogical
    {
        public long UserId { get; set; }

        public User User { get; set; }
    }
}