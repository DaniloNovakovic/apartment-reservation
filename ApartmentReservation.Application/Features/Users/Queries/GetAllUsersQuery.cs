using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Read.Models;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ApartmentReservation.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public string RoleName { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IQueryDbContext context;

        public GetAllUsersQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            IMongoQueryable<UserModel> query = this.context.Users.AsQueryable();

            query = ApplyFilters(request, query);

            var users = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            return users.Select(u => CustomMapper.Map<UserDto>(u));
        }

        private static IMongoQueryable<UserModel> ApplyFilters(GetAllUsersQuery filters, IMongoQueryable<UserModel> query)
        {
            if (!string.IsNullOrWhiteSpace(filters.Gender))
            {
                query = query.Where(u => u.Gender == filters.Gender);
            }

            if (!string.IsNullOrWhiteSpace(filters.RoleName))
            {
                query = query.Where(u => u.RoleName == filters.RoleName);
            }

            if (!string.IsNullOrWhiteSpace(filters.Username))
            {
                query = query.Where(u => u.Username == filters.Username);
            }

            return query;
        }
    }
}