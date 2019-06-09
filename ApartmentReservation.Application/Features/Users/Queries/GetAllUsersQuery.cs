using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public GetAllUsersQueryHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var query = this.context.Users.Where(u => !u.IsDeleted);

            query = ApplyFilters(request, query);

            var users = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

            return users.Select(this.mapper.Map<UserDto>);
        }

        private static IQueryable<User> ApplyFilters(GetAllUsersQuery filters, IQueryable<User> query)
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