using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Hosts.Commands
{
    public class UpdateHostCommand : IRequest
    {
        public long Id { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; } = "";

        public string Gender { get; set; } = "";

        public string LastName { get; set; } = "";
    }

    public class UpdateHostCommandHandler : IRequestHandler<UpdateHostCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateHostCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateHostCommand request, CancellationToken cancellationToken)
        {
            var dbHost = await this.context.Hosts.Include(h => h.User)
                .SingleOrDefaultAsync(h => h.UserId == request.Id && !h.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (dbHost == null)
            {
                throw new NotFoundException($"User with id={request.Id} does not exist!");
            }

            CustomMap(request, dbHost);
            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void CustomMap(UpdateHostCommand src, Host dest)
        {
            dest.User.FirstName = src.FirstName ?? dest.User.FirstName;
            dest.User.LastName = src.LastName ?? dest.User.LastName;
            dest.User.Password = src.Password ?? dest.User.Password;
            dest.User.Gender = src.Gender ?? dest.User.Gender;
            dest.User.RoleName = RoleNames.Host;
            dest.User.IsDeleted = false;
            dest.IsDeleted = false;
        }
    }

    public class UpdateHostCommandValidator : AbstractValidator<UpdateHostCommand>
    {
        public UpdateHostCommandValidator()
        {
            this.RuleFor(u => u.Password).NotEmpty().MinimumLength(4);
        }
    }
}