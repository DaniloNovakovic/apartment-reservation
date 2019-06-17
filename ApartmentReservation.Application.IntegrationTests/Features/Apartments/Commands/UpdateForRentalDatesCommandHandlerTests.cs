using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Apartments.Commands
{
    public class UpdateForRentalDatesCommandHandlerTests : InMemoryContextTestBase
    {
        private UpdateForRentalDatesCommandHandler sut;
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
                ApartmentId = dbApartment.Id,
                ForRentalDates = new[] { requestedDate }
            };

            await sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbRentalDates = await Context.ForRentalDates
                .Where(frd => frd.ApartmentId == request.ApartmentId && !frd.IsDeleted)
                .ToListAsync()
                .ConfigureAwait(false);

            var rentalDate = Assert.Single(dbRentalDates);
            Assert.Equal(requestedDate, rentalDate.Date);
        }

        protected override void LoadTestData()
        {
            this.dbApartment = Context.Add(new Apartment()
            {
                Title = "Test Apartment",
                ActivityState = ActivityStates.Inactive,
                ApartmentType = ApartmentTypes.Full
            }).Entity;

            Context.AddRange(new ForRentalDate()
            {
                Apartment = dbApartment,
                Date = new DateTime(year: 2018, month: 4, day: 5)
            });

            Context.SaveChanges();
        }
    }
}