using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Reservations.Commands;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Domain.Constants;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Reservations.Commands
{
    public class CreateReservationCommandHandlerTests : InMemoryContextTestBase
    {
        private const int DaysToAdd = 3;
        private readonly DateTime minDate = DateTime.Now;
        private readonly DateTime maxDate = DateTime.Now.AddDays(DaysToAdd);
        private readonly Mock<IMediator> mediatorMock;
        private readonly CreateReservationCommandHandler sut;
        private Guest guest;
        private Apartment apartment;
        private IEnumerable<ForRentalDate> forRentalDates;

        public CreateReservationCommandHandlerTests()
        {
            this.mediatorMock = new Mock<IMediator>();
            this.sut = new CreateReservationCommandHandler(this.Context, this.mediatorMock.Object);

            this.mediatorMock.Setup(m => m.Send(It.IsAny<GetTotalCostQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(25);
        }

        [Fact]
        public async Task WhenApartmantIsAvailable_CreateReservationWithCreatedState()
        {
            this.mediatorMock.Setup(m => m.Send(It.IsAny<GetAvailableDatesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.forRentalDates.Select(frd => frd.Date));

            var request = new CreateReservationCommand()
            {
                ApartmentId = this.apartment.Id,
                GuestId = this.guest.UserId,
                StartDate = minDate,
                NumberOfNights = DaysToAdd
            };

            var response = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            var reservation = this.Context.Reservations.SingleOrDefault(r => r.Id == response.Id && !r.IsDeleted);

            Assert.NotNull(reservation);
            Assert.Equal(ReservationStates.Created, reservation.ReservationState);
        }

        [Fact]
        public async Task WhenApartmantIsUnavailable_ThrowApartmentUnavailableException()
        {
            this.mediatorMock.Setup(m => m.Send(It.IsAny<GetAvailableDatesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.forRentalDates.Select(frd => frd.Date).Skip(2));

            var request = new CreateReservationCommand()
            {
                ApartmentId = this.apartment.Id,
                GuestId = this.guest.UserId,
                StartDate = minDate,
                NumberOfNights = DaysToAdd
            };

            await Assert
                .ThrowsAsync<ApartmentUnavailableException>(async () => await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        protected override void LoadTestData()
        {
            this.guest = this.Context.Add(new Guest()
            {
                User = new User()
                {
                    Username = "guest",
                    Password = "guest",
                    RoleName = RoleNames.Guest
                }
            }).Entity;

            this.apartment = this.Context.Add(new Apartment()).Entity;

            this.forRentalDates = this.GetDateRange(DateTime.Now, DaysToAdd)
                .Select(day => new ForRentalDate() { Date = day, Apartment = apartment });

            this.Context.AddRange(this.forRentalDates);

            this.Context.SaveChanges();
        }

        ///<summary>Returns [from, from+1day,..., from+numberOfDays] - closed interval(n+1)</summary>
        private IEnumerable<DateTime> GetDateRange(DateTime from, int numberOfDays)
        {
            var days = new List<DateTime>();
            for (int i = 0; i <= numberOfDays; ++i)
            {
                days.Add(from.AddDays(i));
            }
            return days;
        }
    }
}