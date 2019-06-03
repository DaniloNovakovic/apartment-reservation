using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetAllHostsQuery : IRequest<IEnumerable<HostDto>>
    {
        public class GetAllHostsQueryHandler : IRequestHandler<GetAllHostsQuery, IEnumerable<HostDto>>
        {
            private readonly IApartmentReservationDbContext context;
            private readonly IMapper mapper;

            public GetAllHostsQueryHandler(IApartmentReservationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<IEnumerable<HostDto>> Handle(GetAllHostsQuery request, CancellationToken cancellationToken)
            {
                var query = await context.Hosts.Where(h => !h.IsDeleted).ToListAsync(cancellationToken).ConfigureAwait(false);
                return query.Select(mapper.Map<Host, HostDto>);
            }
        }
    }
}