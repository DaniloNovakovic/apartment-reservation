using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Queries;
using ApartmentReservation.Common.Constants;
using Moq;
using Xunit;

namespace ApartmentReservation.WebUI.UnitTests.Controllers.Apartments
{
    public class ApartmentsController_GetAllTests : ApartmentsControllerTestsBase
    {
        [Fact]
        public async Task WhenUserIsUnauthenticated_CallsMediatorWithActiveQuery()
        {
            var controller = this.CreateController();
            var query = new GetAllApartmentsQuery() { ActivityState = ActivityStates.Inactive };
            var result = await controller.Get(query).ConfigureAwait(false);

            this.mediatorMock.Verify(m => m.Send(It.Is<GetAllApartmentsQuery>(q => q.ActivityState == ActivityStates.Active), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenUserIsGuest_CallsMediatorWithActiveQuery()
        {
            var controller = this.CreateController(userId: 1, role: RoleNames.Guest);
            var query = new GetAllApartmentsQuery() { ActivityState = ActivityStates.Inactive };
            var result = await controller.Get(query).ConfigureAwait(false);

            this.mediatorMock.Verify(m => m.Send(It.Is<GetAllApartmentsQuery>(q => q.ActivityState == ActivityStates.Active), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(ActivityStates.Active)]
        [InlineData(ActivityStates.Inactive)]
        public async Task WhenUserIsAdmin_CallsMediatorWithRequestedQuery(string activityState)
        {
            var controller = this.CreateController(userId: 3, role: RoleNames.Administrator);
            var query = new GetAllApartmentsQuery() { ActivityState = activityState };
            var result = await controller.Get(query).ConfigureAwait(false);

            this.mediatorMock.Verify(m => m.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenUserIsHostAndStranger_CallsMediatorWithHostIdChangedToUserId()
        {
            var controller = this.CreateController(userId: 2, role: RoleNames.Host);
            var query = new GetAllApartmentsQuery() { ActivityState = ActivityStates.Inactive, HostId = 3 };
            var result = await controller.Get(query).ConfigureAwait(false);

            this.mediatorMock.Verify(m => m.Send(It.Is<GetAllApartmentsQuery>(q => q.HostId == 2), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenUserIsNotStranger_CallsMediatorWithRequestedQuery()
        {
            var controller = this.CreateController(userId: 3, role: RoleNames.Host);
            var query = new GetAllApartmentsQuery() { ActivityState = ActivityStates.Inactive, HostId = 3 };
            var result = await controller.Get(query).ConfigureAwait(false);

            this.mediatorMock.Verify(m => m.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenUserIsAdminAndStranger_CallsMediatorWithRequestedQuery()
        {
            var controller = this.CreateController(userId: 3, role: RoleNames.Administrator);
            var query = new GetAllApartmentsQuery() { ActivityState = ActivityStates.Inactive, HostId = 5 };
            var result = await controller.Get(query).ConfigureAwait(false);

            this.mediatorMock.Verify(m => m.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}