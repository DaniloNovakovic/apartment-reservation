namespace ApartmentReservation.Domain.Interfaces
{
    public interface IUser
    {
        string FirstName { get; set; }
        string Gender { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string RoleName { get; set; }
        string Username { get; set; }
    }
}