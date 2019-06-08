using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ApartmentReservation.Application.Features.Guests.Commands
{
    public class UpdateGuestCommand : IRequest
    {
    }

    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand>
    {
        public Task<Unit> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}