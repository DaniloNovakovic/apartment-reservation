using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Persistence.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IApartmentReservationDbContext context;

        public AuthService( IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<UserDto> LoginAsync(LoginUserDto loginUserDto, HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
                await httpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
                .ConfigureAwait(false);

            var dbUser = await GetUserAsync(loginUserDto.Username).ConfigureAwait(false);

            if (dbUser is null)
                throw new NotFoundException($"Could not find user with username = '{loginUserDto.Username}'");

            if (dbUser.Password != loginUserDto.Password)
                throw new UnauthorizedException("Incorrect password!");

            if (dbUser.IsBanned)
            {
                throw new UnauthorizedException($"Access denied: User `{dbUser.Username}` has been banned by the administrator!");
            }
            List<Claim> claims = GenerateClaims(dbUser);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)).ConfigureAwait(false);

            return new UserDto(dbUser);
        }

        public async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
                .ConfigureAwait(false);
        }

        private static List<Claim> GenerateClaims(User dbUser)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Name, dbUser.Username),
                new Claim(ClaimTypes.Role, dbUser.RoleName)
            };
        }


        protected async Task<User> GetUserAsync(string username)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Username == username && !u.IsDeleted).ConfigureAwait(false);
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal userPrincipal)
        {
            string nameIdentifier = userPrincipal?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            if (long.TryParse(nameIdentifier, out long userId))
            {
                return await context.Users.SingleOrDefaultAsync(u => u.Id == userId && !u.IsDeleted).ConfigureAwait(false);
            }

            return null;
        }

        public async Task<bool> CheckIfBanned(ClaimsPrincipal userPrincipal)
        {
            var dbUser = await GetUserAsync(userPrincipal).ConfigureAwait(false);
            return dbUser?.IsBanned ?? false;
        }
    }
}