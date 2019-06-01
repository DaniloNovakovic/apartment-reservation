﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    public class Role
    {
        private readonly IClaimsFactory claimsFactory;
        private readonly IApartmentReservationDbContext context;

        public Role(IClaimsFactory claimsFactory, IApartmentReservationDbContext context)
        {
            this.claimsFactory = claimsFactory;
            this.context = context;
        }

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

        protected virtual async Task<User> GetUserAsync(string username)
        {
            return await this.context.Users.SingleOrDefaultAsync(u => u.Username == username && !u.IsDeleted).ConfigureAwait(false);
        }
    }
}