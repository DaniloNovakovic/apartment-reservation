using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Reservations.Queries
{
    public class GetAllReservationsQueryHandlerTests : IClassFixture<GetAllReservationsQueryDataSetup>
    {
        private readonly GetAllReservationsQueryHandler sut;
        private readonly List<Host> dbHosts;
        private readonly List<Guest> dbGuests;
        private readonly List<Reservation> dbReservations;

        public GetAllReservationsQueryHandlerTests(GetAllReservationsQueryDataSetup data)
        {
            this.sut = new GetAllReservationsQueryHandler(data.Context);
            this.dbHosts = data.DbHosts;
            this.dbGuests = data.DbGuests;
            this.dbReservations = data.DbReservations;
        }

        [Fact]
        public async Task NoFilter_ReturnAllReservations()
        {
            var request = new GetAllReservationsQuery();

            var response = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(this.dbReservations.Select(r => r.Id), response.Select(r => r.Id));
        }

        [Fact]
        public async Task FilterByHostId_ReturnAllReservationsForApartmentsRentedByHost()
        {
            var request = new GetAllReservationsQuery() { HostId = this.dbHosts[0].UserId };

            var response = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var reservation = Assert.Single(response);
            Assert.Equal(request.HostId, reservation.Apartment.Host.Id);
        }

        [Fact]
        public async Task FilterByGuestId_ReturnAllReservationsForApartmentsRentedByGuest()
        {
            var request = new GetAllReservationsQuery() { GuestId = this.dbGuests[0].UserId };

            var response = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var reservation = Assert.Single(response);
            Assert.Equal(request.GuestId, reservation.Guest.Id);
        }
    }

    public class GetAllReservationsQueryDataSetup : InMemoryContextTestBase
    {
        public List<Apartment> DbApartments { get; private set; }
        public List<Guest> DbGuests { get; private set; }
        public List<Host> DbHosts { get; private set; }
        public List<Reservation> DbReservations { get; private set; }

        protected override void LoadTestData()
        {
            var hosts = new[]
            {
                new Host(){ User = new User() { Username = "host1", Password = "host", RoleName = RoleNames.Host }},
                new Host(){ User = new User() { Username = "host2", Password = "host", RoleName = RoleNames.Host }}
            };

            var guests = new[]
            {
                new Guest(){User = new User() { Username = "guest1", Password = "guest", RoleName = RoleNames.Guest }},
                new Guest(){User = new User() { Username = "guest2", Password = "guest", RoleName = RoleNames.Guest }}
            };

            this.Context.AddRange(hosts);
            this.Context.AddRange(guests);

            this.Context.SaveChanges();

            var apartments = new[]
            {
                new Apartment() { Title="Test Apartment 1", ActivityState = ActivityStates.Active, Host = hosts[0] },
                new Apartment() { Title="Test Apartment 2", ActivityState = ActivityStates.Active, Host = hosts[1] }
            };

            this.Context.SaveChanges();

            this.Context.AddRange(new Reservation() { Guest = guests[0], Apartment = apartments[0] }, new Reservation() { Guest = guests[1], Apartment = apartments[1] });

            this.Context.SaveChanges();

            this.DbApartments = this.Context.Apartments.ToList();
            this.DbGuests = this.Context.Guests.ToList();
            this.DbHosts = this.Context.Hosts.ToList();
            this.DbReservations = this.Context.Reservations
                .Include(r => r.Apartment)
                .ThenInclude(a => a.Host)
                .ThenInclude(h => h.User)
                .ToList();
        }
    }
}