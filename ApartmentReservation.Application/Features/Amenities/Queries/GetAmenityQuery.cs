﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        public async Task<AmenityDto> Handle(GetAmenityQuery request, CancellationToken cancellationToken)
        {
            var amenity = await context.Amenities
                .SingleOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted)
                .ConfigureAwait(false);

            if (amenity is null)
            {
                throw new NotFoundException($"Failed to find amenity with id '{request.Id}'");
            }

            return mapper.Map<AmenityDto>(amenity);
        }
    }
}