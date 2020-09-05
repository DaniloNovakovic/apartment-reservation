using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Commands
{
    public class CreateApartmentCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly CreateApartmentCommandHandler sut;
        private Host dbHost;

        public CreateApartmentCommandHandlerTests()
        {
            this.sut = new CreateApartmentCommandHandler(this.Context);
        }

        [Fact]
        public async Task CreateApartment()
        {
            var request = new CreateApartmentCommand()
            {
                HostId = this.dbHost.UserId,
                PricePerNight = 10,
                ApartmentType = ApartmentTypes.Full,
                Title = "My fabulous apartment",
                NumberOfRooms = 5
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbApartment = await this.Context.Apartments.FirstOrDefaultAsync();

            Assert.NotNull(dbApartment);
            Assert.False(dbApartment.IsDeleted);
        }

        [Fact]
        public async Task ReturnsIdOfCreatedElement()
        {
            var request = new CreateApartmentCommand()
            {
                HostId = this.dbHost.UserId,
                PricePerNight = 10,
                ApartmentType = ApartmentTypes.Full,
                Title = "My fabulous apartment",
                NumberOfRooms = 5
            };

            var result = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbApartment = await this.Context.Apartments.FirstOrDefaultAsync();

            Assert.Equal(dbApartment.Id, result.Id);
        }

        [Fact]
        public async Task CreatesApartmentWithLocationAndAddress()
        {
            var request = new CreateApartmentCommand()
            {
                HostId = this.dbHost.UserId,
                Longitude = 40.3,
                Latitude = -73.556,
                CityName = "New York",
                StreetName = "Weird street"
            };

            var result = await this.sut.Handle(request, CancellationToken.None);

            var dbApartment = await this.Context.Apartments
                .Include(a => a.Location)
                .ThenInclude(l => l.Address)
                .FirstOrDefaultAsync();

            Assert.NotNull(dbApartment.Location);

            var location = dbApartment.Location;
            Assert.Equal(location.Longitude, request.Longitude);
            Assert.Equal(location.Latitude, request.Latitude);

            var address = location.Address;
            Assert.Equal(address.CityName, request.CityName);
            Assert.Equal(address.StreetName, request.StreetName);
        }

        protected override void LoadTestData()
        {
            this.dbHost = this.Context.Add(new Host()
            {
                User = new User()
                {
                    Username = "host",
                    Password = "host",
                    RoleName = RoleNames.Host
                }
            }).Entity;

            this.Context.SaveChanges();
        }
    }
}