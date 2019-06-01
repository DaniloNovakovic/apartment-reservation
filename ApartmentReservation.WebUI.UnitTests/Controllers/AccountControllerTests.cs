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
            var user = new LoginUserDto() { Username = "admin", Password = "admin" };

            await controller.Login(user).ConfigureAwait(false);

            this.authServiceMock.Verify(a => a.LoginAsync(user, controller.HttpContext));
        }

        [Fact]
        public async Task Logout_WhenInvoked_CallsAuthService()
        {
            var controller = new AccountController(this.authServiceMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(username: "Admin", role: RoleNames.Administrator)
            };

            await controller.Logout().ConfigureAwait(false);

            this.authServiceMock.Verify(a => a.LogoutAsync(RoleNames.Administrator, controller.HttpContext));
        }
    }
}