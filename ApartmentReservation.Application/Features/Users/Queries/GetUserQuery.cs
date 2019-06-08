using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Users.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public long Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public GetUserQueryHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await this.context.Users.SingleOrDefaultAsync(u => u.Id == request.Id && !u.IsDeleted).ConfigureAwait(false);

            if (dbUser is null)
            {
                throw new NotFoundException($"User with id={request.Id} could not be found!");
            }

            return this.mapper.Map<UserDto>(dbUser);
        }
    }
}