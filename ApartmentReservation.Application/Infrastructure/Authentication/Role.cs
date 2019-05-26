using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    public abstract class Role
    {
        public virtual async Task LoginAsync(UserDto user, HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
                throw new AlreadyLoggedInException();

            string username = user.Username;
            string password = user.Password;

            var dbUser = await this.GetUserAsync(username).ConfigureAwait(false);

            if (dbUser is null)
                throw new NotFoundException($"Could not find user with username = '{username}'");

            if (dbUser.Password != password)
                throw new UnauthorizedException("Incorrect password!");

            var claims = this.GenerateClaims(dbUser);

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

        protected abstract IEnumerable<Claim> GenerateClaims(User user);

        protected abstract Task<User> GetUserAsync(string username);
    }
}