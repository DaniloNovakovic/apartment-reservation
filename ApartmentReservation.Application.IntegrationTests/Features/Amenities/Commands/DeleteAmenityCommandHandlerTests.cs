using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Amenities.Commands;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Amenities.Commands
{
    public class DeleteAmenityCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly DeleteAmenityCommandHandler sut;
        private Amenity dbAmenity;

        public DeleteAmenityCommandHandlerTests()
        {
            this.sut = new DeleteAmenityCommandHandler(this.Context);
        }

        protected override void LoadTestData()
        {
            this.dbAmenity = this.Context.Amenities.Add(new Amenity() { Name = "Sofa" }).Entity;
            this.Context.SaveChanges();
        }

        [Fact]
        public async Task WhenAmenityExists_DeletesAmenity()
        {
            await this.sut
                .Handle(new DeleteAmenityCommand() { Id = this.dbAmenity.Id }, CancellationToken.None)
                .ConfigureAwait(false);

            var amenity = await this.Context.Amenities.FindAsync(this.dbAmenity.Id).ConfigureAwait(false);

            Assert.True(amenity.IsDeleted);
        }

        [Fact]
        public async Task WhenAmenityDoesNotExist_ThrowNotFoundException()
        {
            await Assert
                .ThrowsAsync<NotFoundException>(async () => await this.sut.Handle(new DeleteAmenityCommand() { Id = -1 }, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}