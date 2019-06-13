using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Queries
{
    public class GetAllApartmentsQuery : IRequest<IEnumerable<ApartmentDto>>
    {
    }

    public class GetAllApartmentsQueryHandler : IRequestHandler<GetAllApartmentsQuery, IEnumerable<ApartmentDto>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAllApartmentsQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ApartmentDto>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Apartments.Where(a => !a.IsDeleted);

            query = ApplyFilters(request, query);

            var apartments = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            return apartments.Select(a => new ApartmentDto(a));
        }

        private static IQueryable<Apartment> ApplyFilters(GetAllApartmentsQuery filters, IQueryable<Apartment> query)
        {
            return query;
        }
    }
}