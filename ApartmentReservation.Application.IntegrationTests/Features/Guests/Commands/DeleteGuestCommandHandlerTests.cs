using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Guests.Commands
{
    public class DeleteGuestCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly DeleteGuestCommandHandler sut;
        private Guest dbGuest;

        public DeleteGuestCommandHandlerTests()
        {
            this.sut = new DeleteGuestCommandHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var user = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest };

            this.dbGuest = this.Context.Guests.Add(new Guest() { User = user }).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task UserExists_DeletesHostLogically()
        {
            await this.sut.Handle(new DeleteGuestCommand() { Id = this.dbGuest.UserId }, CancellationToken.None).ConfigureAwait(false);

            var guest = await this.Context.Guests.FindAsync(this.dbGuest.UserId).ConfigureAwait(false);

            Assert.True(guest.IsDeleted);
        }

        [Fact]
        public async Task UserDoesNotExist_ThrowsNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new DeleteGuestCommand() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}