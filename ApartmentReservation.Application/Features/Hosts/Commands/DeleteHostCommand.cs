using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Hosts.Commands
{
    public class DeleteHostCommand : IRequest
    {
        public long HostId { get; set; }
    }

    public class DeleteHostCommandHandler : IRequestHandler<DeleteHostCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMediator mediator;

        public DeleteHostCommandHandler(IApartmentReservationDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteHostCommand request, CancellationToken cancellationToken)
        {
            var host = await this.context.Hosts
                .Include(h => h.ApartmentsForRental)
                .Include(h => h.User)
                .SingleOrDefaultAsync(h => !h.IsDeleted && h.UserId == request.HostId, cancellationToken)
                .ConfigureAwait(false);

            if (host is null)
            {
                throw new NotFoundException($"Could not find user with id = {request.HostId}");
            }

            host.IsDeleted = true;

            await this.CascadeLogicalDeleteAsync(host, cancellationToken).ConfigureAwait(false);

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private async Task CascadeLogicalDeleteAsync(Domain.Entities.Host host, CancellationToken cancellationToken)
        {
            host.User.IsDeleted = true;

            foreach (long id in host.ApartmentsForRental.Where(a => !a.IsDeleted).Select(a => a.Id).ToList())
            {
                var command = new DeleteApartmentCommand() { ApartmentId = id };
                await this.mediator.Send(command, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}