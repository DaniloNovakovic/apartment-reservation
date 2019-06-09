using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Amenities.Commands
{
    public class DeleteAmenityCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteAmenityCommandHandler : IRequestHandler<DeleteAmenityCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public DeleteAmenityCommandHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
        {
            var dbAmenity = await this.context.Amenities
                .SingleOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbAmenity is null)
            {
                throw new NotFoundException();
            }

            dbAmenity.IsDeleted = true;
            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}