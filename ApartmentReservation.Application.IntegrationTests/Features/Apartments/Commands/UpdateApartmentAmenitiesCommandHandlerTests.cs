using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Commands
{
    public class UpdateApartmentAmenitiesCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateApartmentAmenitiesCommandHandler sut;
        private Apartment dbApartment;

        public UpdateApartmentAmenitiesCommandHandlerTests()
        {
            this.sut = new UpdateApartmentAmenitiesCommandHandler(this.Context);
        }

        [Fact]
        public async Task UpdatesAmenitiesInApartment()
        {
            var reqAmenity = this.Context.Add(new Amenity() { Name = "Microwave" }).Entity;

            var request = new UpdateApartmentAmenitiesCommand()
            {
                ApartmentId = this.dbApartment.Id,
                Amenities = new List<AmenityDto>() { new AmenityDto(reqAmenity) }
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            this.dbApartment = await this.Context.Apartments
                .Include("ApartmentAmenities.Amenity")
                .SingleOrDefaultAsync(a => a.Id == this.dbApartment.Id, CancellationToken.None).ConfigureAwait(false);

            var dbAmenities = this.dbApartment.ApartmentAmenities
                .Where(x => !x.IsDeleted)
                .Select(x => x.Amenity)
                .Where(a => !a.IsDeleted);

            var dbAmenity = Assert.Single(dbAmenities);
            Assert.Equal(reqAmenity.Name, dbAmenity.Name);
        }

        protected override void LoadTestData()
        {
            this.dbApartment = this.Context.Add(new Apartment()
            {
                Title = "Test Apartment",
                ActivityState = ActivityStates.Inactive
            }).Entity;

            var amenities = new[]
            {
                new Amenity(){Name="TV"},
                new Amenity(){Name="Kitchen"}
            };

            this.Context.AddRange(
                new ApartmentAmenity() { Amenity = amenities[0], Apartment = dbApartment },
                new ApartmentAmenity() { Amenity = amenities[1], Apartment = dbApartment }
            );

            this.Context.SaveChanges();
        }
    }
}