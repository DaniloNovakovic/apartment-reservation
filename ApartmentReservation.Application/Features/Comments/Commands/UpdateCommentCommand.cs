using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces;
using MediatR;

namespace ApartmentReservation.Application.Features.Comments.Commands
{
    public class UpdateCommentCommand : IRequest
    {
        public byte? Rating { get; set; }
        public string Text { get; set; }
        public long Id { get; set; }
        public bool? Approved { get; set; }
    }

    public class UpdateCommendCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateCommendCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(20);
            return Unit.Value;
        }
    }
}