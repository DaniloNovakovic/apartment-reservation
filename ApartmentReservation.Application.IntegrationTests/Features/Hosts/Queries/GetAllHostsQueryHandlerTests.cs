using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.IntegrationTests.Utils;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Persistence;
using AutoMapper;
using Xunit;
using static ApartmentReservation.Application.Features.Hosts.GetAllHostsQuery;

namespace ApartmentReservation.Application.IntegrationTests.Features.Hosts.Queries
{
    [Collection("QueryCollection")]
    public class GetAllHostsQueryHandlerTests : IDisposable
    {
        private readonly ApartmentReservationDbContext context;
        private readonly IMapper mapper;
        private List<Host> dbHosts;

        public GetAllHostsQueryHandlerTests(QueryTestFixture fixture)
        {
            this.context = fixture.Context;
            this.mapper = fixture.Mapper;

            this.SeedData();
        }

        public void Dispose()
        {
            this.context.Hosts.RemoveRange(context.Hosts.ToList());
            this.context.Users.RemoveRange(context.Users.ToList());
            this.context.SaveChanges();
        }

        [Fact]
        public async Task GetAllHostsTest()
        {
            var sut = new GetAllHostsQueryHandler(this.context, this.mapper);
            var expectedResult = dbHosts.Select(h => h.User.Username);

            var result = await sut.Handle(new GetAllHostsQuery(), CancellationToken.None).ConfigureAwait(false);

            Assert.IsAssignableFrom<IEnumerable<HostDto>>(result);
            Assert.Equal(expectedResult, result.Select(r => r.Username));
        }

        private void SeedData()
        {
            if (context.Hosts.Any())
            {
                context.Hosts.RemoveRange(context.Hosts.ToList());
                context.Users.RemoveRange(context.Users.ToList());
                context.SaveChanges();
            }

            var user = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host };

            this.dbHosts = new List<Host>() { new Host() { User = user } };

            this.context.Hosts.AddRange(this.dbHosts);
            this.context.SaveChanges();
        }
    }
}