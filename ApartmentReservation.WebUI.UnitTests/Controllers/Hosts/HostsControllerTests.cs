using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Hosts
{
    public class HostsControllerTests : HostsControllerTestsBase
    {
        private readonly long userId = 1;

        [Fact]
        public async Task Get_WhenUserIsAdmin_ReturnHostDtoFromMediator()
        {
            // Arrange
            var expectedResultValue = new HostDto { Id = userId };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController(this.userId, RoleNames.Administrator);

            // Act
            var result = await controller.Get(this.userId);

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsType<HostDto>(okResult.Value);
            Assert.Equal(expectedResultValue, value);
        }

        [Fact]
        public async Task Get_WhenUserIsAskingForItsOwnInformation_ReturnHostDtoFromMediator()
        {
            // Arrange
            var expectedResultValue = new HostDto { Id = userId };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController(this.userId, RoleNames.Host);

            // Act
            var result = await controller.Get(this.userId);

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsType<HostDto>(okResult.Value);
            Assert.Equal(expectedResultValue, value);
        }

        [Fact]
        public async Task Get_WhenUserIsAskingForStrangerInformation_ReturnUnauthorized()
        {
            var expectedResultValue = new HostDto { Id = userId };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController(this.userId + 10, RoleNames.Host);

            // Act
            var result = await controller.Get(this.userId);

            // Assert
            var unauthorizedResult = Assert.IsAssignableFrom<UnauthorizedResult>(result);
            this.mediatorMock.Verify(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GetAll_WhenInvoked_ReturnHostDtosFromMediator()
        {
            // Arrange
            var expectedResultValue = new List<HostDto> { new HostDto() };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<IEnumerable<HostDto>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController(this.userId, RoleNames.Administrator);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<HostDto>>(okResult.Value);
            Assert.Equal(expectedResultValue, value);
        }

        [Fact]
        public async Task Post_WhenInvoked_SendCreateCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Administrator);
            var createCommand = new CreateHostCommand() { Username = "Djura", Password = "123" };

            await controller.Post(createCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<CreateHostCommand>(c => c == createCommand), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}