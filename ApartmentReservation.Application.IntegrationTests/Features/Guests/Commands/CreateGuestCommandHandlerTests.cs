using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Guests.Commands
{
    public class CreateGuestCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly CreateGuestCommandHandler sut;

        public CreateGuestCommandHandlerTests()
        {
            this.sut = new CreateGuestCommandHandler(this.Context);
        }

        [Fact]
        public async Task UserDoesNotExist_CreatesGuest()
        {
            // Arrange
            var request = new CreateGuestCommand()
            {
                Username = "guest",
                Password = "guest"
            };

            // Act
            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            // Assert
            var guest = await this.Context.Guests.Include(g => g.User)
                .SingleOrDefaultAsync(g => g.User.Username == request.Username, CancellationToken.None)
                .ConfigureAwait(false);

            Assert.NotNull(guest);
            CustomAssertAreEqual(request, guest);
        }

        [Fact]
        public async Task UserIsLogicallyDeleted_UpdatesHost()
        {
            await this.Context.Guests.AddAsync(new Domain.Entities.Guest()
            {
                IsDeleted = true,
                User = new Domain.Entities.User()
                {
                    IsDeleted = true,
                    Username = "guest",
                    Password = "steva"
                }
            }).ConfigureAwait(false);

            await this.Context.SaveChangesAsync().ConfigureAwait(false);

            var request = new CreateGuestCommand()
            {
                Username = "guest",
                Password = "guest",
                FirstName = "Milos"
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var guest = await this.Context.Guests.Include(g => g.User)
                .SingleOrDefaultAsync(g => g.User.Username == request.Username, CancellationToken.None)
                .ConfigureAwait(false);

            Assert.NotNull(guest);
            CustomAssertAreEqual(request, guest);
        }

        private static void CustomAssertAreEqual(CreateGuestCommand request, Domain.Entities.Guest host)
        {
            Assert.Equal(request.Username, host.User.Username);
            Assert.Equal(request.Password, host.User.Password);
            Assert.Equal(request.FirstName, host.User.FirstName);
            Assert.Equal(request.LastName, host.User.LastName);
            Assert.Equal(request.Gender, host.User.Gender);
            Assert.Equal(RoleNames.Guest, host.User.RoleName);
            Assert.False(host.User.IsDeleted);
            Assert.False(host.IsDeleted);
        }
    }
}