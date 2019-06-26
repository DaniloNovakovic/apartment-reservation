using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Moq;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Apartments
{
    public class ApartmentsControllerTestsBase
    {
        protected readonly Mock<IMediator> mediatorMock;

        public ApartmentsControllerTestsBase()
        {
            this.mediatorMock = new Mock<IMediator>();
        }

        protected ApartmentsController CreateController(long userId, string role)
        {
            return new ApartmentsController(this.mediatorMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(userId, role)
            };
        }

        protected ApartmentsController CreateController()
        {
            return new ApartmentsController(this.mediatorMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext()
            };
        }
    }
}