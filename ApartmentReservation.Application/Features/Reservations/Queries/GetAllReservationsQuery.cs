using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Read.Models;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ApartmentReservation.Application.Features.Reservations.Queries
{
    public class GetAllReservationsQuery : IRequest<IEnumerable<ReservationDto>>
    {
        public long? HostId { get; set; }
        public long? GuestId { get; set; }
        public long? ApartmentId { get; set; }
        public string GuestUsername { get; set; }
        public string ReservationState { get; set; }
    }

    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, IEnumerable<ReservationDto>>
    {
        private readonly IQueryDbContext context;

        public GetAllReservationsQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            IMongoQueryable<ReservationModel> query = this.context.Reservations.AsQueryable();

            query = this.ApplyFilters(request, query);

            var reservations = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            return reservations.Select(r => CustomMapper.Map<ReservationDto>(r));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "RCS1155:Use StringComparison when comparing strings.", Justification = "Current version of MongoDB.Driver.Linq doesn't support it")]
        private IMongoQueryable<ReservationModel> ApplyFilters(GetAllReservationsQuery filters, IMongoQueryable<ReservationModel> query)
        {
            if (filters.HostId != null)
            {
                query = query.Where(r => r.HostId == filters.HostId);
            }

            if (filters.GuestId != null)
            {
                query = query.Where(r => r.GuestId == filters.GuestId);
            }
            if (!string.IsNullOrEmpty(filters.GuestUsername))
            {
                query = query.Where(r => r.GuestUsername.ToLower() == filters.GuestUsername.ToLower());
            }

            if (filters.ApartmentId != null)
            {
                query = query.Where(r => r.ApartmentId == filters.ApartmentId);
            }

            if (!string.IsNullOrEmpty(filters.ReservationState))
            {
                query = query.Where(r => r.ReservationState.ToLower() == filters.ReservationState.ToLower());
            }

            return query;
        }
    }
}