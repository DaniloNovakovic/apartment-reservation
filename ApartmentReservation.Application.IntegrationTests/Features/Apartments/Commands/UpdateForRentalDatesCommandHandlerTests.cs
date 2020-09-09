using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Commands
{
    public class UpdateForRentalDatesCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateForRentalDatesCommandHandler sut;
        private Apartment dbApartment;

        public UpdateForRentalDatesCommandHandlerTests()
        {
            this.sut = new UpdateForRentalDatesCommandHandler(this.Context);
        }

        [Fact]
        public async Task UpdatesForRentalDates()
        {
            var requestedDate = new DateTime(year: 2019, month: 5, day: 17);

            var request = new UpdateForRentalDatesCommand()
            {
                ApartmentId = this.dbApartment.Id,
                ForRentalDates = new[] { requestedDate }
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbRentalDates = await this.Context.ForRentalDates
                .Where(frd => frd.ApartmentId == request.ApartmentId && !frd.IsDeleted)
                .ToListAsync()
                .ConfigureAwait(false);

            var rentalDate = Assert.Single(dbRentalDates);
            Assert.Equal(requestedDate, rentalDate.Date);
        }

        protected override void LoadTestData()
        {
            this.dbApartment = this.Context.Add(new Apartment()
            {
                Title = "Test Apartment",
                ActivityState = ActivityStates.Inactive,
                ApartmentType = ApartmentTypes.Full
            }).Entity;

            this.Context.AddRange(new ForRentalDate()
            {
                Apartment = dbApartment,
                Date = new DateTime(year: 2018, month: 4, day: 5)
            });

            this.Context.SaveChanges();
        }
    }
}