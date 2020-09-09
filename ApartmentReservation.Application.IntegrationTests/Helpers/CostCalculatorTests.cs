using ApartmentReservation.Application.Helpers;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using ApartmentReservation.Common.Interfaces;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApartmentReservation.Application.IntegrationTests.Helpers
{
    public class CostCalculatorTests : InMemoryContextTestBase
    {
        private readonly Mock<IHolidayService> holidayServiceMock;
        private readonly CostCalculator sut;
        private Apartment apartment;

        public CostCalculatorTests()
        {
            holidayServiceMock = new Mock<IHolidayService>();
            sut = new CostCalculator(Context, holidayServiceMock.Object);
            HolidayRate = CostCalculator.HolidayRate;
            WeekendRate = CostCalculator.WeekendRate;
        }

        public double HolidayRate { get; }
        public double WeekendRate { get; }

        [Fact]
        public async Task NoWeekendsOrHoliday_ReturnTotalCost()
        {
            holidayServiceMock.Setup(m => m.GetHolidaysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<IHoliday>());

            var monday = DateTimeHelpers.StartOfWeek(DateTime.Now, DayOfWeek.Monday);
            var request = new GetTotalCostArgs()
            {
                ApartmentId = apartment.Id,
                StartDate = monday,
                NumberOfNights = 2
            };
            double expectedCost = apartment.PricePerNight * request.NumberOfNights;

            double totalCost = await sut.CalculateTotalCostAsync(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(expectedCost, totalCost, precision: 2);
        }

        [Fact]
        public async Task OnHolidays_IncreaseTotalCostByHolidayRatePerDayOfHoliday()
        {
            var tuesday = DateTimeHelpers.StartOfWeek(DateTime.Now, DayOfWeek.Tuesday);
            var wednesday = tuesday.AddDays(1);

            holidayServiceMock.Setup(m => m.GetHolidaysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<IHoliday>() {
                    new Holiday(){ Month=tuesday.Month, Day=tuesday.Day},
                    new Holiday(){ Month=wednesday.Month, Day=wednesday.Day}
                });

            var request = new GetTotalCostArgs()
            {
                ApartmentId = apartment.Id,
                StartDate = tuesday,
                NumberOfNights = 2
            };
            double perNight = apartment.PricePerNight;
            double expectedCost = (perNight + HolidayRate * perNight) * request.NumberOfNights;

            double totalCost = await sut.CalculateTotalCostAsync(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(expectedCost, totalCost, precision: 2);
        }

        [Fact]
        public async Task OnWeekends_ReduceTotalCostByWeekendRatePerWeekDay()
        {
            holidayServiceMock.Setup(m => m.GetHolidaysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<IHoliday>());

            var startDate = DateTimeHelpers.StartOfWeek(DateTime.Now, DayOfWeek.Saturday);
            var request = new GetTotalCostArgs()
            {
                ApartmentId = apartment.Id,
                StartDate = startDate,
                NumberOfNights = 2
            };
            double perNight = apartment.PricePerNight;
            double expectedCost = (perNight - WeekendRate * perNight) * request.NumberOfNights;

            double totalCost = await sut.CalculateTotalCostAsync(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(expectedCost, totalCost, precision: 2);
        }

        protected override void LoadTestData()
        {
            apartment = Context.Add(new Apartment() { PricePerNight = 12 }).Entity;
            Context.SaveChanges();
        }
    }
}