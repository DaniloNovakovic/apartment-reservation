using System.Threading.Tasks;
using ApartmentReservation.Common.Constants;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Account
{
    public class AccountController_LogoutTests : AccountControllerTestsBase
    {
        [Fact]
        public async Task Logout_WhenInvoked_CallsAuthService()
        {
            var controller = this.GetAuthenticatedController();

            await controller.Logout().ConfigureAwait(false);

            this.authServiceMock.Verify(a => a.LogoutAsync(controller.HttpContext));
        }
    }
}