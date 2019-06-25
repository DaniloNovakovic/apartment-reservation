using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Guests.Commands
{
    public class DeleteGuestCommand : IRequest
    {
        public long GuestId { get; set; }
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
            var guest = await this.context.Guests
                .Include(g => g.User)
                .Include(g => g.Reservations)
                .SingleOrDefaultAsync(g => !g.IsDeleted && g.UserId == request.GuestId, cancellationToken)
                .ConfigureAwait(false);

            guest.IsDeleted = true;

            CascadeLogicalDelete(guest);

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void CascadeLogicalDelete(Domain.Entities.Guest guest)
        {
            guest.User.IsDeleted = true;

            foreach (var item in guest.Reservations)
            {
                item.IsDeleted = true;
            }
        }
    }
}