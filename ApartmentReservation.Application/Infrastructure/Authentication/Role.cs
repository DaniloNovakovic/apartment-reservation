using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Infrastructure.Authentication
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

            var claims = this.claimsFactory.GenerateClaims(dbUser);

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