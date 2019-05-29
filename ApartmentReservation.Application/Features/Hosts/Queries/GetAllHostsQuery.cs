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
            private readonly IUnitOfWork unitOfWork;

            public GetAllHostsQueryHandler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<HostDto>> Handle(GetAllHostsQuery request, CancellationToken cancellationToken)
            {
                var hosts = await this.unitOfWork.Hosts.GetAllAsync(cancellationToken).ConfigureAwait(false);
                return hosts.Select(host => Mapper.Map<HostDto>(host));
            }
        }
    }
}