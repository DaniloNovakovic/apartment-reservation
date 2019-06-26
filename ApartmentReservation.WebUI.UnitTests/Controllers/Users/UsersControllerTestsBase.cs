using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Moq;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Users
{
    public class UsersControllerTestsBase
    {
        protected readonly Mock<IMediator> mediatorMock;
        protected readonly Mock<IAuthService> authServiceMock;

        public UsersControllerTestsBase()
        {
            this.mediatorMock = new Mock<IMediator>();
            this.authServiceMock = new Mock<IAuthService>();
        }

        protected UsersController CreateController(long userId, string role)
        {
            return new UsersController(this.mediatorMock.Object, this.authServiceMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(userId, role)
            };
        }

        protected UsersController CreateController()
        {
            return new UsersController(this.mediatorMock.Object, this.authServiceMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext()
            };
        }
    }
}