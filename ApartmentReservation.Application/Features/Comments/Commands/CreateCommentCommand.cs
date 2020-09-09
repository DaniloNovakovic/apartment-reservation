using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Comments.Commands
{
    public class CreateCommentCommand : IRequest<EntityCreatedResult>
    {
        public long ApartmentId { get; set; }
        public long GuestId { get; set; }
        public byte Rating { get; set; }
        public string Text { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, EntityCreatedResult>
    {
        private readonly IApartmentReservationDbContext context;

        public CreateCommentCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<EntityCreatedResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var apartment = await this.context.Apartments.SingleOrDefaultAsync(a => !a.IsDeleted && a.Id == request.ApartmentId, cancellationToken).ConfigureAwait(false);
            if (apartment is null)
            {
                throw new NotFoundException($"apartment {request.ApartmentId} not found!");
            }

            var guest = await this.context.Guests.SingleOrDefaultAsync(g => !g.IsDeleted && g.UserId == request.GuestId, cancellationToken).ConfigureAwait(false);
            if (guest is null)
            {
                throw new NotFoundException($"guest {request.GuestId} not found!");
            }

            var addedComment = this.context.Comments.Add(new Domain.Entities.Comment()
            {
                ApartmentId = request.ApartmentId,
                GuestId = request.GuestId,
                Rating = request.Rating,
                Text = request.Text
            }).Entity;


            apartment.IsSyncNeeded = true;

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new EntityCreatedResult() { Id = addedComment.Id };
        }
    }
}