using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.WebUI.Controllers;
using MediatR;
using Moq;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Hosts
{
    public class HostsControllerTestsBase
    {
        protected readonly Mock<IMediator> mediatorMock;
        protected readonly Mock<IAuthService> authServiceMock;

        public HostsControllerTestsBase()
        {
            this.mediatorMock = new Mock<IMediator>();
            this.authServiceMock = new Mock<IAuthService>();
        }

        protected HostsController CreateController(long userId, string role)
        {
            return new HostsController(this.mediatorMock.Object, this.authServiceMock.Object)
            {
                ControllerContext = ControllerContextFactory.CreateContext(userId, role)
            };
        }
    }
}