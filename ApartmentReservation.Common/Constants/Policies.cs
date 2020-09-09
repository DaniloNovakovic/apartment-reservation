using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ApartmentReservation.Common.Constants
{
    public static class Policies
    {
        public const string AdministratorOnly = "AdministratorOnly";
        public const string HostOnly = "HostOnly";
        public const string AdministratorOrHostOnly = "AdministratorOrHostOnly";
        public const string AdministratorOrGuestOnly = "AdministratorOrGuestOnly";
        public const string GuestOnly = "GuestOnly";

        public static void AddPolicies(AuthorizationOptions options)
        {
            options.AddPolicy(GuestOnly, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, RoleNames.Guest);
            });

            options.AddPolicy(HostOnly, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, RoleNames.Host);
            });

            options.AddPolicy(AdministratorOnly, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
            });
            options.AddPolicy(AdministratorOrHostOnly, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.Host);
            });

            options.AddPolicy(AdministratorOrGuestOnly, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.Guest);
            });
        }
    }
}