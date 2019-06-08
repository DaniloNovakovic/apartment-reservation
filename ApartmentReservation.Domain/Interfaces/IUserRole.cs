using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Domain.Interfaces
{
    public interface IUserRole
    {
        User User { get; set; }
    }
}