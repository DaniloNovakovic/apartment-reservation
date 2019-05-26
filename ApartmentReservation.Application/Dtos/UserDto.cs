using System.Text.RegularExpressions;
using FluentValidation;

namespace ApartmentReservation.Application.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Gender { get; set; } = "";
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