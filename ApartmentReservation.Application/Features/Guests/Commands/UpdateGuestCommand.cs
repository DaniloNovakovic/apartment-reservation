using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Guests.Commands
{
    public class UpdateGuestCommand : IRequest
    {
        public long Id { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; } = "";

        public string Gender { get; set; } = "";

        public string LastName { get; set; } = "";
    }

    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateGuestCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var dbGuest = await this.context.Guests
                .SingleOrDefaultAsync(g => g.UserId == request.Id && !g.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbGuest is null)
            {
                throw new NotFoundException($"User with id={request.Id} does not exist!");
            }

            CustomMap(request, dbGuest);

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void CustomMap(UpdateGuestCommand src, Guest dest)
        {
            dest.User.FirstName = src.FirstName ?? dest.User.FirstName;
            dest.User.LastName = src.LastName ?? dest.User.LastName;
            dest.User.Password = src.Password ?? dest.User.Password;
            dest.User.Gender = src.Gender ?? dest.User.Gender;
            dest.User.RoleName = RoleNames.Guest;
            dest.User.IsDeleted = false;
            dest.IsDeleted = false;
        }
    }
}