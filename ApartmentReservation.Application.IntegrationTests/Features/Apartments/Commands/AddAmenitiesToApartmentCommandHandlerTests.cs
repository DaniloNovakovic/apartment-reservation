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
    public class AddAmenitiesToApartmentCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly AddAmenitiesToApartmentCommandHandler sut;
        private Apartment dbApartment;

        public AddAmenitiesToApartmentCommandHandlerTests()
        {
            this.sut = new AddAmenitiesToApartmentCommandHandler(this.Context);
        }

        [Fact]
        public async Task AddsAmenitiesToApartment()
        {
            var amenities = await this.Context.Amenities.Select(a => new AmenityDto(a)).ToListAsync();

            var request = new AddAmenitiesToApartmentCommand()
            {
                ApartmentId = this.dbApartment.Id,
                Amenities = amenities
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            this.dbApartment = await this.Context.Apartments
                .Include("ApartmentAmenities.Amenity")
                .SingleOrDefaultAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(amenities.Select(a => a.Name), this.dbApartment.Amenities.Select(a => a.Name));
        }

        protected override void LoadTestData()
        {
            this.dbApartment = this.Context.Add(new Apartment()
            {
                Title = "Test Apartment",
                ActivityState = ActivityStates.Inactive
            }).Entity;

            this.Context.AddRange(new[]
            {
                new Amenity(){Name="TV"},
                new Amenity(){Name="Kitchen"}
            });

            this.Context.SaveChanges();
        }
    }
}