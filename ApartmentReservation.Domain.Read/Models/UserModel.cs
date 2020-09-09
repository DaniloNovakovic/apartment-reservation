using ApartmentReservation.Domain.Read.Common;

namespace ApartmentReservation.Domain.Read.Models
{
    public class UserModel : Record
    {
        public string FirstName { get; set; } = "";
        public string Gender { get; set; } = "";

        public string LastName { get; set; } = "";
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Username { get; set; }
        public bool Banned { get; set; }
    }
}
