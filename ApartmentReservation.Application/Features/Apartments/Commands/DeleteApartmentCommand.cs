using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class DeleteApartmentCommand : IRequest
    {
        public long ApartmentId { get; set; }
    }

    public class DeleteApartmentCommandHandler : IRequestHandler<DeleteApartmentCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public DeleteApartmentCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            var apartment = await this.context.Apartments
                .Include(a => a.Images)
                .Include(a => a.Reservations)
                .Include(a => a.Comments)
                .Include(a => a.ApartmentAmenities)
                .Include(a => a.ForRentalDates)
                .SingleOrDefaultAsync(a => !a.IsDeleted && a.Id == request.ApartmentId, cancellationToken).ConfigureAwait(false);

            if (apartment is null)
            {
                throw new NotFoundException($"Could not find apartment with id `{request.ApartmentId}`. Possible reason: already deleted.");
            }

            apartment.IsDeleted = true;
            CascadeLogicalDelete(apartment);

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void CascadeLogicalDelete(Domain.Entities.Apartment apartment)
        {
            foreach (var item in apartment.Images)
            {
                item.IsDeleted = true;
            }
            foreach (var item in apartment.Reservations)
            {
                item.IsDeleted = true;
            }
            foreach (var item in apartment.Comments)
            {
                item.IsDeleted = true;
            }
            foreach (var item in apartment.ApartmentAmenities)
            {
                item.IsDeleted = true;
            }
            foreach (var item in apartment.ForRentalDates)
            {
                item.IsDeleted = true;
            }
        }
    }
}