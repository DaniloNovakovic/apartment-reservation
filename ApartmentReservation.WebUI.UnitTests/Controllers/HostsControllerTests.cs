using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = this.CreateController("Admin", RoleNames.Administrator);

            // Act
            var result = await controller.Get(hostId);

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsType<HostDto>(okResult.Value);
        }

        [Fact]
        public async Task Get_WhenUserIsAskingForItsOwnInformation_ReturnHostDtoFromMediator()
        {
            // Arrange
            const string hostId = "Djura";
            var expectedResultValue = new HostDto { Id = hostId };

            mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = CreateController(hostId, RoleNames.Host);

            // Act
            var result = await controller.Get(hostId);

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsType<HostDto>(okResult.Value);
        }

        [Fact]
        public async Task Get_WhenUserIsAskingForStrangerInformation_ReturnUnauthorized()
        {
            // Arrange
            const string hostId = "Me";
            var expectedResultValue = new HostDto { Id = hostId };

            mediatorMock
                .Setup(m => m.Send(It.IsAny<IRequest<HostDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResultValue);

            var controller = CreateController("Stranger", RoleNames.Host);

            // Act
            var result = await controller.Get(hostId);

            // Assert
            var unauthorizedResult = Assert.IsAssignableFrom<UnauthorizedResult>(result);
        }

        private HostsController CreateController(string username, string role)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, role)
            }, "mock"));

            return new HostsController(mediatorMock.Object, authServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
        }
    }
}