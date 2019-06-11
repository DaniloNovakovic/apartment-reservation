using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Amenities.Commands;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Amenities.Commands
{
    public class CreateAmenityCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly CreateAmenityCommandHandler sut;
        private Amenity dbAmenity;

        public CreateAmenityCommandHandlerTests()
        {
            this.sut = new CreateAmenityCommandHandler(this.Context);
        }

        [Fact]
        public async Task WhenDoesNotExist_CreateAmenity()
        {
            const string AmenityName = "Sofa";

            await this.sut
                .Handle(new CreateAmenityCommand() { Name = AmenityName }, CancellationToken.None)
                .ConfigureAwait(false);

            var dbAmenity = await this.Context.Amenities
                .SingleOrDefaultAsync(a => a.Name == AmenityName, CancellationToken.None)
                .ConfigureAwait(false);

            Assert.NotNull(dbAmenity);
            Assert.Equal(AmenityName, dbAmenity.Name);
            Assert.False(dbAmenity.IsDeleted);
        }

        [Fact]
        public async Task WhenIsLogicallyDeleted_UpdateAmenity()
        {
            await this.LoadTestDataAsync().ConfigureAwait(false);

            await this.sut
                .Handle(new CreateAmenityCommand() { Name = this.dbAmenity.Name }, CancellationToken.None)
                .ConfigureAwait(false);

            var amenity = await this.Context.Amenities
                .SingleOrDefaultAsync(a => a.Id == this.dbAmenity.Id)
                .ConfigureAwait(false);

            Assert.NotNull(amenity);
            Assert.Equal(this.dbAmenity.Name, amenity.Name);
            Assert.False(amenity.IsDeleted);
        }

        protected override async Task LoadTestDataAsync()
        {
            var amenityEntry = await this.Context.Amenities
                 .AddAsync(new Amenity() { Name = "Bed", IsDeleted = true }).ConfigureAwait(false);

            this.dbAmenity = amenityEntry.Entity;

            await this.Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}