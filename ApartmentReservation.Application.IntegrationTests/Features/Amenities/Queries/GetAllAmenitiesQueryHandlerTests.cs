using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Amenities.Queries;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Amenities.Queries
{
    public class GetAllAmenitiesQueryHandlerTests : InMemoryContextTestBase
    {
        private List<Amenity> dbAmenities;
        private readonly GetAllAmenitiesQueryHandler sut;

        public GetAllAmenitiesQueryHandlerTests()
        {
            this.sut = new GetAllAmenitiesQueryHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            this.dbAmenities = new List<Amenity>()
            {
                new Amenity() { Name = "Sofa" },
                new Amenity() { Name = "Bed" }
            };

            this.Context.Amenities.AddRange(this.dbAmenities);
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task NoFilter_ReturnAllAmenities()
        {
            var expectedResult = this.dbAmenities.Select(g => g.Name);

            var result = await this.sut.Handle(new GetAllAmenitiesQuery(), CancellationToken.None).ConfigureAwait(false);

            Assert.IsAssignableFrom<IEnumerable<AmenityDto>>(result);
            Assert.Equal(expectedResult, result.Select(r => r.Name));
        }

        [Fact]
        public async Task FilterBySearch_ReturnAllAmenitiesThatContainName()
        {
            var result = await this.sut.Handle(new GetAllAmenitiesQuery() { Search = "so" }, CancellationToken.None).ConfigureAwait(false);

            var amenity = Assert.Single<AmenityDto>(result);
            Assert.Equal("Sofa", amenity.Name);
        }
    }
}