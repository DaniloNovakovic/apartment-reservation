using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ApartmentReservation.Application.Features.Guests.Commands
{
    public class DeleteGuestCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand>
    {
        public Task<Unit> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}