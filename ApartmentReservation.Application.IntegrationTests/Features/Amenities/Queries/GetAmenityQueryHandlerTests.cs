using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Amenities.Queries;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Amenities.Queries
{
    public class GetAmenityQueryHandlerTests : InMemoryContextTestBase
    {
        private readonly GetAmenityQueryHandler sut;
        private Amenity dbAmenity;

        public GetAmenityQueryHandlerTests()
        {
            this.sut = new GetAmenityQueryHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            this.dbAmenity = this.Context.Amenities.Add(new Amenity { Name = "Sofa" }).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenAmenityExists_ReturnsAmenity()
        {
            var result = await this.sut
                .Handle(new GetAmenityQuery() { Id = this.dbAmenity.Id }, CancellationToken.None)
                .ConfigureAwait(false);

            var dtoResult = Assert.IsAssignableFrom<AmenityDto>(result);
            Assert.Equal(this.dbAmenity.Name, dtoResult.Name);
        }

        [Fact]
        public async Task WhenAmenityDoesNotExist_ThrowNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new GetAmenityQuery() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}