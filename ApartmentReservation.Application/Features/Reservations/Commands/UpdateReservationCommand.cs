using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using MediatR;

namespace ApartmentReservation.Application.Features.Reservations.Commands
{
    public class UpdateReservationCommand : IRequest
    {
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
            var dbReservation = await context.Reservations.FindAsync(new object[] { request.Id }, cancellationToken).ConfigureAwait(false);

            dbReservation.ReservationState = request.ReservationState;

            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}