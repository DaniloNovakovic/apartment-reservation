using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Hosts.Commands
{
    public class DeleteHostCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteHostCommandHandler : IRequestHandler<DeleteHostCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public DeleteHostCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteHostCommand request, CancellationToken cancellationToken)
        {
            var hostToDelete = await this.context.Hosts
                .SingleOrDefaultAsync(h => h.UserId == request.Id && !h.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (hostToDelete != null)
            {
                hostToDelete.IsDeleted = true;
                await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                throw new NotFoundException($"Couldn't find resource with Id={request.Id}. Possible reasons: It is already deleted.");
            }

            return Unit.Value;
        }
    }
}