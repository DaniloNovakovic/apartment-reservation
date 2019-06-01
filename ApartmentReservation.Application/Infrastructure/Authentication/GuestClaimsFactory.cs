using System.Collections.Generic;
using System.Security.Claims;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    internal class GuestClaimsFactory : IClaimsFactory
    {
        public IEnumerable<Claim> GenerateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, RoleNames.Guest)
            };
        }
    }
}