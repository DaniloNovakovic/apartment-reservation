using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Guests.Commands;
using ApartmentReservation.Application.Features.Hosts.Commands;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMediator mediator;

        public DeleteUserCommandHandler(IApartmentReservationDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await this.context.Users
                .SingleOrDefaultAsync(u => u.Id == request.Id && !u.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbUser is null)
            {
                throw new NotFoundException($"Couldn't find resource with Id={request.Id}. Possible reasons: It is already deleted.");
            }

            dbUser.IsDeleted = true;

            if (dbUser.RoleName == RoleNames.Guest)
            {
                await this.mediator.Send(new DeleteGuestCommand() { GuestId = dbUser.Id }).ConfigureAwait(false);
            }
            else if (dbUser.RoleName == RoleNames.Host)
            {
                await this.mediator.Send(new DeleteHostCommand() { HostId = dbUser.Id }).ConfigureAwait(false);
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}