using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Guests.Queries;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Guests.Queries
{
    public class GetAllGuestsQueryHandlerTests : InMemoryContextTestBase
    {
        private List<Guest> dbGuests;
        private readonly GetAllGuestsQueryHandler sut;

        public GetAllGuestsQueryHandlerTests()
        {
            this.sut = new GetAllGuestsQueryHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            var user = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest };

            this.dbGuests = new List<Guest>() { new Guest() { User = user } };

            this.Context.Guests.AddRange(this.dbGuests);
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task GetAllGuestsTest()
        {
            var expectedResult = this.dbGuests.Select(g => g.User.Username);

            var result = await this.sut.Handle(new GetAllGuestsQuery(), CancellationToken.None).ConfigureAwait(false);

            Assert.IsAssignableFrom<IEnumerable<GuestDto>>(result);
            Assert.Equal(expectedResult, result.Select(r => r.Username));
        }
    }
}