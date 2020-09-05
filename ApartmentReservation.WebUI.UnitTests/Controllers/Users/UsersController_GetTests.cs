using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Users
{
    public class UsersController_GetTests : UsersControllerTestsBase
    {
        private readonly long userId = 1;
        private long StrangerId => this.userId + 10;

        [Fact]
        public async Task Get_WhenUserIsAdmin_ReturnUserDtoFromMediator()
        {
            // Arrange
            var expectedResultValue = new UserDto { Id = userId };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<UserDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController(this.userId, RoleNames.Administrator);

            // Act
            var result = await controller.Get(this.userId);

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(expectedResultValue, value);
        }

        [Fact]
        public async Task Get_WhenUserIsAskingForItsOwnInformation_ReturnUserDtoFromMediator()
        {
            // Arrange
            var expectedResultValue = new UserDto { Id = userId };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<UserDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController(this.userId, RoleNames.Host);

            // Act
            var result = await controller.Get(this.userId);

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(expectedResultValue, value);
        }

        [Fact]
        public async Task Get_WhenUserIsAskingForStrangerInformation_ReturnUnauthorized()
        {
            // Arrange
            var controller = this.CreateController(this.userId, RoleNames.Host);

            // Act
            var result = await controller.Get(this.StrangerId);

            // Assert
            var unauthorizedResult = Assert.IsAssignableFrom<UnauthorizedResult>(result);
            this.mediatorMock.Verify(m => m.Send(It.IsAny<IRequest<UserDto>>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}