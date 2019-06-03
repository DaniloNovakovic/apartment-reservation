using ApartmentReservation.Application.Infrastructure.Authentication;

namespace ApartmentReservation.Application.Dtos
{
    public class HostDto : UserDto
    {
        public HostDto()
        {
            this.RoleName = RoleNames.Host;
        }
    }
}