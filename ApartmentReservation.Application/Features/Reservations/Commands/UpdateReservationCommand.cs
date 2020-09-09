using System;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Reservations.Commands
{
    public class UpdateReservationCommand : IRequest
    {
        /// <summary>
        /// Accepts old reservation state and returns bool
        /// </summary>
        public Predicate<CanUpdateReservationArgs> CanUpdate { get; set; }

        public long Id { get; set; }
        public string ReservationState { get; set; }
    }

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateReservationCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var dbReservation = await this.context.Reservations
                .Include(r => r.Apartment)
                .SingleOrDefaultAsync(r => r.Id == request.Id && !r.IsDeleted, cancellationToken)
                .ConfigureAwait(false);

            var canUpdateArgs = new CanUpdateReservationArgs()
            {
                GuestId = dbReservation.GuestId ?? -1,
                HostId = dbReservation.Apartment.HostId ?? -1,
                NumberOfNightsRented = dbReservation.NumberOfNightsRented,
                ReservationState = dbReservation.ReservationState,
                ReservationStartDate = dbReservation.ReservationStartDate,
                TotalCost = dbReservation.TotalCost,
            };

            if (request.CanUpdate(canUpdateArgs))
            {
                dbReservation.ReservationState = request.ReservationState;
            }
            else
            {
                throw new CustomInvalidOperationException();
            }

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}