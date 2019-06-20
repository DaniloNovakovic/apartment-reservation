using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Reservations.Queries
{
    public class GetTotalCostQuery : IRequest<double>
    {
        public long ApartmentId { get; set; }

        public DateTime StartDate { get; set; }

        public int NumberOfNights { get; set; }
    }

    public class GetTotalCostQueryHandler : IRequestHandler<GetTotalCostQuery, double>
    {
        private readonly IApartmentReservationDbContext context;

        public GetTotalCostQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<double> Handle(GetTotalCostQuery request, CancellationToken cancellationToken)
        {
            var apartment = await context.Apartments
                .SingleOrDefaultAsync(a => a.Id == request.ApartmentId && !a.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (apartment is null)
            {
                throw new NotFoundException($"Apartment {request.ApartmentId} could not be found!");
            }

            return apartment.PricePerNight * request.NumberOfNights;
        }
    }
}