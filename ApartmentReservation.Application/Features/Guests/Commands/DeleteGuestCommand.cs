using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Guests.Commands
{
    public class DeleteGuestCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public DeleteGuestCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            var guestToDelete = await this.context.Guests
                .SingleOrDefaultAsync(g => g.UserId == request.Id && !g.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (guestToDelete is null)
            {
                throw new NotFoundException($"Couldn't find resource with Id={request.Id}. Possible reasons: It is already deleted.");
            }

            guestToDelete.IsDeleted = true;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}