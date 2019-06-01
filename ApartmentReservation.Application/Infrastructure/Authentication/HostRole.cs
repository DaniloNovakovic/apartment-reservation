using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    internal class HostRole : Role
    {
        private readonly IApartmentReservationDbContext context;

        public HostRole(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        protected override IEnumerable<Claim> GenerateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, RoleNames.Host)
            };
        }

        protected override async Task<User> GetUserAsync(string username)
        {
            return await this.context.Users.FindAsync(username).ConfigureAwait(false);
        }
    }
}