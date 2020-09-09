using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Domain.Entities
{
    public class User : Logical, IUser, ISyncable
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string RoleName { get; set; }

        public bool IsBanned { get; set; } = false;

        public bool IsSyncNeeded { get; set; } = true;
    }
}