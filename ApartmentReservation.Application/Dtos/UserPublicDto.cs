using ApartmentReservation.Domain.Interfaces;

namespace ApartmentReservation.Application.Dtos
{
    public class UserPublicDto
    {
        public UserPublicDto()
        {
        }

        public UserPublicDto(IUser user, long? id = null)
        {
            if (user is null)
                return;

            this.Id = id;
            CustomMapper.Map(user, this);
        }

        public UserPublicDto(IUserRole userRole) : this(userRole?.User, userRole?.User?.Id)
        {
        }

        public long? Id { get; set; }
        public string FirstName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Username { get; set; }
    }
}