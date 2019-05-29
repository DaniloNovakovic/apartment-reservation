using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.WebUI.Controllers;
using ApartmentReservation.WebUI.UnitTests.Utils;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IAuthService> authServiceMock;

        public AccountControllerTests()
        {
            this.authServiceMock = new Mock<IAuthService>();
        }

        [Fact]
        public async Task Login_WhenInvoked_CallsAuthService()
        {
            var controller = new AccountController(this.authServiceMock.Object);
            var user = new UserDto() { Username = "admin", Password = "admin", RoleName = RoleNames.Administrator };

            await controller.Login(user).ConfigureAwait(false);

            this.authServiceMock.Verify(a => a.LoginAsync(user, user.RoleName, controller.HttpContext));
        }

        [Fact]
        public async Task Logout_WhenInvoked_CallsAuthService()
        {
            var user = new UserDto() { Username = "admin", Password = "admin", RoleName = RoleNames.Administrator };

            var controller = new AccountController(this.authServiceMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(user.Username, user.RoleName)
            };

            await controller.Logout().ConfigureAwait(false);

            this.authServiceMock.Verify(a => a.LogoutAsync(user.RoleName, controller.HttpContext));
        }
    }
}