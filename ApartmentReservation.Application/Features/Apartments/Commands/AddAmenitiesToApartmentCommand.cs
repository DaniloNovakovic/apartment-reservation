using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class AddAmenitiesToApartmentCommand : IRequest
    {
        public AddAmenitiesToApartmentCommand()
        {
            this.Amenities = new List<AmenityDto>();
        }

        public long ApartmentId { get; set; }
        public IEnumerable<AmenityDto> Amenities { get; set; }
    }

    public class AddAmenitiesToApartmentCommandHandler : IRequestHandler<AddAmenitiesToApartmentCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public AddAmenitiesToApartmentCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(AddAmenitiesToApartmentCommand request, CancellationToken cancellationToken)
        {
            var dbApartment = await this.context.Apartments
                .Include(a => a.ApartmentAmenities)
                .SingleOrDefaultAsync(a => a.Id == request.ApartmentId && !a.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbApartment is null)
            {
                throw new NotFoundException($"Apartment with id={request.ApartmentId} not found!");
            }

            var dict = dbApartment.ApartmentAmenities.ToDictionary(keySelector: a => a.AmenityId);

            foreach (var item in request.Amenities)
            {
                if (item.Id.HasValue && !dict.ContainsKey(item.Id.Value))
                {
                    dbApartment.ApartmentAmenities.Add(new Domain.Entities.ApartmentAmenity()
                    {
                        AmenityId = item.Id.Value,
                        ApartmentId = dbApartment.Id
                    });

                    dbApartment.IsSyncNeeded = true;
                }
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}