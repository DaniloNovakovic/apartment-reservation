using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    public class RoleFactory
    {
        private readonly Dictionary<string, Role> dict;
        private readonly NoRole emptyRole = new NoRole();

        public RoleFactory(IUnitOfWork unitOfWork)
        {
            this.dict = new Dictionary<string, Role>
            {
                [RoleNames.Administrator] = new AdministratorRole(unitOfWork),
                [RoleNames.Guest] = new GuestRole(unitOfWork),
                [RoleNames.Host] = new HostRole(unitOfWork)
            };
        }

        public void RegisterRole(string roleName, Role roleToRegister)
        {
            this.dict[roleName] = roleToRegister;
        }

        public Role GetRole(string roleName)
        {
            return this.dict.TryGetValue(roleName, out var role) ? role : this.emptyRole;
        }

        private class NoRole : Role
        {
            public override Task LoginAsync(UserDto user, HttpContext httpContext)
            {
                return Task.CompletedTask;
            }

            public override Task LogoutAsync(HttpContext httpContext)
            {
                return Task.CompletedTask;
            }

            protected override IEnumerable<Claim> GenerateClaims(User user)
            {
                return null;
            }

            protected override Task<User> GetUserAsync(string username)
            {
                return Task.FromResult<User>(null);
            }
        }
    }
}