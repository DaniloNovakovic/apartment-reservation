using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Hosts.Commands
{
    public class CreateHostCommand : HostDto, IRequest<HostDto>
    {
    }

    public class CreateHostCommandHandler : IRequestHandler<CreateHostCommand, HostDto>
    {
        private readonly IApartmentReservationDbContext context;

        public CreateHostCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<HostDto> Handle(CreateHostCommand request, CancellationToken cancellationToken)
        {
            var dbHost = await this.context.Hosts.Include(h => h.User)
                .SingleOrDefaultAsync(h => h.User.Username == request.Username, cancellationToken)
                .ConfigureAwait(false);

            if (dbHost == null)
            {
                dbHost = await this.AddNewHostAsync(request, cancellationToken).ConfigureAwait(false);
            }
            else if (dbHost.IsDeleted || dbHost.User.IsDeleted)
            {
                CustomMap(request, dbHost);
            }
            else
            {
                throw new AlreadyCreatedException($"User '{dbHost.User.Username}' already exists!");
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new HostDto(dbHost);
        }

        private static void CustomMap(CreateHostCommand src, Host dest)
        {
            CustomMapper.Map(src, dest, RoleNames.Host, isDeleted: false);
        }

        private async Task<Host> AddNewHostAsync(CreateHostCommand request, CancellationToken cancellationToken)
        {
            var hostToAdd = new Host()
            {
                User = new User()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender,
                    RoleName = RoleNames.Host,
                    Username = request.Username,
                    Password = request.Password,
                    IsDeleted = false
                },
                IsDeleted = false
            };

            var addedHost = await this.context.Hosts.AddAsync(hostToAdd, cancellationToken).ConfigureAwait(false);
            return addedHost.Entity;
        }
    }
}