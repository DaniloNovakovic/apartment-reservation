using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
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
            var dbApartment = await this.GetApartmentWithIncludedRelations(request);

            if (dbApartment is null || dbApartment.IsDeleted)
            {
                throw new NotFoundException("Requested apartment not found");
            }

            return new ApartmentDto(dbApartment)
            {
                Rating = await this.context.Comments.Where(c => !c.IsDeleted && c.ApartmentId == dbApartment.Id)
                    .DefaultIfEmpty()
                    .AverageAsync(c => (double)c.Rating).ConfigureAwait(false)
            };
        }

        private async Task<Apartment> GetApartmentWithIncludedRelations(GetApartmentQuery request)
        {
            return await this.context.Apartments
                .Include("ApartmentAmenities.Amenity")
                .Include(a => a.ForRentalDates)
                .Include(a => a.Images)
                .Include(a => a.Location)
                .ThenInclude(l => l.Address)
                .Include(a => a.Host)
                .ThenInclude(h => h.User)
                .SingleOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted)
                .ConfigureAwait(false);
        }
    }
}