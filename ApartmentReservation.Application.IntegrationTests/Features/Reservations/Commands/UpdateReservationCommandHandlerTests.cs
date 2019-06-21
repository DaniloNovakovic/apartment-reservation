﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Commands;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Reservations.Commands
{
    public class UpdateReservationCommandHandlerTests : InMemoryContextTestBase
    {
        private readonly UpdateReservationCommandHandler sut;
        private Reservation reservation;

        public UpdateReservationCommandHandlerTests()
        {
            this.sut = new UpdateReservationCommandHandler(this.Context);
        }

        [Fact]
        public async Task UpdatesReservation()
        {
            var request = new UpdateReservationCommand()
            {
                Id = this.reservation.Id,
                ReservationState = ReservationStates.Accepted
            };

            await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var dbReservation = await this.Context.Reservations.FindAsync(request.Id).ConfigureAwait(false);

            Assert.Equal(request.ReservationState, dbReservation.ReservationState);
        }

        protected override void LoadTestData()
        {
            var apartment = this.Context.Add(new Apartment()
            {
                ActivityState = ActivityStates.Active,
                Title = "Test apartment"
            }).Entity;

            var guest = this.Context.Add(new Guest()
            {
                User = new User()
                {
                    Username = "guest",
                    Password = "guest",
                    RoleName = RoleNames.Guest
                }
            }).Entity;

            this.reservation = this.Context.Add(new Reservation()
            {
                Apartment = apartment,
                Guest = guest,
                ReservationStartDate = DateTime.Now,
                ReservationState = ReservationStates.Created,
                TotalCost = 20,
                NumberOfNightsRented = 1
            }).Entity;

            this.Context.SaveChanges();
        }
    }
}