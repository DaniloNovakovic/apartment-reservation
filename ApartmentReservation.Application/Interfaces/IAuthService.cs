﻿using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Interfaces
{
    public interface IAuthService
    {
        Task LoginAsync(LoginUserDto user, HttpContext httpContext);

        Task LogoutAsync(string roleName, HttpContext httpContext);

        Task<User> GetUserAsync(ClaimsPrincipal userPrincipal);

        Task<bool> CheckIfBanned(ClaimsPrincipal userPrincipal);
    }
}