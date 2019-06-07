using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Guests.Commands;
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
    public class AccountControllerTests
    {
        private readonly Mock<IAuthService> authServiceMock;
        private readonly Mock<IMediator> mediatorMock;

        public AccountControllerTests()
        {
            this.authServiceMock = new Mock<IAuthService>();
            this.mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Login_WhenInvoked_CallsAuthService()
        {
            var controller = this.GetUnauthenticatedController();
            var user = new LoginUserDto() { Username = "admin", Password = "admin" };

            await controller.Login(user).ConfigureAwait(false);

            this.authServiceMock.Verify(a => a.LoginAsync(user, controller.HttpContext));
        }

        [Fact]
        public async Task Logout_WhenInvoked_CallsAuthService()
        {
            var controller = this.GetAuthenticatedController();

            await controller.Logout().ConfigureAwait(false);

            this.authServiceMock.Verify(a => a.LogoutAsync(RoleNames.Administrator, controller.HttpContext));
        }

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

        private AccountController GetAuthenticatedController(long userId = 1, string role = RoleNames.Administrator)
        {
            return new AccountController(this.authServiceMock.Object, this.mediatorMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(userId, role)
            };
        }

        private AccountController GetUnauthenticatedController()
        {
            return new AccountController(this.authServiceMock.Object, this.mediatorMock.Object)
            {
                ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() }
            };
        }
    }
}