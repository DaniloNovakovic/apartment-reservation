using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Exceptions;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetHostQuery : IRequest<HostDto>
    {
        public long Id { get; set; }
    }

    public class GetHostQueryHandler : IRequestHandler<GetHostQuery, HostDto>
    {
        private readonly IQueryDbContext context;

        public GetHostQueryHandler(IQueryDbContext context)
        {
            this.context = context;
        }

        public async Task<HostDto> Handle(GetHostQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await context.Users.Find(u => u.Id == request.Id && u.RoleName == RoleNames.Host).SingleOrDefaultAsync(cancellationToken);

            if (dbUser is null)
            {
                throw new NotFoundException($"User with id={request.Id} could not be found!");
            }

            return CustomMapper.Map<HostDto>(dbUser);
        }
    }

    public class GetHostQueryValidator : AbstractValidator<GetHostQuery>
    {
        public GetHostQueryValidator()
        {
            this.RuleFor(q => q.Id).NotEmpty();
        }
    }
}