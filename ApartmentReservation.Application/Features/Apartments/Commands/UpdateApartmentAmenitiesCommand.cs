using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class UpdateApartmentAmenitiesCommand : IRequest
    {
        public UpdateApartmentAmenitiesCommand()
        {
            this.Amenities = new List<AmenityDto>();
        }

        public IEnumerable<AmenityDto> Amenities { get; set; }
        public long ApartmentId { get; set; }
    }

    public class UpdateApartmentAmenitiesCommandHandler : IRequestHandler<UpdateApartmentAmenitiesCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateApartmentAmenitiesCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateApartmentAmenitiesCommand request, CancellationToken cancellationToken)
        {
            var dbApartment = await this.context.Apartments
                .Include(a => a.ApartmentAmenities)
                .SingleOrDefaultAsync(a => a.Id == request.ApartmentId && !a.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbApartment is null)
            {
                throw new NotFoundException($"Apartment with id={request.ApartmentId} not found!");
            }

            UpdateApartmentAmenities(request, dbApartment);

            dbApartment.IsSyncNeeded = true;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void UpdateApartmentAmenities(UpdateApartmentAmenitiesCommand request, Apartment dbApartment)
        {
            var dict = dbApartment.ApartmentAmenities.ToDictionary(keySelector: a => a.AmenityId, elementSelector: a => new HelperClass(a));

            foreach (var item in request.Amenities)
            {
                if (!item.Id.HasValue)
                    continue;
                long currId = item.Id.Value;

                if (!dict.ContainsKey(currId))
                {
                    dbApartment.ApartmentAmenities.Add(new ApartmentAmenity()
                    {
                        AmenityId = item.Id.Value,
                        ApartmentId = dbApartment.Id
                    });
                }
                else
                {
                    dict[currId].ApartmentAmenity.IsDeleted = false;
                    dict[currId].IsFound = true;
                }
            }

            foreach (var item in dict.Values.Where(x => !x.IsFound))
            {
                item.ApartmentAmenity.IsDeleted = true;
            }
        }

        private class HelperClass
        {
            public ApartmentAmenity ApartmentAmenity { get; set; }
            public bool IsFound { get; set; }

            public HelperClass(ApartmentAmenity aa)
            {
                this.ApartmentAmenity = aa;
                this.IsFound = false;
            }
        }
    }
}