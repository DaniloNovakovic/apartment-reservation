using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentReservation.WebUI.UnitTests
{
    public static class ControllerContextFactory
    {
        public static ControllerContext CreateContext()
        {
            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        public static ControllerContext CreateContext(long userId, string role)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            }, "mock"));

            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
    }
}