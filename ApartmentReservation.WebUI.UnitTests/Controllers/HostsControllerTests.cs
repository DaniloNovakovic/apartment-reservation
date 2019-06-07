using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers
{
    public class HostsControllerTests
    {
        protected readonly Mock<IMediator> mediatorMock;
        private readonly long userId = 1;

        public HostsControllerTests()
        {
            this.mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Delete_WhenUserIsAdmin_SendDeleteCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Administrator);

            await controller.Delete(this.userId);

            this.mediatorMock.Verify(m => m.Send(It.Is<DeleteHostCommand>(c => c.Id == this.userId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenUserIsUpdatingItsOwnInformation_SendDeleteCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Host);

            await controller.Delete(this.userId);

            this.mediatorMock.Verify(m => m.Send(It.Is<DeleteHostCommand>(c => c.Id == this.userId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenUserIsUpdatingStrangersInformation_ReturnUnauthorized()
        {
            var controller = this.CreateController(this.userId, RoleNames.Host);

            var result = await controller.Delete(this.userId + 10);

            var unauthorized = Assert.IsAssignableFrom<UnauthorizedResult>(result);

            this.mediatorMock.Verify(m => m.Send(It.IsAny<DeleteHostCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

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

        [Fact]
        public async Task Put_WhenUserIsAdmin_SendUpdateCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Administrator);
            var updateCommand = new UpdateHostCommand();

            await controller.Put(this.userId + 10, updateCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateHostCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_WhenUserIsUpdatingItsOwnInformation_SendUpdateCommandToMediator()
        {
            var controller = this.CreateController(this.userId, RoleNames.Host);
            var updateCommand = new UpdateHostCommand();

            await controller.Put(this.userId, updateCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateHostCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_WhenUserIsUpdatingStrangersInformation_ReturnUnauthorized()
        {
            var controller = this.CreateController(this.userId, RoleNames.Host);
            var updateCommand = new UpdateHostCommand();

            var result = await controller.Put(this.userId + 10, updateCommand);

            var unauthorized = Assert.IsAssignableFrom<UnauthorizedResult>(result);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateHostCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()), Times.Never);
        }

        protected HostsController CreateController(long userId, string role)
        {
            return new HostsController(this.mediatorMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(userId, role)
            };
        }
    }
}