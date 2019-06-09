using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ApartmentReservation.Application.Features.Amenities.Queries
{
    public class GetAllAmenitiesQuery : IRequest<IEnumerable<AmenityDto>>
    {
    }

    public class GetAllAmenitiesQueryHandler : IRequestHandler<GetAllAmenitiesQuery, IEnumerable<AmenityDto>>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public GetAllAmenitiesQueryHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<IEnumerable<AmenityDto>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}