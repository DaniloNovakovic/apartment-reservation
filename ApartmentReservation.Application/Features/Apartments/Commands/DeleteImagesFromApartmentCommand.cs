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
    public class DeleteImagesFromApartmentCommand : IRequest
    {
        public DeleteImagesFromApartmentCommand()
        {
            this.Images = new List<ImageDto>();
        }

        public IEnumerable<ImageDto> Images { get; set; }
        public long ApartmentId { get; set; }
    }

    public class DeleteImagesFromApartmentCommandHandler : IRequestHandler<DeleteImagesFromApartmentCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public DeleteImagesFromApartmentCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteImagesFromApartmentCommand request, CancellationToken cancellationToken)
        {
            var apartment = await context.Apartments.SingleOrDefaultAsync(a => a.Id == request.ApartmentId, cancellationToken);

            if (apartment == null)
            {
                throw new NotFoundException($"Apartment with id {request.ApartmentId} not found!");
            }

            var images = await this.context.Images
                .Where(i => i.ApartmentId == request.ApartmentId && !i.IsDeleted)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            foreach (var img in images)
            {
                if (request.Images.Any(i => i.Id == img.Id))
                {
                    img.IsDeleted = true;

                    apartment.IsSyncNeeded = true;
                }
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}