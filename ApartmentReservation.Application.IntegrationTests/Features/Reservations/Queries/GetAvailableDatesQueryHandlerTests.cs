using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Common;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Reservations.Queries
{
    public class GetAvailableDatesQueryHandlerTests : IClassFixture<GetAvailableDatesQueryDataSetup>
    {
        private readonly GetAvailableDatesQueryHandler sut;
        private readonly GetAvailableDatesQueryDataSetup data;

        public GetAvailableDatesQueryHandlerTests(GetAvailableDatesQueryDataSetup data)
        {
            this.sut = new GetAvailableDatesQueryHandler(data.Context);
            this.data = data;
        }

        [Fact]
        public async Task WhenNoReservations_ReturnForRentalDates()
        {
            var availableDates = await sut.Handle(new GetAvailableDatesQuery() { ApartmentId = data.Apartment.Id }, CancellationToken.None).ConfigureAwait(false);

            Assert.All(
                availableDates,
                date => Assert.Contains(data.ForRentalDates, frd => DateTimeHelpers.AreSameDay(frd.Date, date)));
        }

        [Fact]
        public async Task WhenReserved_ReturnAvailable()
        {
            data.Context.Add(new Reservation()
            {
                ApartmentId = data.Apartment.Id,
                GuestId = data.Guest.UserId,
                ReservationStartDate = data.MinDate,
                NumberOfNightsRented = 1,
                ReservationState = ReservationStates.Accepted
            });

            data.Context.SaveChanges();

            var availableDates = await sut.Handle(new GetAvailableDatesQuery() { ApartmentId = data.Apartment.Id }, CancellationToken.None).ConfigureAwait(false);

            Assert.All(
                availableDates,
                date => Assert.Contains(data.ForRentalDates.Skip(1), frd => DateTimeHelpers.AreSameDay(frd.Date, date)));
        }
    }

    public class GetAvailableDatesQueryDataSetup : InMemoryContextTestBase
    {
        public const int DaysToAdd = 5;

        public Host Host { get; private set; }
        public Guest Guest { get; private set; }
        public Apartment Apartment { get; private set; }
        public IEnumerable<ForRentalDate> ForRentalDates { get; private set; }
        public DateTime MinDate => DateTime.Now;

        protected override void LoadTestData()
        {
            this.Host = Context.Add(new Host() { User = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host } }).Entity;
            this.Guest = Context.Add(new Guest() { User = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest } }).Entity;
            this.Apartment = Context.Add(new Apartment() { Title = "Test apartment" }).Entity;

            this.ForRentalDates = DateTimeHelpers.GetDateDayRange(MinDate, DaysToAdd)
                .Select(day => new ForRentalDate() { Date = day, Apartment = Apartment });

            Context.AddRange(ForRentalDates);
            Context.SaveChanges();
        }
    }
}