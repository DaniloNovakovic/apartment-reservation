using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Hosts.Commands
{
    public class UpdateHostCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateHostCommandHandler sut;
        private Host dbHost;

        public UpdateHostCommandHandlerTests()
        {
            this.sut = new UpdateHostCommandHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var host = new Host()
            {
                User = new User()
                {
                    Username = "host",
                    Password = "host"
                }
            };

            this.dbHost = this.Context.Add(host).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenHostExists_UpdateHost()
        {
            var request = new UpdateHostCommand()
            {
                Id = this.dbHost.UserId,
                FirstName = "Danilo",
                LastName = "Novakovic",
                Gender = "Male",
                Password = "codex"
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var host = await this.Context.Hosts.Include(h => h.User)
                .SingleOrDefaultAsync(h => h.UserId == request.Id)
                .ConfigureAwait(false);

            Assert.NotNull(host);
            CustomAssertAreEqual(request, host);
        }

        [Fact]
        public async Task UserDoesNotExist_ThrowsNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new UpdateHostCommand() { Id = -1, Password = "codex" }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        private static void CustomAssertAreEqual(UpdateHostCommand request, Domain.Entities.Host host)
        {
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