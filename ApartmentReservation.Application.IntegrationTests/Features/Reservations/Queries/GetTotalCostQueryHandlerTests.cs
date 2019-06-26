using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using ApartmentReservation.Domain.Entities;
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
            this.sut = new GetTotalCostQueryHandler(this.Context, holidayServiceMock.Object);
        }

        [Fact]
        public async Task NoWeekendsOrHoliday_ReturnTotalCost()
        {
            holidayServiceMock.Setup(m => m.GetHolidaysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<IHoliday>());

            var monday = DateTimeHelpers.StartOfWeek(DateTime.Now, DayOfWeek.Monday);
            var request = new GetTotalCostQuery()
            {
                ApartmentId = this.apartment.Id,
                StartDate = monday,
                NumberOfNights = 2
            };
            double expectedCost = apartment.PricePerNight * request.NumberOfNights;

            double totalCost = await this.sut.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.Equal(expectedCost, totalCost);
        }

        protected override void LoadTestData()
        {
            this.apartment = this.Context.Add(new Apartment() { PricePerNight = 12 }).Entity;
            this.Context.SaveChanges();
        }
    }
}