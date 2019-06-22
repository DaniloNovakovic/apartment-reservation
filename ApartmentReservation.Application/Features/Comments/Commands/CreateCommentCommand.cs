using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using MediatR;

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
            await Task.Delay(20);
            return new EntityCreatedResult();
        }
    }
}