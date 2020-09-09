using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Reservations.Queries.Legacy;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Exceptions;
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
        private readonly ICostCalculator costCalculator;

        // TODO: Refactor code so that handler doesn't rely on IMediator (Reason: Using IMediator inside command handler is bad practice)
        private readonly IMediator mediator;

        public CreateReservationCommandHandler(IApartmentReservationDbContext context, IMediator mediator, ICostCalculator costCalculator)
        {
            this.context = context;
            this.mediator = mediator;
            this.costCalculator = costCalculator;
        }

        public async Task<EntityCreatedResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            await this.ThrowIfGuestDoesNotExist(request, cancellationToken).ConfigureAwait(false);
            await this.ThrowIfApartmentIsUnavailable(request, cancellationToken).ConfigureAwait(false);

            double totalCost = await this.GetTotalCost(request, cancellationToken).ConfigureAwait(false);

            var reservation = this.context.Reservations.Add(new Domain.Entities.Reservation()
            {
                ApartmentId = request.ApartmentId,
                GuestId = request.GuestId,
                ReservationStartDate = request.StartDate,
                NumberOfNightsRented = request.NumberOfNights,
                ReservationState = ReservationStates.Created,
                TotalCost = totalCost
            }).Entity;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new EntityCreatedResult() { Id = reservation.Id };
        }

        private async Task<double> GetTotalCost(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            return await this.costCalculator.CalculateTotalCostAsync(new GetTotalCostArgs()
                {
                    ApartmentId = request.ApartmentId,
                    StartDate = request.StartDate,
                    NumberOfNights = request.NumberOfNights
                }, cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task ThrowIfApartmentIsUnavailable(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var availableDates = await this.mediator.Send(new GetAvailableDatesQuery() { ApartmentId = request.ApartmentId }, cancellationToken);

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
            var guest = await this.context.Guests
                            .SingleOrDefaultAsync(g => g.UserId == request.GuestId && !g.IsDeleted, cancellationToken)
                            .ConfigureAwait(false);

            if (guest is null)
            {
                throw new NotFoundException($"Guest {request.GuestId} not found");
            }
        }
    }
}