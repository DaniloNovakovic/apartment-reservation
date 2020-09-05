using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Users.Commands;
using ApartmentReservation.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Users
{
    public class UsersController_DeleteTests : UsersControllerTestsBase
    {
        private readonly long userId = 1;
        private long StrangerId => this.userId + 10;

        [Fact]
        public async Task Delete_WhenUserIsAdmin_SendDeleteCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Administrator);

            await controller.Delete(this.userId);

            this.mediatorMock.Verify(m => m.Send(It.Is<DeleteUserCommand>(c => c.Id == this.userId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenUserIsUpdatingItsOwnInformation_SendDeleteCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Host);

            await controller.Delete(this.userId);

            this.mediatorMock.Verify(m => m.Send(It.Is<DeleteUserCommand>(c => c.Id == this.userId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenUserIsUpdatingStrangersInformation_ReturnUnauthorized()
        {
            var controller = this.CreateController(this.userId, RoleNames.Host);

            var result = await controller.Delete(this.StrangerId);

            var unauthorized = Assert.IsAssignableFrom<UnauthorizedResult>(result);

            this.mediatorMock.Verify(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}