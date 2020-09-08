using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Exceptions;
using ApartmentReservation.Domain.Read.Models;
using MediatR;
using MongoDB.Driver;

namespace ApartmentReservation.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public long Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IQueryDbContext context;

        public GetUserByIdQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await context.Users.Find(u => u.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if (dbUser is null)
            {
                throw new NotFoundException($"User with id={request.Id} could not be found!");
            }

            return CustomMapper.Map<UserDto>(dbUser);
        }
    }
}