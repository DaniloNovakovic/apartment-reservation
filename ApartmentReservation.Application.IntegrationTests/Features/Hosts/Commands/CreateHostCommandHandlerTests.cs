using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Hosts.Commands
{
    public class CreateHostCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly CreateHostCommandHandler sut;

        public CreateHostCommandHandlerTests()
        {
            this.sut = new CreateHostCommandHandler(this.Context);
        }

        [Fact]
        public async Task UserDoesNotExist_CreatesHost()
        {
            // Arrange
            var request = new CreateHostCommand()
            {
                Username = "host",
                Password = "host",
                RoleName = RoleNames.Host
            };

            // Act
            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            // Assert
            var host = await this.Context.Hosts.Include(h => h.User)
                .SingleOrDefaultAsync(h => h.User.Username == request.Username, CancellationToken.None)
                .ConfigureAwait(false);

            Assert.NotNull(host);
            CustomAssertAreEqual(request, host);
        }

        [Fact]
        public async Task UserIsLogicallyDeleted_UpdatesHost()
        {
            await this.Context.Hosts.AddAsync(new Domain.Entities.Host()
            {
                IsDeleted = true,
                User = new Domain.Entities.User()
                {
                    IsDeleted = true,
                    Username = "host",
                    Password = "steva"
                }
            }).ConfigureAwait(false);

            await this.Context.SaveChangesAsync().ConfigureAwait(false);

            var request = new CreateHostCommand()
            {
                Username = "host",
                Password = "host",
                FirstName = "Milos",
                RoleName = RoleNames.Host
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var host = await this.Context.Hosts.Include(h => h.User)
                .SingleOrDefaultAsync(h => h.User.Username == request.Username, CancellationToken.None)
                .ConfigureAwait(false);

            Assert.NotNull(host);
            CustomAssertAreEqual(request, host);
        }

        private static void CustomAssertAreEqual(CreateHostCommand request, Domain.Entities.Host host)
        {
            Assert.Equal(request.Username, host.User.Username);
            Assert.Equal(request.Password, host.User.Password);
            Assert.Equal(request.FirstName, host.User.FirstName);
            Assert.Equal(request.LastName, host.User.LastName);
            Assert.Equal(request.Gender, host.User.Gender);
            Assert.Equal(RoleNames.Host, host.User.RoleName);
            Assert.False(host.User.IsDeleted);
            Assert.False(host.IsDeleted);
        }
    }
}