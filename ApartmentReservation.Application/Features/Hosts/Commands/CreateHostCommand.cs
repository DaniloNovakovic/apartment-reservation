using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using AutoMapper;
using MediatR;

namespace ApartmentReservation.Application.Features.Hosts.Commands
{
    public class CreateHostCommand : UserDto, IRequest
    {
        public class Handler : IRequestHandler<CreateHostCommand>
        {
            private readonly IApartmentReservationDbContext context;

            public Handler(IApartmentReservationDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(CreateHostCommand request, CancellationToken cancellationToken)
            {
                return Unit.Value;
            }
        }
    }
}