using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using MediatR;

namespace ApartmentReservation.Application.Features.Guests.Queries
{
    public class GetAllGuestsQuery : IRequest<IEnumerable<GuestDto>>
    {
    }

    public class GetAllGuestsQueryHandler : IRequestHandler<GetAllGuestsQuery, IEnumerable<GuestDto>>
    {
        public Task<IEnumerable<GuestDto>> Handle(GetAllGuestsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}