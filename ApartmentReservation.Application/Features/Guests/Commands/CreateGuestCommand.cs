using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Guests.Commands
{
    public class CreateGuestCommand : UserDto, IRequest<GuestDto>
    {
    }

    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, GuestDto>
    {
        private readonly IApartmentReservationDbContext context;

        public CreateGuestCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<GuestDto> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var dbGuest = await this.context.Guests.Include(g => g.User)
                .SingleOrDefaultAsync(g => g.User.Username == request.Username).ConfigureAwait(false);

            if (dbGuest == null)
            {
                dbGuest = await this.AddNewGuestAsync(request, cancellationToken).ConfigureAwait(false);
            }
            else if (dbGuest.IsDeleted || dbGuest.User.IsDeleted)
            {
                CustomMap(request, dbGuest);
            }
            else
            {
                throw new AlreadyCreatedException($"User '{dbGuest.User.Username}' already exists!");
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new GuestDto(dbGuest);
        }

        private static void CustomMap(CreateGuestCommand src, Guest dest)
        {
            CustomMapper.Map(src, dest, RoleNames.Guest, isDeleted: false);
        }

        private async Task<Guest> AddNewGuestAsync(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestToAdd = new Guest()
            {
                User = new User()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender,
                    RoleName = RoleNames.Guest,
                    Username = request.Username,
                    Password = request.Password,
                    IsDeleted = false
                },
                IsDeleted = false
            };

            var addedGuest = await this.context.Guests.AddAsync(guestToAdd, cancellationToken).ConfigureAwait(false);
            return addedGuest.Entity;
        }
    }
}