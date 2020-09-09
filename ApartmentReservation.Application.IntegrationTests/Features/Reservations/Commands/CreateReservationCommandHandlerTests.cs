using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Application.Features.Reservations.Commands;
using ApartmentReservation.Common;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Moq;
using Xunit;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Application.Features.Reservations.Queries.Legacy;

namespace ApartmentReservation.Application.IntegrationTests.Features.Reservations.Commands
{
    public class CreateReservationCommandHandlerTests : InMemoryContextTestBase
    {
        private const int DaysToAdd = 3;
        private readonly DateTime minDate = DateTime.Now;
        private readonly DateTime maxDate = DateTime.Now.AddDays(DaysToAdd);
        private readonly Mock<IMediator> mediatorMock;
        private readonly Mock<ICostCalculator> costCalculatorMock;
        private readonly CreateReservationCommandHandler sut;
        private Guest guest;
        private Apartment apartment;
        private IEnumerable<ForRentalDate> forRentalDates;

        public CreateReservationCommandHandlerTests()
        {
            this.mediatorMock = new Mock<IMediator>();
            this.costCalculatorMock = new Mock<ICostCalculator>();
            this.sut = new CreateReservationCommandHandler(this.Context, this.mediatorMock.Object, this.costCalculatorMock.Object);

            this.costCalculatorMock.Setup(m => m.CalculateTotalCostAsync(It.IsAny<GetTotalCostArgs>(), It.IsAny<CancellationToken>()))
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

            this.forRentalDates = DateTimeHelpers.GetDateDayRange(DateTime.Now, DaysToAdd)
                .Select(day => new ForRentalDate() { Date = day, Apartment = apartment });

            this.Context.AddRange(this.forRentalDates);

            this.Context.SaveChanges();
        }
    }
}