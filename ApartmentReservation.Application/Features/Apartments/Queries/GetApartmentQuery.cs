using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace ApartmentReservation.Application.Features.Apartments.Queries
{
    public class GetApartmentQuery : IRequest<ApartmentDto>
    {
        public long Id { get; set; }
    }

    public class GetApartmentQueryHandler : IRequestHandler<GetApartmentQuery, ApartmentDto>
    {
        private readonly IQueryDbContext context;

        public GetApartmentQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<ApartmentDto> Handle(GetApartmentQuery request, CancellationToken cancellationToken)
        {
            var apartment = await context.Apartments.Find(a => a.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            return CustomMapper.Map(apartment);
        }
    }
}