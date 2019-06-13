using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Application.Dtos
{
    public class GuestDto : UserDto
    {
        public GuestDto() : base()
        {
        }

        public GuestDto(IUser user, long? id = null) : base(user, id)
        {
        }

        public GuestDto(IUserRole userRole) : base(userRole)
        {
        }
    }
}