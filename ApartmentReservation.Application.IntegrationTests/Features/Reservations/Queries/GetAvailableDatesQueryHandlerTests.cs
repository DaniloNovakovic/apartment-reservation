using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Queries.Legacy;
using ApartmentReservation.Common;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
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
            var availableDates = await this.sut.Handle(new GetAvailableDatesQuery() { ApartmentId = this.data.Apartment.Id }, CancellationToken.None).ConfigureAwait(false);

            Assert.All(
                availableDates,
                date => Assert.Contains(this.data.ForRentalDates, frd => DateTimeHelpers.AreSameDay(frd.Date, date)));
        }

        [Fact]
        public async Task WhenReserved_ReturnAvailable()
        {
            this.data.Context.Add(new Reservation()
            {
                ApartmentId = this.data.Apartment.Id,
                GuestId = this.data.Guest.UserId,
                ReservationStartDate = this.data.MinDate,
                NumberOfNightsRented = 1,
                ReservationState = ReservationStates.Accepted
            });

            this.data.Context.SaveChanges();

            var availableDates = await this.sut.Handle(new GetAvailableDatesQuery() { ApartmentId = this.data.Apartment.Id }, CancellationToken.None).ConfigureAwait(false);

            Assert.All(
                availableDates,
                date => Assert.Contains(this.data.ForRentalDates.Skip(1), frd => DateTimeHelpers.AreSameDay(frd.Date, date)));
        }

        [Fact]
        public async Task DoesNotReturnDaysBeforeToday()
        {
            this.data.Context.Add(new ForRentalDate()
            {
                ApartmentId = this.data.Apartment.Id,
                Date = DateTime.Now.AddDays(-5)
            });

            var availableDates = await this.sut.Handle(new GetAvailableDatesQuery() { ApartmentId = this.data.Apartment.Id }, CancellationToken.None).ConfigureAwait(false);

            Assert.DoesNotContain(availableDates, DateTimeHelpers.IsBeforeToday);
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
            this.Host = this.Context.Add(new Host() { User = new User() { Username = "host", Password = "host", RoleName = RoleNames.Host } }).Entity;
            this.Guest = this.Context.Add(new Guest() { User = new User() { Username = "guest", Password = "guest", RoleName = RoleNames.Guest } }).Entity;
            this.Apartment = this.Context.Add(new Apartment() { Title = "Test apartment" }).Entity;

            this.ForRentalDates = DateTimeHelpers.GetDateDayRange(this.MinDate, DaysToAdd)
                .Select(day => new ForRentalDate() { Date = day, Apartment = Apartment });

            this.Context.AddRange(this.ForRentalDates);
            this.Context.SaveChanges();
        }
    }
}