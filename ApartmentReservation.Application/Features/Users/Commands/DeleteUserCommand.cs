using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
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

        public DeleteUserCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
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

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}