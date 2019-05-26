using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    internal class HostRole : Role
    {
        private readonly IUnitOfWork unitOfWork;

        public HostRole(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override IEnumerable<Claim> GenerateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, RoleNames.Host)
            };
        }

        protected override async Task<User> GetUserAsync(string username)
        {
            return await this.unitOfWork.Hosts.GetAsync(username).ConfigureAwait(false);
        }
    }
}