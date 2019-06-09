using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Moq;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Hosts
{
    public class HostsControllerTestsBase
    {
        protected readonly Mock<IMediator> mediatorMock;

        public HostsControllerTestsBase()
        {
            this.mediatorMock = new Mock<IMediator>();
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