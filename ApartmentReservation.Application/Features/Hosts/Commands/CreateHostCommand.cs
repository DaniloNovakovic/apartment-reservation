using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Infrastructure.Authentication;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Hosts.Commands
{
    public class CreateHostCommand : HostDto, IRequest
    {
    }

    public class CreateHostCommandHandler : IRequestHandler<CreateHostCommand>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public CreateHostCommandHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateHostCommand request, CancellationToken cancellationToken)
        {
            var dbHost = await this.context.Hosts.Include(h => h.User)
                .SingleOrDefaultAsync(h => h.User.Username == request.Username, cancellationToken)
                .ConfigureAwait(false);

            if (dbHost == null)
            {
                await this.AddNewHostAsync(request, cancellationToken).ConfigureAwait(false);
            }
            else if (dbHost.IsDeleted || dbHost.User.IsDeleted)
            {
                CustomMap(request, dbHost);
            }
            else
            {
                throw new AlreadyCreatedException($"User '{dbHost.UserId}' already exists!");
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        private static void CustomMap(CreateHostCommand src, Host dest)
        {
            dest.User.FirstName = src.FirstName ?? dest.User.FirstName;
            dest.User.LastName = src.LastName ?? dest.User.LastName;
            dest.User.Password = src.Password ?? dest.User.Password;
            dest.User.Gender = src.Gender ?? dest.User.Gender;
            dest.User.RoleName = RoleNames.Host;
            dest.User.IsDeleted = false;
            dest.IsDeleted = false;
        }

        private async Task AddNewHostAsync(CreateHostCommand request, CancellationToken cancellationToken)
        {
            var hostToAdd = new Host()
            {
                User = this.mapper.Map<User>(request)
            };

            hostToAdd.User.IsDeleted = false;
            hostToAdd.IsDeleted = false;
            hostToAdd.User.RoleName = RoleNames.Host;

            await this.context.Hosts.AddAsync(hostToAdd, cancellationToken).ConfigureAwait(false);
        }
    }
}