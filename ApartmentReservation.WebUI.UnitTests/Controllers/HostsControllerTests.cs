using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers
{
    public class HostsControllerTests
    {
        protected readonly Mock<IMediator> mediatorMock;

        public HostsControllerTests()
        {
            this.mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Delete_WhenUserIsAdmin_SendDeleteCommandToMediator()
        {
            string userId = "Admin";
            var controller = this.CreateController(userId, RoleNames.Administrator);

            await controller.Delete(userId);

            this.mediatorMock.Verify(m => m.Send(It.Is<DeleteHostCommand>(c => c.Id == userId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenUserIsUpdatingItsOwnInformation_SendDeleteCommandToMediator()
        {
            string userId = "Djura";
            var controller = this.CreateController(userId, RoleNames.Host);

            await controller.Delete(userId);

            this.mediatorMock.Verify(m => m.Send(It.Is<DeleteHostCommand>(c => c.Id == userId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenUserIsUpdatingStrangersInformation_ReturnUnauthorized()
        {
            string userId = "Djura";
            var controller = this.CreateController(userId, RoleNames.Host);

            var result = await controller.Delete("Steva");

            var unauthorized = Assert.IsAssignableFrom<UnauthorizedResult>(result);

            this.mediatorMock.Verify(m => m.Send(It.IsAny<DeleteHostCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Get_WhenUserIsAdmin_ReturnHostDtoFromMediator()
        {
            // Arrange
            const string hostId = "Djura";
            var expectedResultValue = new HostDto { Id = hostId };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController("Admin", RoleNames.Administrator);

            // Act
            var result = await controller.Get(hostId);

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsType<HostDto>(okResult.Value);
            Assert.Equal(expectedResultValue, value);
        }

        [Fact]
        public async Task Get_WhenUserIsAskingForItsOwnInformation_ReturnHostDtoFromMediator()
        {
            // Arrange
            const string hostId = "Djura";
            var expectedResultValue = new HostDto { Id = hostId };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController(hostId, RoleNames.Host);

            // Act
            var result = await controller.Get(hostId);

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsType<HostDto>(okResult.Value);
            Assert.Equal(expectedResultValue, value);
        }

        [Fact]
        public async Task Get_WhenUserIsAskingForStrangerInformation_ReturnUnauthorized()
        {
            // Arrange
            const string hostId = "Me";
            var expectedResultValue = new HostDto { Id = hostId };

            this.mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController("Stranger", RoleNames.Host);

            // Act
            var result = await controller.Get(hostId);

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

            var controller = this.CreateController("Admin", RoleNames.Administrator);

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
            var controller = this.CreateController("Admin", RoleNames.Administrator);
            var createCommand = new CreateHostCommand() { Username = "Djura", Password = "123" };

            await controller.Post(createCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<CreateHostCommand>(c => c == createCommand), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_WhenUserIsAdmin_SendUpdateCommandToMediator()
        {
            var controller = this.CreateController("Admin", RoleNames.Administrator);
            var updateCommand = new UpdateHostCommand();

            await controller.Put("Admin", updateCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateHostCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_WhenUserIsUpdatingItsOwnInformation_SendUpdateCommandToMediator()
        {
            var controller = this.CreateController("Djura", RoleNames.Host);
            var updateCommand = new UpdateHostCommand();

            await controller.Put("Djura", updateCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateHostCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_WhenUserIsUpdatingStrangersInformation_ReturnUnauthorized()
        {
            var controller = this.CreateController("Djura", RoleNames.Host);
            var updateCommand = new UpdateHostCommand();

            var result = await controller.Put("Pera", updateCommand);

            var unauthorized = Assert.IsAssignableFrom<UnauthorizedResult>(result);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateHostCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()), Times.Never);
        }

        protected HostsController CreateController(string username, string role)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, role)
            }, "mock"));

            return new HostsController(this.mediatorMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
        }
    }
}