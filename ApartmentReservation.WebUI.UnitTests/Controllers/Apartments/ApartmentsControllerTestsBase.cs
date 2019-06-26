using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Moq;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Apartments
{
    public class ApartmentsControllerTestsBase
    {
        protected readonly Mock<IMediator> mediatorMock;
        protected readonly Mock<IAuthService> authServiceMock;

        public ApartmentsControllerTestsBase()
        {
            this.mediatorMock = new Mock<IMediator>();
            this.authServiceMock = new Mock<IAuthService>();
        }

        protected ApartmentsController CreateController(long userId, string role)
        {
            return new ApartmentsController(this.mediatorMock.Object, this.authServiceMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(userId, role)
            };
        }

        protected ApartmentsController CreateController()
        {
            return new ApartmentsController(this.mediatorMock.Object, this.authServiceMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext()
            };
        }
    }
}