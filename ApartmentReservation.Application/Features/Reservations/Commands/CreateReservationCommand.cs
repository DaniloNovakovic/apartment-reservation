using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Features.Reservations.Queries;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using ApartmentReservation.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Reservations.Commands
{
    public class CreateReservationCommand : IRequest<EntityCreatedResult>
    {
        public long ApartmentId { get; set; }

        public long GuestId { get; set; }

        public DateTime StartDate { get; set; }

        public int NumberOfNights { get; set; }
    }

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, EntityCreatedResult>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMediator mediator;

        public CreateReservationCommandHandler(IApartmentReservationDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<EntityCreatedResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            await ThrowIfGuestDoesNotExist(request, cancellationToken).ConfigureAwait(false);
            await ThrowIfApartmentIsUnavailable(request, cancellationToken).ConfigureAwait(false);

            double totalCost = await GetTotalCost(request, cancellationToken).ConfigureAwait(false);

            var reservation = context.Reservations.Add(new Domain.Entities.Reservation()
            {
                ApartmentId = request.ApartmentId,
                GuestId = request.GuestId,
                ReservationStartDate = request.StartDate,
                NumberOfNightsRented = request.NumberOfNights,
                ReservationState = ReservationStates.Created,
                TotalCost = totalCost
            }).Entity;

            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new EntityCreatedResult() { Id = reservation.Id };
        }

        private async Task<double> GetTotalCost(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            return await mediator
                .Send(new GetTotalCostQuery()
                {
                    ApartmentId = request.ApartmentId,
                    StartDate = request.StartDate,
                    NumberOfNights = request.NumberOfNights
                }, cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task ThrowIfApartmentIsUnavailable(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var availableDates = await mediator.Send(new GetAvailableDatesQuery() { ApartmentId = request.ApartmentId }, cancellationToken);

            var currDay = request.StartDate;
            for (int i = 0; i < request.NumberOfNights; ++i)
            {
                if (!availableDates.Any(d => DateTimeHelpers.AreSameDay(d, currDay)))
                {
                    string formatedDay = DateTimeHelpers.FormatToYearMonthDayString(currDay);
                    throw new ApartmentUnavailableException($"{formatedDay} is unavailable!");
                }
                currDay = currDay.AddDays(1);
            }
        }

        private async Task ThrowIfGuestDoesNotExist(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var guest = await context.Guests
                            .SingleOrDefaultAsync(g => g.UserId == request.GuestId && !g.IsDeleted, cancellationToken)
                            .ConfigureAwait(false);

            if (guest is null)
            {
                throw new NotFoundException($"Guest {request.GuestId} not found");
            }
        }
    }
}