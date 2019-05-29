using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
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
        private readonly Mock<IAuthService> authServiceMock;
        private readonly Mock<IMediator> mediatorMock;

        public HostsControllerTests()
        {
            this.authServiceMock = new Mock<IAuthService>();
            this.mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Login_WhenInvoked_CallsAuthServiceWithCorrectParameters()
        {
            var host = new HostsController(null, this.authServiceMock.Object);

            var userDto = new UserDto() { Username = "Djura", Password = "123" };

            var result = await host.Login(userDto);

            this.authServiceMock.Verify(x => x.LoginAsync(userDto, RoleNames.Host, host.HttpContext), Times.Once);
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

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateHostCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Put_WhenUserIsUpdatingItsOwnInformation_SendUpdateCommandToMediator()
        {
            var controller = this.CreateController("Djura", RoleNames.Host);
            var updateCommand = new UpdateHostCommand();

            await controller.Put("Djura", updateCommand);

            this.mediatorMock.Verify(m => m.Send(It.Is<UpdateHostCommand>(c => c == updateCommand), It.IsAny<CancellationToken>()));
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

        private HostsController CreateController(string username, string role)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, role)
            }, "mock"));

            return new HostsController(this.mediatorMock.Object, this.authServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
        }
    }
}