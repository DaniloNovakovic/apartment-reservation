using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using MediatR;

namespace ApartmentReservation.Application.Features.Apartments.Queries
{
    public class GetAllApartmentsQuery : IRequest<IEnumerable<ApartmentDto>>
    {
    }

    public class GetAllApartmentsQueryHandler : IRequestHandler<GetAllApartmentsQuery, IEnumerable<ApartmentDto>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAllApartmentsQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ApartmentDto>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(50);
            return new List<ApartmentDto>();
        }
    }
}