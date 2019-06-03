using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Hosts.Queries
{
    public class GetAllHostsQueryHandlerTests : InMemoryContextTestBase
    {
        private List<Host> dbHosts;
        private readonly GetAllHostsQueryHandler sut;

        public GetAllHostsQueryHandlerTests()
        {
            this.sut = new GetAllHostsQueryHandler(this.Context, this.Mapper);
        }

        protected override void LoadTestData()
        {
            var user = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host };

            this.dbHosts = new List<Host>() { new Host() { User = user } };

            this.Context.Hosts.AddRange(this.dbHosts);
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task GetAllHostsTest()
        {
            var expectedResult = this.dbHosts.Select(h => h.User.Username);

            var result = await this.sut.Handle(new GetAllHostsQuery(), CancellationToken.None).ConfigureAwait(false);

            Assert.IsAssignableFrom<IEnumerable<HostDto>>(result);
            Assert.Equal(expectedResult, result.Select(r => r.Username));
        }
    }
}