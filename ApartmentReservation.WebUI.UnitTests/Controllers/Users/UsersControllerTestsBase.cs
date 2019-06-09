using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Moq;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Users
{
    public class UsersControllerTestsBase
    {
        protected readonly Mock<IMediator> mediatorMock;

        public UsersControllerTestsBase()
        {
            this.mediatorMock = new Mock<IMediator>();
        }

        protected UsersController CreateController(long userId, string role)
        {
            return new UsersController(this.mediatorMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(userId, role)
            };
        }

        protected UsersController CreateController()
        {
            return new UsersController(this.mediatorMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext()
            };
        }
    }
}