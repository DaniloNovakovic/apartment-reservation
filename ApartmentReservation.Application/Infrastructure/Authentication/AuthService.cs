using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Infrastructure.Authentication
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

            var dbUser = await this.GetUserAsync(loginUserDto.Username).ConfigureAwait(false);

            if (dbUser is null)
                throw new NotFoundException($"Could not find user with username = '{loginUserDto.Username}'");

            var role = this.roleFactory.GetRole(dbUser.RoleName);

            await role.LoginAsync(loginUserDto, dbUser, httpContext).ConfigureAwait(false);
        }

        public async Task LogoutAsync(string roleName, HttpContext httpContext)
        {
            var role = this.roleFactory.GetRole(roleName);

            await role.LogoutAsync(httpContext).ConfigureAwait(false);
        }

        protected async Task<User> GetUserAsync(string username)
        {
            return await this.context.Users.SingleOrDefaultAsync(u => u.Username == username && !u.IsDeleted).ConfigureAwait(false);
        }
    }
}