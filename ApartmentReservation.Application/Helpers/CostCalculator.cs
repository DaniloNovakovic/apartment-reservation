using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Application.Helpers
{
    public class CostCalculator : ICostCalculator
    {
        public const double HolidayRate = 0.05;
        public const double WeekendRate = 0.1;

        private readonly IApartmentReservationDbContext context;
        private readonly IHolidayService holidayService;

        public CostCalculator(IApartmentReservationDbContext context, IHolidayService holidayService)
        {
            this.context = context;
            this.holidayService = holidayService;
        }

        public async Task<double> CalculateTotalCostAsync(GetTotalCostArgs request, CancellationToken cancellationToken = default)
        {
            var apartment = await context.Apartments
                .SingleOrDefaultAsync(a => a.Id == request.ApartmentId && !a.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (apartment is null)
            {
                throw new NotFoundException($"Apartment {request.ApartmentId} could not be found!");
            }

            var holidays = await holidayService.GetHolidaysAsync(cancellationToken).ConfigureAwait(false);

            double totalCost = 0;

            for (int nightCount = 0; nightCount < request.NumberOfNights; ++nightCount)
            {
                var currDate = request.StartDate.AddDays(nightCount);
                double currPrice = apartment.PricePerNight;

                if (DateTimeHelpers.IsWeekend(currDate))
                {
                    currPrice -= apartment.PricePerNight * WeekendRate;
                }

                if (holidays.Any(h => h.Day == currDate.Day && h.Month == currDate.Month))
                {
                    currPrice += apartment.PricePerNight * HolidayRate;
                }

                totalCost += currPrice;
            }

            return totalCost;
        }
    }
}
