using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Users.Commands
{
    public class UnbanUserCommand : IRequest
    {
        public long UserId { get; set; }
    }

    public class UnbanUserCommandHandler : IRequestHandler<UnbanUserCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UnbanUserCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(u => u.Id == request.UserId && !u.IsDeleted).ConfigureAwait(false);

            if (user is null)
            {
                throw new NotFoundException($"Could not find user with id `{user.Id}`");
            }

            user.IsBanned = false;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}