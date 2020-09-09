using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Application.Features.Reservations.Queries.Legacy
{
    /// <summary>
    /// (Obfuscated) Left behind for legacy reasons - preffer using Domain.Apartment.GetAvailableDates()
    /// </summary>
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

            return apartment.GetAvailableDates();
        }
    }
}