using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Reservations.Queries
{
    public class GetAvailableDatesQuery : IRequest<IEnumerable<DateTime>>
    {
        public long ApartmentId { get; set; }
    }

    public class GetAvailableDatesQueryHandler : IRequestHandler<GetAvailableDatesQuery, IEnumerable<DateTime>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAvailableDatesQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DateTime>> Handle(GetAvailableDatesQuery request, CancellationToken cancellationToken)
        {
            long apartmentId = request.ApartmentId;

            var apartment = await context.Apartments
                .Include(a => a.ForRentalDates)
                .Include(a => a.Reservations)
                .SingleOrDefaultAsync(a => a.Id == apartmentId && !a.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (apartment is null)
            {
                throw new NotFoundException($"Apartment {apartmentId} not found");
            }

            var forRentalDates = apartment.ForRentalDates.Where(frd => !frd.IsDeleted).ToList();
            var reservations = apartment.Reservations.Where(r => !r.IsDeleted).ToList();

            var availableDates = new List<DateTime>();

            foreach (var forRentalDate in forRentalDates)
            {
                foreach (var reservation in reservations)
                {
                    var startDate = reservation.ReservationStartDate;
                    var endDate = startDate.AddDays(reservation.NumberOfNightsRented);
                }
            }

            return availableDates;
        }
    }
}