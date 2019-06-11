using System.Text.RegularExpressions;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Interfaces;
using FluentValidation;

namespace ApartmentReservation.Application.Dtos
{
    public class UserDto : IUser
    {
        public long? Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Gender { get; set; } = "";

        public string RoleName { get; set; } = RoleNames.Guest;

        public UserDto()
        {
        }

        public UserDto(IUser user, long? id = null)
        {
            Id = id;
            CustomMapper.Map(user, this);
        }

        public UserDto(IUserRole userRole, long? id = null)
        {
            Id = id;
            CustomMapper.Map(userRole.User, this);
        }
    }

    public class UserDtoValidation : AbstractValidator<UserDto>
    {
        public UserDtoValidation()
        {
            this.RuleFor(u => u.Username).Matches("^[a-z]+[a-z0-9]*$", RegexOptions.IgnoreCase).MinimumLength(3);
            this.RuleFor(u => u.Password).NotEmpty().MinimumLength(4);
        }
    }
}