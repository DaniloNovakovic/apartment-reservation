using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Hosts.Commands
{
    public class CreateHostCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly CreateHostCommandHandler sut;

        public CreateHostCommandHandlerTests()
        {
            this.sut = new CreateHostCommandHandler(this.Context, this.Mapper);
        }

        [Fact]
        public async Task WhenInvoked_CreatesHost()
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
            Assert.Equal(request.Username, host.User.Username);
            Assert.Equal(request.Password, host.User.Password);
            Assert.Equal(request.RoleName, host.User.RoleName);
        }
    }
}