using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace ApartmentReservation.Application.Interfaces
{
    public interface IAuthService
    {
        Task LoginAsync(UserDto user, string roleName, HttpContext httpContext);

        Task LogoutAsync(string roleName, HttpContext httpContext);
    }
}