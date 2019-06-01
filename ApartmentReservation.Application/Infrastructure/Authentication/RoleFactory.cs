using System.Collections.Generic;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    public class RoleFactory
    {
        private readonly Dictionary<string, Role> dict;
        private readonly NullObjectRole nullRole = new NullObjectRole();

        public RoleFactory(IApartmentReservationDbContext context)
        {
            this.dict = new Dictionary<string, Role>
            {
                [RoleNames.Administrator] = new Role(new AdministratorClaimsFactory(), context),
                [RoleNames.Guest] = new Role(new GuestClaimsFactory(), context),
                [RoleNames.Host] = new Role(new HostClaimsFactory(), context)
            };
        }

        public void RegisterRole(string roleName, Role roleToRegister)
        {
            this.dict[roleName] = roleToRegister;
        }

        public Role GetRole(string roleName)
        {
            return this.dict.TryGetValue(roleName, out var role) ? role : this.nullRole;
        }

        private class NullObjectRole : Role
        {
            public NullObjectRole(IClaimsFactory claimsFactory = null, IApartmentReservationDbContext context = null) : base(claimsFactory, context)
            {
            }

            public override Task LoginAsync(UserDto user, HttpContext httpContext)
            {
                return Task.CompletedTask;
            }

            public override Task LogoutAsync(HttpContext httpContext)
            {
                return Task.CompletedTask;
            }
        }
    }
}