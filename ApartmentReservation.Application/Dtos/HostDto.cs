using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Application.Dtos
{
    public class HostDto : UserDto
    {
        public HostDto()
        {
            this.RoleName = RoleNames.Host;
        }

        public HostDto(IUser user, long? id = null) : base(user, id)
        {
        }

        public HostDto(IUserRole userRole) : base(userRole)
        {
        }
    }
}