using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Users.Commands;
using ApartmentReservation.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Users
{
    public class UsersController_PutTests : UsersControllerTestsBase
    {
        private readonly long userId = 1;
        private long StrangerId => this.userId + 10;

        [Fact]
        public async Task Put_WhenUserIsAdmin_SendUpdateCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Administrator);
            var updateCommand = new UpdateUserCommand();

            await controller.Put(this.userId + 10, updateCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateUserCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_WhenUserIsUpdatingItsOwnInformation_SendUpdateCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Host);
            var updateCommand = new UpdateUserCommand();

            await controller.Put(this.userId, updateCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateUserCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_WhenUserIsUpdatingStrangersInformation_ReturnUnauthorized()
        {
            var controller = this.CreateController(this.userId, RoleNames.Host);
            var updateCommand = new UpdateUserCommand();

            var result = await controller.Put(this.StrangerId, updateCommand);

            var unauthorized = Assert.IsAssignableFrom<UnauthorizedResult>(result);

            this.mediatorMock.Verify(m => m.Send(
                It.Is<UpdateUserCommand>(c => c == updateCommand),
                It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}