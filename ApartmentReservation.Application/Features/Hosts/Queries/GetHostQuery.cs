using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Dtos;
using ApartmentReservation.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace ApartmentReservation.Application.Features.Hosts
{
    public class GetHostQuery : IRequest<HostDto>
    {
        public string Id { get; set; }
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
            return new HostDto();
        }
    }

    public class GetHostQueryValidator : AbstractValidator<GetHostQuery>
    {
        public GetHostQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }
}