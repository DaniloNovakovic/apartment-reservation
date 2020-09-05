using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Persistence.Authentication
{
    public class Role
    {
        private readonly IClaimsFactory claimsFactory;

        public Role(IClaimsFactory claimsFactory)
        {
            this.claimsFactory = claimsFactory;
        }

        public virtual async Task LoginAsync(LoginUserDto webUser, User dbUser, HttpContext httpContext)
        {
            if (dbUser.Password != webUser.Password)
                throw new UnauthorizedException("Incorrect password!");

            if (dbUser.IsBanned)
            {
                throw new UnauthorizedException($"Access denied: User `{dbUser.Username}` has been banned by the administrator!");
            }

            var claims = claimsFactory.GenerateClaims(dbUser);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)).ConfigureAwait(false);
        }

        public virtual async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
                .ConfigureAwait(false);
        }
    }
}