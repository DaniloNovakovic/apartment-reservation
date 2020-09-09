using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Application.Features.Guests.Commands;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Account
{
    public class AccountController_RegisterTests : AccountControllerTestsBase
    {
        [Fact]
        public async Task Register_CreatesGuestUsingMediatorAndReturnsCreatedGuest()
        {
            var expectedGuestDto = new GuestDto() { Id = 5, Username = "guest", Password = "guest" };
            this.mediatorMock.Setup(m => m.Send(It.IsAny<CreateGuestCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedGuestDto);

            var controller = this.GetUnauthenticatedController();

            var command = new CreateGuestCommand()
            {
                Username = "guest",
                Password = "guest"
            };

            var result = await controller.Register(command).ConfigureAwait(false);

            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var user = Assert.IsAssignableFrom<UserDto>(okObjectResult.Value);
            Assert.Equal(expectedGuestDto.Id, user.Id);

            this.mediatorMock.Verify(m => m.Send(command, CancellationToken.None), Times.Once);
        }
    }
}