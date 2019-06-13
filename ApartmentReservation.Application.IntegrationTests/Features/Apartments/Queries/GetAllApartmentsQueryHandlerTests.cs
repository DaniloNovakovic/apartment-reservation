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
    public class GetAllApartmentsQueryDataSetup : InMemoryContextTestBase
    {
        public List<Apartment> DbApartments { get; private set; }

        protected override void LoadTestData()
        {
            var activeApartment = new Apartment() { ActivityState = ActivityStates.Active };
            var inactiveApartment = new Apartment() { ActivityState = ActivityStates.Inactive };

            activeApartment.Amenities.Add(new Amenity() { Name = "TV" });
            activeApartment.Amenities.Add(new Amenity() { Name = "Heating" });

            var apartments = new List<Apartment>()
            {
               activeApartment,
               inactiveApartment
            };

            this.Context.AddRange(apartments);
            this.Context.SaveChanges();

            this.DbApartments = this.Context.Apartments.ToList();
        }
    }

    public class GetAllApartmentsQueryHandlerTests : IClassFixture<GetAllApartmentsQueryDataSetup>
    {
        private readonly GetAllApartmentsQueryHandler sut;
        private readonly IEnumerable<Apartment> dbApartments;

        public GetAllApartmentsQueryHandlerTests(GetAllApartmentsQueryDataSetup data)
        {
            this.sut = new GetAllApartmentsQueryHandler(data.Context);
            this.dbApartments = data.DbApartments.Where(a => !a.IsDeleted);
        }

        [Theory]
        [InlineData(ActivityStates.Active)]
        [InlineData(ActivityStates.Inactive)]
        public async Task FilterByActiveState_ReturnApartmentsWithRequestedActivity(string activityState)
        {
            var request = new GetAllApartmentsQuery()
            {
                ActivityState = activityState
            };

            var apartments = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.NotEmpty(apartments);
            Assert.All(apartments, apartment => Assert.Equal(apartment.ActivityState, activityState));
        }

        [Theory]
        [InlineData("TV")]
        [InlineData("Heating")]
        public async Task FilterByAmenity_ReturnApartmentsWithRequestedAmenity(string amenityName)
        {
            var request = new GetAllApartmentsQuery()
            {
                AmenityName = amenityName
            };

            var apartments = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.NotEmpty(apartments);
            Assert.All(apartments, apartment =>
            {
                Assert.Contains(apartment.Amenities, (amenity) =>
                {
                    return string.Equals(amenity.Name, amenityName, System.StringComparison.OrdinalIgnoreCase);
                });
            });
        }

        [Fact]
        public async Task NoFilter_ReturnAllApartments()
        {
            var request = new GetAllApartmentsQuery();

            var apartments = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(this.dbApartments.Select(a => a.Id), apartments.Select(a => a.Id));
        }
    }
}