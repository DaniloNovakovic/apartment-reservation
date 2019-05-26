using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ApartmentReservation.Application.Infrastructure.Authentication
{
    public static class Policies
    {
        public const string AdministratorOnly = "AdministratorOnly";
        public const string AdministratorOrHostOnly = "AdministratorOrHostOnly";

        public static void AddPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("AdministratorOnly", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
            });
            options.AddPolicy("AdministratorOrHostOnly", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.Host);
            });
        }
    }
}