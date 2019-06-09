using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Amenities.Commands
{
    public class UpdateAmenityCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public UpdateAmenityCommandHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            var dbAmenity = await this.context.Amenities
                .SingleOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted)
                .ConfigureAwait(false);

            if (dbAmenity is null)
            {
                throw new NotFoundException();
            }

            dbAmenity.IsDeleted = false;
            dbAmenity.Name = request.Name;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}