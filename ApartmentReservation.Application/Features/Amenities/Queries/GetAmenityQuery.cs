using System;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ApartmentReservation.Application.Features.Amenities.Queries
{
    public class GetAmenityQuery : IRequest<AmenityDto>
    {
        public long Id { get; set; }
    }

    public class GetAmenityQueryHandler : IRequestHandler<GetAmenityQuery, AmenityDto>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public GetAmenityQueryHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<AmenityDto> Handle(GetAmenityQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}