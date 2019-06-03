using System.Text.RegularExpressions;
using FluentValidation;

namespace ApartmentReservation.Application.Dtos
{
    public class HostDto
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }
    }
}