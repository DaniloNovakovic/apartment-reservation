using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Amenities.Queries
{
    public class GetAmenityQuery : IRequest<AmenityDto>
    {
        public long Id { get; set; }
    }

    public class GetAmenityQueryHandler : IRequestHandler<GetAmenityQuery, AmenityDto>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAmenityQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<AmenityDto> Handle(GetAmenityQuery request, CancellationToken cancellationToken)
        {
            var amenity = await this.context.Amenities
                .SingleOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted)
                .ConfigureAwait(false);

            if (amenity is null)
            {
                throw new NotFoundException($"Failed to find amenity with id '{request.Id}'");
            }

            return new AmenityDto(amenity);
        }
    }
}