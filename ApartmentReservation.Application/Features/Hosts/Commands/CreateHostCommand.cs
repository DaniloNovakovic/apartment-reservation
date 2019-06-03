using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using AutoMapper;
using MediatR;

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
            var hostToAdd = new Host()
            {
                User = mapper.Map<User>(request)
            };

            await this.context.Hosts.AddAsync(hostToAdd, cancellationToken).ConfigureAwait(false);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}