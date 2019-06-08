using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Guests.Commands
{
    public class UpdateGuestCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateGuestCommandHandler sut;
        private Guest dbGuest;

        public UpdateGuestCommandHandlerTests()
        {
            this.sut = new UpdateGuestCommandHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var guest = new Guest()
            {
                User = new User()
                {
                    Username = "guest",
                    Password = "guest"
                }
            };

            this.dbGuest = this.Context.Guests.Add(guest).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenHostExists_UpdateHost()
        {
            var request = new UpdateGuestCommand()
            {
                Id = this.dbGuest.UserId,
                FirstName = "Danilo",
                LastName = "Novakovic",
                Gender = "Male",
                Password = "codex"
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var guest = await this.Context.Guests.Include(g => g.User)
                .SingleOrDefaultAsync(g => g.UserId == request.Id)
                .ConfigureAwait(false);

            Assert.NotNull(guest);
            CustomAssertAreEqual(request, guest);
        }

        [Fact]
        public async Task UserDoesNotExist_ThrowsNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new UpdateGuestCommand() { Id = -1, Password = "codex" }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        private static void CustomAssertAreEqual(UpdateGuestCommand request, Domain.Entities.Guest guest)
        {
            Assert.Equal(request.Password, guest.User.Password);
            Assert.Equal(request.FirstName, guest.User.FirstName);
            Assert.Equal(request.LastName, guest.User.LastName);
            Assert.Equal(request.Gender, guest.User.Gender);
            Assert.Equal(RoleNames.Guest, guest.User.RoleName);
            Assert.False(guest.User.IsDeleted);
            Assert.False(guest.IsDeleted);
        }
    }
}