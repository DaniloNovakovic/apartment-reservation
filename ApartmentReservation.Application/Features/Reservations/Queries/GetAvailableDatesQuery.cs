﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApartmentReservation.Domain.Entities;
using ApartmentReservation.Domain.Constants;

namespace ApartmentReservation.Application.Features.Reservations.Queries
{
    public class GetAvailableDatesQuery : IRequest<IEnumerable<DateTime>>
    {
        public long ApartmentId { get; set; }
    }

    public class GetAvailableDatesQueryHandler : IRequestHandler<GetAvailableDatesQuery, IEnumerable<DateTime>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAvailableDatesQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DateTime>> Handle(GetAvailableDatesQuery request, CancellationToken cancellationToken)
        {
            long apartmentId = request.ApartmentId;

            var apartment = await context.Apartments
                .Include(a => a.ForRentalDates)
                .Include(a => a.Reservations)
                .SingleOrDefaultAsync(a => a.Id == apartmentId && !a.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            if (apartment is null)
            {
                throw new NotFoundException($"Apartment {apartmentId} not found");
            }

            var forRentalDates = apartment.ForRentalDates.Where(frd => !frd.IsDeleted).ToList();

            var reservationStatesToIgnore = new[] { ReservationStates.Denied, ReservationStates.Withdrawn };
            var reservations = apartment.Reservations
                .Where(r => !r.IsDeleted && !reservationStatesToIgnore.Contains(r.ReservationState)).ToList();

            return forRentalDates
                .Where(forRentalDate => IsDateAvailable(forRentalDate.Date, reservations))
                .Select(forRentalDate => forRentalDate.Date).ToList();
        }

        private static bool IsDateAvailable(DateTime date, IEnumerable<Reservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                if (DateTimeHelpers.IsContainedInDayRange(date, reservation.ReservationStartDate, reservation.NumberOfNightsRented))
                {
                    return false;
                }
            }

            return true;
        }
    }
}