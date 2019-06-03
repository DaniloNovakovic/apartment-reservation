using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.IntegrationTests.Utils;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Persistence;
using AutoMapper;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Hosts.Queries
{
    [Collection("QueryCollection")]
    public class GetHostQueryHandlerTests : IDisposable
    {
        private readonly ApartmentReservationDbContext context;
        private readonly IMapper mapper;
        private Host dbHost;

        public GetHostQueryHandlerTests(QueryTestFixture fixture)
        {
            this.context = fixture.Context;
            this.mapper = fixture.Mapper;

            this.SeedData();
        }

        public void Dispose()
        {
            this.context.Hosts.Remove(this.dbHost);
            this.context.Users.Remove(this.dbHost.User);
            this.context.SaveChanges();
        }

        [Fact]
        public async Task WhenHostExists_ReturnHost()
        {
            var sut = new GetHostQueryHandler(this.context, this.mapper);

            var result = await sut.Handle(new GetHostQuery() { Id = this.dbHost.UserId }, CancellationToken.None).ConfigureAwait(false);

            Assert.IsAssignableFrom<HostDto>(result);
            Assert.Equal(this.dbHost.User.Username, result.Username);
        }

        [Fact]
        public async Task WhenHostDoesNotExist_ThrowNotFoundException()
        {
            var sut = new GetHostQueryHandler(this.context, this.mapper);

            await Assert
                .ThrowsAsync<NotFoundException>(async () => await sut.Handle(new GetHostQuery() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        private void SeedData()
        {
            if (this.context.Hosts.Any())
            {
                this.context.Hosts.RemoveRange(this.context.Hosts.ToList());
                this.context.Users.RemoveRange(this.context.Users.ToList());
                this.context.SaveChanges();
            }

            var user = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host };

            this.dbHost = new Host() { User = user };

            this.dbHost = this.context.Hosts.Add(this.dbHost).Entity;
            this.context.SaveChanges();
        }
    }
}