using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Queries;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Queries
{
    public class GetAllApartmentsQueryHandlerTests : InMemoryContextTestBase
    {
        private List<Apartment> dbApartments;
        private readonly GetAllApartmentsQueryHandler sut;

        public GetAllApartmentsQueryHandlerTests()
        {
            this.sut = new GetAllApartmentsQueryHandler(this.Context);
        }

        [Fact]
        public async Task GetAllTest()
        {
            var request = new GetAllApartmentsQuery();

            var apartments = await sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(dbApartments.Select(a => a.Id), apartments.Select(a => a.Id));
        }

        protected override void LoadTestData()
        {
            var apartments = new List<Apartment>()
            {
                new Apartment()
                {
                    ActivityState = ActivityStates.Active
                },
                new Apartment()
                {
                    ActivityState = ActivityStates.Inactive
                }
            };

            this.Context.AddRange(apartments);
            this.Context.SaveChanges();

            this.dbApartments = Context.Apartments.ToList();
        }
    }
}