using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using MediatR;

namespace ApartmentReservation.Application.Features.Guests.Queries
{
    public class GetGuestQuery : IRequest<GuestDto>
    {
        public long Id { get; set; }
    }

    public class GetGuestQueryHandler : IRequestHandler<GetGuestQuery, GuestDto>
    {
        public Task<GuestDto> Handle(GetGuestQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}