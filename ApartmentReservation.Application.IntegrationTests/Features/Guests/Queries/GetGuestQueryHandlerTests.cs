using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Guests.Queries;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Guests.Queries
{
    public class GetGuestQueryHandlerTests : InMemoryContextTestBase
    {
        private Guest dbGuest;
        private readonly GetGuestQueryHandler sut;

        public GetGuestQueryHandlerTests()
        {
            this.sut = new GetGuestQueryHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var user = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest };

            this.dbGuest = new Guest() { User = user };

            this.dbGuest = this.Context.Guests.Add(this.dbGuest).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenGuestExists_ReturnGuest()
        {
            var result = await this.sut.Handle(new GetGuestQuery() { Id = this.dbGuest.UserId }, CancellationToken.None).ConfigureAwait(false);

            Assert.IsAssignableFrom<GuestDto>(result);
            Assert.Equal(this.dbGuest.User.Username, result.Username);
        }

        [Fact]
        public async Task WhenGuestDoesNotExist_ThrowNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new GetGuestQuery() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}