using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Infrastructure;
using Moq;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Features.Reservations.Queries
{
    public class GetTotalCostQueryHandlerTests : InMemoryContextTestBase
    {
        private readonly Mock<IHolidayService> holidayServiceMock;
        private readonly GetTotalCostQueryHandler sut;
        private Apartment apartment;

        public GetTotalCostQueryHandlerTests()
        {
            this.holidayServiceMock = new Mock<IHolidayService>();
            this.sut = new GetTotalCostQueryHandler(this.Context, this.holidayServiceMock.Object);
            HolidayRate = GetTotalCostQueryHandler.HolidayRate;
            WeekendRate = GetTotalCostQueryHandler.WeekendRate;
        }

        public double HolidayRate { get; }
        public double WeekendRate { get; }

        [Fact]
        public async Task NoWeekendsOrHoliday_ReturnTotalCost()
        {
            this.holidayServiceMock.Setup(m => m.GetHolidaysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<IHoliday>());

            var monday = DateTimeHelpers.StartOfWeek(DateTime.Now, DayOfWeek.Monday);
            var request = new GetTotalCostQuery()
            {
                ApartmentId = this.apartment.Id,
                StartDate = monday,
                NumberOfNights = 2
            };
            double expectedCost = this.apartment.PricePerNight * request.NumberOfNights;

            double totalCost = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(expectedCost, totalCost, precision: 2);
        }

        [Fact]
        public async Task OnHolidays_IncreaseTotalCostByHolidayRatePerDayOfHoliday()
        {
            var tuesday = DateTimeHelpers.StartOfWeek(DateTime.Now, DayOfWeek.Tuesday);
            var wednesday = tuesday.AddDays(1);

            this.holidayServiceMock.Setup(m => m.GetHolidaysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<IHoliday>() {
                    new Holiday(){ Month=tuesday.Month, Day=tuesday.Day},
                    new Holiday(){ Month=wednesday.Month, Day=wednesday.Day}
                });

            var request = new GetTotalCostQuery()
            {
                ApartmentId = this.apartment.Id,
                StartDate = tuesday,
                NumberOfNights = 2
            };
            double perNight = this.apartment.PricePerNight;
            double expectedCost = (perNight + (HolidayRate * perNight)) * request.NumberOfNights;

            double totalCost = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(expectedCost, totalCost, precision: 2);
        }

        [Fact]
        public async Task OnWeekends_ReduceTotalCostByWeekendRatePerWeekDay()
        {
            this.holidayServiceMock.Setup(m => m.GetHolidaysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<IHoliday>());

            var startDate = DateTimeHelpers.StartOfWeek(DateTime.Now, DayOfWeek.Saturday);
            var request = new GetTotalCostQuery()
            {
                ApartmentId = this.apartment.Id,
                StartDate = startDate,
                NumberOfNights = 2
            };
            double perNight = this.apartment.PricePerNight;
            double expectedCost = (perNight - (WeekendRate * perNight)) * request.NumberOfNights;

            double totalCost = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(expectedCost, totalCost, precision: 2);
        }

        protected override void LoadTestData()
        {
            this.apartment = this.Context.Add(new Apartment() { PricePerNight = 12 }).Entity;
            this.Context.SaveChanges();
        }
    }
}