using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    internal class AdministratorRole : Role
    {
        private IUnitOfWork unitOfWork;

        public AdministratorRole(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override IEnumerable<Claim> GenerateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, RoleNames.Administrator)
            };
        }

        protected override async Task<User> GetUserAsync(string username)
        {
            return await this.unitOfWork.Administrators.GetAsync(username).ConfigureAwait(false);
        }
    }
}