using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Users.Commands
{
    public class BanUserCommand : IRequest
    {
        public long UserId { get; set; }
    }

    public class BanUserCommandHandler : IRequestHandler<BanUserCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public BanUserCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(u => u.Id == request.UserId && !u.IsDeleted).ConfigureAwait(false);

            if (user is null)
            {
                throw new NotFoundException($"Could not find user with id `{user.Id}`");
            }

            if (user.RoleName == RoleNames.Administrator)
            {
                throw new UnauthorizedException($"Denied: User `{user.Id}` is administrator, therefore he cannot be banned!");
            }

            user.IsBanned = true;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}