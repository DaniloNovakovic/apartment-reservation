using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Queries
{
    public class GetApartmentQuery : IRequest<ApartmentDto>
    {
        public long Id { get; set; }
    }

    public class GetApartmentQueryHandler : IRequestHandler<GetApartmentQuery, ApartmentDto>
    {
        private readonly IApartmentReservationDbContext context;

        public GetApartmentQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<ApartmentDto> Handle(GetApartmentQuery request, CancellationToken cancellationToken)
        {
            var dbApartment = await GetApartmentWithIncludedRelations(request);

            if (dbApartment is null || dbApartment.IsDeleted)
            {
                throw new NotFoundException("Requested apartment not found");
            }

            return new ApartmentDto(dbApartment);
        }

        private async Task<Apartment> GetApartmentWithIncludedRelations(GetApartmentQuery request)
        {
            return await context.Apartments
                .Include("ApartmentAmenities.Amenity")
                .Include(a => a.ForRentalDates)
                .Include(a => a.Images)
                .Include(a => a.Location)
                .ThenInclude(l => l.Address)
                .Include(a => a.Host)
                .ThenInclude(h => h.User)
                .Include(a => a.Comments)
                .SingleOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted)
                .ConfigureAwait(false);
        }
    }
}