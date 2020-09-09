using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Amenities.Commands;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Amenities.Commands
{
    public class UpdateAmenityCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateAmenityCommandHandler sut;
        private Amenity dbAmenity;

        public UpdateAmenityCommandHandlerTests()
        {
            this.sut = new UpdateAmenityCommandHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            this.dbAmenity = this.Context.Amenities.Add(new Amenity() { Name = "Sofa" }).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenAmenityExists_UpdateAmenity()
        {
            await this.sut
                .Handle(new UpdateAmenityCommand() { Id = this.dbAmenity.Id, Name = "Bed" }, CancellationToken.None)
                .ConfigureAwait(false);

            var amenity = await this.Context.Amenities.FindAsync(this.dbAmenity.Id).ConfigureAwait(false);

            Assert.NotNull(amenity);
            Assert.Equal("Bed", amenity.Name);
        }

        [Fact]
        public async Task WhenAmenityDoesNotExist_ThrowNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new UpdateAmenityCommand() { Id = -1, Name = "Bed" }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}