using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        private readonly IApartmentReservationDbContext context;

        public GetAllReservationsQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var query = this.context.Reservations
                .Include(r => r.Guest).ThenInclude(g => g.User)
                .Include(r => r.Apartment).ThenInclude(a => a.Host).ThenInclude(h => h.User)
                .Where(r => !r.IsDeleted && !r.Apartment.IsDeleted && !r.Guest.IsDeleted);

            query = this.ApplyFilters(request, query);

            var reservations = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            return reservations.Select(r => new ReservationDto(r));
        }

        private IQueryable<Reservation> ApplyFilters(GetAllReservationsQuery filters, IQueryable<Reservation> query)
        {
            if (filters.HostId != null)
            {
                query = query.Where(r => r.Apartment.HostId == filters.HostId);
            }

            if (filters.GuestId != null)
            {
                query = query.Where(r => r.GuestId == filters.GuestId);
            }
            if (!string.IsNullOrEmpty(filters.GuestUsername))
            {
                query = query.Where(r => string.Equals(r.Guest.User.Username, filters.GuestUsername, StringComparison.OrdinalIgnoreCase));
            }

            if (filters.ApartmentId != null)
            {
                query = query.Where(r => r.ApartmentId == filters.ApartmentId);
            }

            if (!string.IsNullOrEmpty(filters.ReservationState))
            {
                query = query.Where(r => string.Equals(r.ReservationState, filters.ReservationState, StringComparison.OrdinalIgnoreCase));
            }

            return query;
        }
    }
}