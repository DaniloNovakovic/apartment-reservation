using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
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
                HostId = dbHost.UserId,
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