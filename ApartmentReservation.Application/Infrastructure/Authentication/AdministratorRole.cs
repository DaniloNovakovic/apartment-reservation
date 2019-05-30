using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    internal class AdministratorRole : Role
    {
        private readonly IUnitOfWork unitOfWork;

        public AdministratorRole(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override IEnumerable<Claim> GenerateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, RoleNames.Administrator)
            };
        }

        protected override async Task<User> GetUserAsync(string username)
        {
            return await this.unitOfWork.Users.GetAsync(username).ConfigureAwait(false);
        }
    }
}