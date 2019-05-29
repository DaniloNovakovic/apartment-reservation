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
            private readonly IUnitOfWork unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateHostCommand request, CancellationToken cancellationToken)
            {
                var host = Mapper.Map<Host>(request);

                await unitOfWork.Hosts.AddAsync(host, cancellationToken).ConfigureAwait(false);

                await unitOfWork.CompleteAsync(cancellationToken).ConfigureAwait(false);

                return Unit.Value;
            }
        }
    }
}