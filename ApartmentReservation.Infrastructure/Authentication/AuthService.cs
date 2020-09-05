using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Persistence.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly RoleFactory roleFactory;
        private readonly IApartmentReservationDbContext context;

        public AuthService(RoleFactory roleFactory, IApartmentReservationDbContext context)
        {
            this.roleFactory = roleFactory;
            this.context = context;
        }

        public async Task LoginAsync(LoginUserDto loginUserDto, HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
                throw new AlreadyLoggedInException();

            var dbUser = await GetUserAsync(loginUserDto.Username).ConfigureAwait(false);

            if (dbUser is null)
                throw new NotFoundException($"Could not find user with username = '{loginUserDto.Username}'");

            var role = roleFactory.GetRole(dbUser.RoleName);

            await role.LoginAsync(loginUserDto, dbUser, httpContext).ConfigureAwait(false);
        }

        public async Task LogoutAsync(string roleName, HttpContext httpContext)
        {
            var role = roleFactory.GetRole(roleName);

            await role.LogoutAsync(httpContext).ConfigureAwait(false);
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