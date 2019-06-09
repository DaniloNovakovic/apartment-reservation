using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Account
{
    public class AccountController_RegisterTests : AccountControllerTestsBase
    {
        [Fact]
        public async Task Register_UserIsAuthenticated_DoesNotCallMediator()
        {
            var controller = this.GetAuthenticatedController();

            var command = new CreateGuestCommand()
            {
                Username = "guest",
                Password = "guest"
            };

            await controller.Register(command).ConfigureAwait(false);

            this.mediatorMock.Verify(m => m.Send(command, CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Register_UserIsUnauthenticated_CallsMediator()
        {
            var controller = this.GetUnauthenticatedController();

            var command = new CreateGuestCommand()
            {
                Username = "guest",
                Password = "guest"
            };

            await controller.Register(command).ConfigureAwait(false);

            this.mediatorMock.Verify(m => m.Send(command, CancellationToken.None), Times.Once);
        }
    }
}