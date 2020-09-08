using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Apartments.Commands
{
    public class UpdateForRentalDatesCommand : IRequest
    {
        public UpdateForRentalDatesCommand()
        {
            this.ForRentalDates = new List<DateTime>();
        }

        public long ApartmentId { get; set; }
        public IEnumerable<DateTime> ForRentalDates { get; set; }
    }

    public class UpdateForRentalDatesCommandHandler : IRequestHandler<UpdateForRentalDatesCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateForRentalDatesCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateForRentalDatesCommand request, CancellationToken cancellationToken)
        {
            var apartment = await context.Apartments.SingleOrDefaultAsync(a => a.Id == request.ApartmentId, cancellationToken);

            if (apartment == null)
            {
                throw new NotFoundException($"Apartment with id {request.ApartmentId} not found!");
            }

            var forRentalDates = await this.context.ForRentalDates
                .Where(d => d.ApartmentId == request.ApartmentId && !d.IsDeleted)
                .ToDictionaryAsync(
                    keySelector: frd => ConvertDateToKey(frd.Date),
                    elementSelector: frd => new HelperClass(frd),
                    cancellationToken)
                .ConfigureAwait(false);

            foreach (var date in request.ForRentalDates)
            {
                string key = ConvertDateToKey(date);
                if (!forRentalDates.ContainsKey(key))
                {
                    this.context.ForRentalDates.Add(new ForRentalDate()
                    {
                        ApartmentId = request.ApartmentId,
                        Date = date
                    });
                }
                else
                {
                    forRentalDates[key].ForRentalDate.IsDeleted = false;
                    forRentalDates[key].IsFound = true;
                }
            }

            foreach (var item in forRentalDates.Values.Where(i => !i.IsFound))
            {
                item.ForRentalDate.IsDeleted = true;
            }


            apartment.IsSyncNeeded = true;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }

        public static string ConvertDateToKey(DateTime dateTime)
        {
            return $"{dateTime.Year}/{dateTime.Month}/{dateTime.Day}";
        }

        private class HelperClass
        {
            public bool IsFound { get; set; }
            public ForRentalDate ForRentalDate { get; set; }

            public HelperClass(ForRentalDate frd)
            {
                this.ForRentalDate = frd;
                this.IsFound = false;
            }
        }
    }
}