using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Commands
{
    public class UpdateApartmentCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateApartmentCommandHandler sut;
        private Apartment dbApartment;

        public UpdateApartmentCommandHandlerTests()
        {
            this.sut = new UpdateApartmentCommandHandler(this.Context);
        }

        [Fact]
        public async Task UpdatesApartment()
        {
            var request = new UpdateApartmentCommand()
            {
                Id = this.dbApartment.Id,
                ActivityState = ActivityStates.Inactive,
                ApartmentType = ApartmentTypes.SingleRoom,
                CheckInTime = "11:00:00",
                CheckOutTime = "20:15:33",
                Title = "My new title",
                Latitude = 33,
                Longitude = 34,
                CityName = "New York",
                StreetName = "Digit Street",
                StreetNumber = "33",
                CountryName = "America"
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            this.dbApartment = await this.Context.Apartments
                .Include(a => a.Location)
                .ThenInclude(l => l.Address)
                .SingleOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted)
                .ConfigureAwait(false);

            Assert.NotNull(this.dbApartment);
            Assert.Equal(this.dbApartment.Title, request.Title);
            Assert.NotNull(this.dbApartment.Location);
            Assert.Equal(this.dbApartment.Location.Latitude, request.Latitude);
            Assert.Equal(this.dbApartment.Location.Longitude, request.Longitude);
            Assert.NotNull(this.dbApartment.Location.Address);
            Assert.Equal(this.dbApartment.Location.Address.CountryName, request.CountryName);
        }

        protected override void LoadTestData()
        {
            this.dbApartment = this.Context.Add(new Apartment()
            {
                ActivityState = ActivityStates.Active,
                CheckInTime = "14:00:00",
                CheckOutTime = "10:00:00",
                Title = "My Title",
                ApartmentType = ApartmentTypes.Full,
                PricePerNight = 32,
                NumberOfRooms = 3,
                Location = new Location()
                {
                    Latitude = 2,
                    Longitude = 3,
                    Address = new Address()
                    {
                        CityName = "Novi Sad",
                        StreetName = "Djura lazevica",
                        StreetNumber = "25"
                    }
                }
            }).Entity;

            this.Context.SaveChanges();
        }
    }
}