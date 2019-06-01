using System.Collections.Generic;
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
        private readonly NullObjectRole nullRole = new NullObjectRole();

        public RoleFactory()
        {
            this.dict = new Dictionary<string, Role>
            {
                [RoleNames.Administrator] = new Role(new AdministratorClaimsFactory()),
                [RoleNames.Guest] = new Role(new GuestClaimsFactory()),
                [RoleNames.Host] = new Role(new HostClaimsFactory())
            };
        }

        public virtual void RegisterRole(string roleName, Role roleToRegister)
        {
            this.dict[roleName] = roleToRegister;
        }

        public virtual Role GetRole(string roleName)
        {
            return this.dict.TryGetValue(roleName, out var role) ? role : this.nullRole;
        }

        private class NullObjectRole : Role
        {
            public NullObjectRole(IClaimsFactory claimsFactory = null) : base(claimsFactory)
            {
            }

            public override Task LoginAsync(LoginUserDto webUser, User dbUser, HttpContext httpContext)
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