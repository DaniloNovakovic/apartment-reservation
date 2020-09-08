using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using MediatR;
using MongoDB.Driver;

namespace ApartmentReservation.Application.Features.Guests.Queries
{
    public class GetAllGuestsQuery : IRequest<IEnumerable<GuestDto>>
    {
    }

    public class GetAllGuestsQueryHandler : IRequestHandler<GetAllGuestsQuery, IEnumerable<GuestDto>>
    {
        private readonly IQueryDbContext context;

        public GetAllGuestsQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GuestDto>> Handle(GetAllGuestsQuery request, CancellationToken cancellationToken)
        {
            var query = await this.context.Users.Find(u => u.RoleName == RoleNames.Guest).ToListAsync(cancellationToken).ConfigureAwait(false);
            return query.Select(h => CustomMapper.Map<GuestDto>(h));
        }
    }
}