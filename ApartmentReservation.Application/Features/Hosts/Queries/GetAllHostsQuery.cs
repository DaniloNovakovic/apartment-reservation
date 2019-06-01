using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetAllHostsQuery : IRequest<IEnumerable<HostDto>>
    {
        public class GetAllHostsQueryHandler : IRequestHandler<GetAllHostsQuery, IEnumerable<HostDto>>
        {
            private readonly IApartmentReservationDbContext context;

            public GetAllHostsQueryHandler(IApartmentReservationDbContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<HostDto>> Handle(GetAllHostsQuery request, CancellationToken cancellationToken)
            {
                return new List<HostDto>();
            }
        }
    }
}