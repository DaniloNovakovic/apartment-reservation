using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetAllHostsQuery : IRequest<IEnumerable<HostDto>>
    {
    }

    public class GetAllHostsQueryHandler : IRequestHandler<GetAllHostsQuery, IEnumerable<HostDto>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAllHostsQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<HostDto>> Handle(GetAllHostsQuery request, CancellationToken cancellationToken)
        {
            var query = await this.context.Hosts.Where(h => !h.IsDeleted).ToListAsync(cancellationToken).ConfigureAwait(false);
            return query.Select(h => new HostDto(h));
        }
    }
}