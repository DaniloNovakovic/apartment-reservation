using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Hosts.Commands
{
    public class DeleteHostCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly DeleteHostCommandHandler sut;
        private Host dbHost;

        public DeleteHostCommandHandlerTests()
        {
            sut = new DeleteHostCommandHandler(Context);
        }

        protected override void LoadTestData()
        {
            var user = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host };

            this.dbHost = this.Context.Hosts.Add(new Host() { User = user }).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task UserExists_DeletesHostLogically()
        {
            await sut.Handle(new DeleteHostCommand() { Id = dbHost.UserId }, CancellationToken.None).ConfigureAwait(false);

            var host = await this.Context.Hosts.FindAsync(dbHost.UserId).ConfigureAwait(false);

            Assert.True(host.IsDeleted);
        }

        [Fact]
        public async Task UserDoesNotExist_ThrowsNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new DeleteHostCommand() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}