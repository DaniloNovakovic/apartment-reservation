using System;
using System.Collections.Generic;
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
    public class AuthenticationService
    {
        private readonly RoleFactory roleFactory;

        public AuthenticationService(RoleFactory roleFactory)
        {
            this.roleFactory = roleFactory;
        }

        public async Task Login(UserDto user, string roleName, HttpContext httpContext)
        {
            var role = roleFactory.GetRole(roleName);
            await role.LoginAsync(user, httpContext);
        }

        public async Task LogoutAsync(string roleName, HttpContext httpContext)
        {
            var role = roleFactory.GetRole(roleName);
            await role.LogoutAsync(httpContext);
        }
    }

    public class RoleFactory
    {
        internal Role GetRole(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}