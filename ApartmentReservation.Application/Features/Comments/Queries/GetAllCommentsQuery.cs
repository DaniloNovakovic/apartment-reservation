using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Application.Features.Comments.Queries
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<CommentDto>>
    {
        public long ApartmentId { get; set; }
        public bool? Approved { get; set; }
    }

    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentDto>>
    {
        private readonly IQueryDbContext context;

        public GetAllCommentsQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await context.Apartments
                .Find(a => a.Id == request.ApartmentId)
                .Project(a => a.Comments)
                .SingleOrDefaultAsync(cancellationToken);

            if (request.Approved != null)
                comments = comments.Where(c => c.Approved == request.Approved);

            return comments.Select(c => CustomMapper.Map<CommentDto>(c)).ToList();
        }

    }
}