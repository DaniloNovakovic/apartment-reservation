﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Exceptions;
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

    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly IApartmentReservationDbContext context;

        public UpdateCommentCommandHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await context.Comments.FindAsync(request.Id).ConfigureAwait(false);

            if (comment is null)
            {
                throw new NotFoundException($"Could not find comment with id={request.Id}");
            }

            comment.Rating = request.Rating ?? comment.Rating;
            comment.Approved = request.Approved ?? comment.Approved;
            if (!string.IsNullOrEmpty(request.Text))
            {
                comment.Text = request.Text;
            }

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}