using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Account
{
    public class AccountControllerTestsBase
    {
        protected readonly Mock<IAuthService> authServiceMock;
        protected readonly Mock<IMediator> mediatorMock;

        public AccountControllerTestsBase()
        {
            this.authServiceMock = new Mock<IAuthService>();
            this.mediatorMock = new Mock<IMediator>();
        }

        protected AccountController GetAuthenticatedController(long userId = 1, string role = RoleNames.Administrator)
        {
            return new AccountController(this.authServiceMock.Object, this.mediatorMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(userId, role)
            };
        }

        protected AccountController GetUnauthenticatedController()
        {
            return new AccountController(this.authServiceMock.Object, this.mediatorMock.Object)
            {
                ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() }
            };
        }
    }
}