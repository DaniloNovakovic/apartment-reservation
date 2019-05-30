using ApartmentReservation.Domain.Enumerations;

namespace ApartmentReservation.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string RoleName { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}