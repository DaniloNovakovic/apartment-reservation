using System.Text.RegularExpressions;
using FluentValidation;

namespace ApartmentReservation.Application.Dtos
{
    public class LoginUserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class LoginUserDtoValidation : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidation()
        {
            this.RuleFor(u => u.Username).Matches("^[a-z]+[a-z0-9]*$", RegexOptions.IgnoreCase).MinimumLength(3);
            this.RuleFor(u => u.Password).NotEmpty().MinimumLength(4);
        }
    }
}