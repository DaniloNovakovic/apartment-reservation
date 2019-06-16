using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Queries
{
    public class GetAllApartmentsQueryDataSetup : InMemoryContextTestBase
    {
        public List<Apartment> DbApartments { get; private set; }
        public Host DbHost { get; private set; }

        protected override void LoadTestData()
        {
            this.DbHost = this.Context.Add(new Host()
            {
                User = new User()
                {
                    Username = "host",
                    Password = "host",
                    RoleName = RoleNames.Host
                }
            }).Entity;

            this.Context.SaveChanges();

            var apartments = new[]
            {
                new Apartment() { ActivityState = ActivityStates.Active, Host = DbHost },
                new Apartment() { ActivityState = ActivityStates.Inactive, Host = DbHost }
            };

            var amenities = new[]
            {
                new Amenity(){Name="TV"},
                new Amenity(){Name="Heating"}
            };

            Context.AddRange(
                new ApartmentAmenity() { Apartment = apartments[0], Amenity = amenities[0] },
                new ApartmentAmenity() { Apartment = apartments[0], Amenity = amenities[1] },
                new ApartmentAmenity() { Apartment = apartments[1], Amenity = amenities[0] }
            );

            this.Context.SaveChanges();

            this.DbApartments = this.Context.Apartments.ToList();
        }
    }

    public class GetAllApartmentsQueryHandlerTests : IClassFixture<GetAllApartmentsQueryDataSetup>
    {
        private readonly GetAllApartmentsQueryHandler sut;
        private readonly IEnumerable<Apartment> dbApartments;
        private readonly Host dbHost;

        public GetAllApartmentsQueryHandlerTests(GetAllApartmentsQueryDataSetup data)
        {
            this.sut = new GetAllApartmentsQueryHandler(data.Context);
            this.dbApartments = data.DbApartments.Where(a => !a.IsDeleted);
            this.dbHost = data.DbHost;
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
        public async Task FilterByHostId_ReturnApartmentsWithRequestedHostId()
        {
            var request = new GetAllApartmentsQuery()
            {
                HostId = dbHost.UserId
            };

            var apartments = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.NotEmpty(apartments);
            Assert.All(apartments, apartment => Assert.Equal(apartment.Host.Id, request.HostId));
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