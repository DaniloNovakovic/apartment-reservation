using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public long Id { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string Gender { get; set; }

        public string LastName { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateUserCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await this.context.Users
                .SingleOrDefaultAsync(u => u.Id == request.Id && !u.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbUser is null)
            {
                throw new NotFoundException($"User with id={request.Id} could not be found!");
            }

            CustomMap(request, dbUser);

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void CustomMap(UpdateUserCommand src, User dest)
        {
            dest.FirstName = src.FirstName ?? dest.FirstName;
            dest.LastName = src.LastName ?? dest.LastName;
            dest.Password = src.Password ?? dest.Password;
            dest.Gender = src.Gender ?? dest.Gender;
            dest.IsDeleted = false;
        }
    }
}