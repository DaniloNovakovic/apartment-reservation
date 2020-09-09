using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Amenities.Commands
{
    public class CreateAmenityCommand : IRequest
    {
        public string Name { get; set; }
    }

    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public CreateAmenityCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            var dbAmenity = await this.context.Amenities
                .SingleOrDefaultAsync(a => a.Name == request.Name, cancellationToken)
                .ConfigureAwait(false);

            if (dbAmenity is null)
            {
                this.context.Amenities.Add(new Domain.Entities.Amenity() { Name = request.Name });
            }
            else if (dbAmenity.IsDeleted)
            {
                dbAmenity.IsDeleted = false;
                dbAmenity.Name = request.Name;
            }
            else
            {
                throw new AlreadyCreatedException();
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}