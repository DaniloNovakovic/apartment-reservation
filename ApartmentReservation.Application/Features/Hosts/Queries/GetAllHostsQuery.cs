using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using MediatR;
using MongoDB.Driver;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetAllHostsQuery : IRequest<IEnumerable<HostDto>>
    {
    }

    public class GetAllHostsQueryHandler : IRequestHandler<GetAllHostsQuery, IEnumerable<HostDto>>
    {
        private readonly IQueryDbContext context;

        public GetAllHostsQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<HostDto>> Handle(GetAllHostsQuery request, CancellationToken cancellationToken)
        {
            var query = await this.context.Users.Find(u => u.RoleName == RoleNames.Host).ToListAsync(cancellationToken).ConfigureAwait(false);
            return query.Select(h => CustomMapper.Map<HostDto>(h));
        }
    }
}