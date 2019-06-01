using System.Collections.Generic;
using System.Security.Claims;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Interfaces
{
    public interface IClaimsFactory
    {
        IEnumerable<Claim> GenerateClaims(User user);
    }
}