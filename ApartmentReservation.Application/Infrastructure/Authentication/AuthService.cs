using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly RoleFactory roleFactory;

        public AuthService(RoleFactory roleFactory)
        {
            this.roleFactory = roleFactory;
        }

        public async Task LoginAsync(UserDto user, string roleName, HttpContext httpContext)
        {
            var role = this.roleFactory.GetRole(roleName);
            await role.LoginAsync(user, httpContext).ConfigureAwait(false);
        }

        public async Task LogoutAsync(string roleName, HttpContext httpContext)
        {
            var role = this.roleFactory.GetRole(roleName);
            await role.LogoutAsync(httpContext).ConfigureAwait(false);
        }
    }
}