using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Hosts.Queries
{
    public class GetHostQueryHandlerTests : InMemoryContextTestBase
    {
        private Host dbHost;
        private readonly GetHostQueryHandler sut;

        public GetHostQueryHandlerTests()
        {
            this.sut = new GetHostQueryHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var user = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host };

            this.dbHost = new Host() { User = user };

            this.dbHost = this.Context.Hosts.Add(this.dbHost).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenHostExists_ReturnHost()
        {
            var result = await this.sut.Handle(new GetHostQuery() { Id = this.dbHost.UserId }, CancellationToken.None).ConfigureAwait(false);

            Assert.IsAssignableFrom<HostDto>(result);
            Assert.Equal(this.dbHost.User.Username, result.Username);
        }

        [Fact]
        public async Task WhenHostDoesNotExist_ThrowNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new GetHostQuery() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}