using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Exceptions;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetHostQuery : IRequest<HostDto>
    {
        public long Id { get; set; }
    }

    public class GetHostQueryHandler : IRequestHandler<GetHostQuery, HostDto>
    {
        private readonly IApartmentReservationDbContext context;
        private readonly IMapper mapper;

        public GetHostQueryHandler(IApartmentReservationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<HostDto> Handle(GetHostQuery request, CancellationToken cancellationToken)
        {
            var host = await this.context.Hosts
                .Include(h => h.User)
                .SingleOrDefaultAsync(h => h.UserId == request.Id && !h.IsDeleted).ConfigureAwait(false);

            if (host == null)
            {
                throw new NotFoundException($"Could not find user with id={request.Id}");
            }

            return this.mapper.Map<HostDto>(host);
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