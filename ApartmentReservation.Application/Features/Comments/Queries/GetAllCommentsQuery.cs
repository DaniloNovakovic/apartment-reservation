using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Comments.Queries
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<CommentDto>>
    {
        public long? ApartmentId { get; set; }
        public bool? Approved { get; set; }
    }

    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentDto>>
    {
        private readonly IApartmentReservationDbContext context;

        public GetAllCommentsQueryHandler(IApartmentReservationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = this.context.Comments.Include(c => c.Guest).ThenInclude(g => g.User).Where(c => !c.IsDeleted);

            query = ApplyFilters(request, query);

            var comments = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            return comments.Select(c => new CommentDto(c));
        }

        private static IQueryable<Domain.Entities.Comment> ApplyFilters(GetAllCommentsQuery filters, IQueryable<Domain.Entities.Comment> query)
        {
            if (filters.Approved != null)
            {
                query = query.Where(c => c.Approved == filters.Approved);
            }

            if (filters.ApartmentId != null)
            {
                query = query.Where(c => c.ApartmentId == filters.ApartmentId);
            }

            return query;
        }
    }
}